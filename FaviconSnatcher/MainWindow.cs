using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace FaviconSnatcher
{
    /// <summary>
    /// Main application window for the Favicon Snatcher tool.
    /// Allows users to download and manage favicons from websites.
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// The current website URL being processed.
        /// </summary>
        public string WebsiteURL = string.Empty;

        /// <summary>
        /// Optional form window for viewing large favicon images.
        /// </summary>
        private Form? _viewerWindow;

        /// <summary>
        /// Picture box used in the viewer window to display favicons.
        /// </summary>
        private PictureBox? _viewerImage;

        /// <summary>
        /// Image list for storing and displaying favicons in the ListView.
        /// </summary>
        private readonly ImageList _faviconImageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// Sets up UI components, event handlers, and initial state.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Text = $"Raccoonus Favicon Snatcher (v{Application.ProductVersion})";
            lv_Results.View = View.LargeIcon;
            lv_Results.LargeImageList = _faviconImageList;
            lv_Results.SmallImageList = _faviconImageList;
            lv_Results.ItemActivate += lv_Results_ItemActivate;
            lv_Results.ItemChecked += lv_Results_ItemChecked;
            tb_WebsiteInput.DragEnter += tb_WebsiteInput_DragEnter;
            tb_WebsiteInput.DragDrop += tb_WebsiteInput_DragDrop;
            contextMenuStrip.Opening += contextMenuStrip_Opening;
            EnsureDetailsColumns();
            AcceptButton = btn_Snatch;
            UpdateSaveMenuState();
            toolStripProgressBar.Visible = false;
        }

        /// <summary>
        /// Handles the Clipboard button click event.
        /// Pastes text from clipboard into the website input field.
        /// </summary>
        private void btn_Clipboard_Click(object sender, EventArgs e)
        {
            tb_WebsiteInput.Text = Clipboard.GetText();
        }

        /// <summary>
        /// Handles the Snatch button click event.
        /// Processes URLs from the input field and downloads their favicons.
        /// </summary>
        private async void btn_Snatch_Click(object sender, EventArgs e)
        {
            List<string> urls = GetUrlsFromInput(tb_WebsiteInput.Text);
            if (urls.Count == 0)
            {
                UpdateStatus("Invalid URL.");
                return;
            }

            tb_WebsiteInput.Text = "";
            int totalAdded = 0;
            
            foreach (string url in urls)
            {
                WebsiteURL = url;
                int added = await GetFavicon(url);
                totalAdded += added;
            }

            if (urls.Count > 1)
            {
                UpdateStatus(totalAdded == 0
                    ? $"No new icons added from {urls.Count} links."
                    : $"Added {totalAdded} icon(s) from {urls.Count} links.");
            }
        }

        /// <summary>
        /// Cleans and normalizes a URL to its base form (scheme + host).
        /// </summary>
        /// <param name="url">The URL to clean.</param>
        /// <returns>The cleaned URL in the format "scheme://host", or the original URL if parsing fails.</returns>
        public string CleanURL(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                return $"{uri.Scheme}://{uri.Host}";
            }
            catch
            {
                return url; // Return the original URL if it's not valid
            }
        }

        /// <summary>
        /// Attempts to parse and validate a URL string.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <param name="url">When this method returns, contains the cleaned URL if validation succeeds; otherwise, empty string.</param>
        /// <returns>True if a valid URL was extracted; otherwise, false.</returns>
        private bool TryGetValidUrl(string? input, out string url)
        {
            url = string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            string trimmed = input.Trim();
            if (Uri.TryCreate(trimmed, UriKind.Absolute, out Uri? absoluteUri))
            {
                url = CleanURL(absoluteUri.AbsoluteUri);
                return true;
            }

            if (Uri.TryCreate($"https://{trimmed}", UriKind.Absolute, out Uri? httpsUri))
            {
                url = CleanURL(httpsUri.AbsoluteUri);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Extracts and validates multiple URLs from a delimited input string.
        /// Supports comma and newline delimiters.
        /// </summary>
        /// <param name="input">The input string containing one or more URLs.</param>
        /// <returns>A list of valid, cleaned URLs.</returns>
        private List<string> GetUrlsFromInput(string? input)
        {
            List<string> urls = new List<string>();
            if (string.IsNullOrWhiteSpace(input))
            {
                return urls;
            }

            string[] parts = input.Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (string part in parts)
            {
                if (TryGetValidUrl(part, out string url))
                {
                    urls.Add(url);
                }
            }

            return urls;
        }

        /// <summary>
        /// Downloads and processes favicons from a given URL.
        /// Searches the HTML for icon links, downloads images, and adds them to the results list.
        /// </summary>
        /// <param name="url">The website URL to scan for favicons.</param>
        /// <returns>The number of new favicons successfully added to the results.</returns>
        public async Task<int> GetFavicon(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                UpdateStatus("Enter a website URL.");
                return 0;
            }

            UpdateStatus($"Loading {url}...");
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar.Visible = true;
            
            using HttpClient client = new HttpClient();
            string html;

            try
            {
                html = await client.GetStringAsync(url);
            }
            catch
            {
                UpdateStatus("Failed to load page.");
                toolStripProgressBar.Visible = false;
                return 0;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? baseUri))
            {
                UpdateStatus("Invalid URL.");
                toolStripProgressBar.Visible = false;
                return 0;
            }

            UpdateStatus("Scanning for icon links...");

            if (_viewerImage != null)
            {
                _viewerImage.Image?.Dispose();
                _viewerImage.Image = null;
            }

            // Parse HTML for favicon link tags and collect candidates
            List<Uri> icoCandidates = new List<Uri>();
            List<Uri> otherCandidates = new List<Uri>();
            HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            MatchCollection linkMatches = Regex.Matches(html, "<link\\b[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match match in linkMatches)
            {
                string linkTag = match.Value;
                Match hrefMatch = Regex.Match(linkTag, "href\\s*=\\s*[\"'](?<href>[^\"'#>]+)[\"']", RegexOptions.IgnoreCase);
                if (!hrefMatch.Success)
                {
                    continue;
                }

                string href = hrefMatch.Groups["href"].Value;
                Match relMatch = Regex.Match(linkTag, "rel\\s*=\\s*[\"'](?<rel>[^\"']+)[\"']", RegexOptions.IgnoreCase);
                string relValue = relMatch.Success ? relMatch.Groups["rel"].Value : string.Empty;

                bool isIconRel = relValue.Contains("icon", StringComparison.OrdinalIgnoreCase);
                bool isIco = href.EndsWith(".ico", StringComparison.OrdinalIgnoreCase);

                if (!isIconRel && !isIco)
                {
                    continue;
                }

                if (!Uri.TryCreate(baseUri, href, out Uri? iconUri))
                {
                    continue;
                }

                if (!seen.Add(iconUri.AbsoluteUri))
                {
                    continue;
                }

                // Prioritize .ico files over other formats
                if (isIco)
                {
                    icoCandidates.Add(iconUri);
                }
                else
                {
                    otherCandidates.Add(iconUri);
                }
            }

            List<Uri> candidates = icoCandidates.Count > 0 ? icoCandidates : otherCandidates;
            if (candidates.Count == 0)
            {
                UpdateStatus("No icon links found.");
                toolStripProgressBar.Visible = false;
                return 0;
            }

            UpdateStatus($"Downloading {candidates.Count} icon(s)...");
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
            toolStripProgressBar.Maximum = candidates.Count;
            toolStripProgressBar.Value = 0;

            // Build dictionary of existing groups for efficient lookup
            Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>(StringComparer.OrdinalIgnoreCase);
            foreach (ListViewGroup existingGroup in lv_Results.Groups)
            {
                groups[existingGroup.Name] = existingGroup;
            }

            int addedCount = 0;
            int processed = 0;

            // Download and process each favicon candidate
            foreach (Uri candidate in candidates)
            {
                try
                {
                    using HttpResponseMessage response = await client.GetAsync(candidate);
                    if (!response.IsSuccessStatusCode)
                    {
                        continue;
                    }

                    byte[] data = await response.Content.ReadAsByteArrayAsync();
                    await using Stream stream = new MemoryStream(data);
                    Image? image = null;

                    // Handle .ico format specially; other formats can be loaded directly
                    if (candidate.AbsolutePath.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
                    {
                        using Icon icon = new Icon(stream);
                        image = icon.ToBitmap();
                    }
                    else
                    {
                        image = Image.FromStream(stream);
                    }

                    if (image == null)
                    {
                        continue;
                    }

                    string key = candidate.AbsoluteUri;
                    if (!_faviconImageList.Images.ContainsKey(key))
                    {
                        _faviconImageList.Images.Add(key, image);
                    }
                    else
                    {
                        image.Dispose();
                    }

                    // Group favicons by file extension
                    string extension = Path.GetExtension(candidate.AbsolutePath);
                    string groupKey = string.IsNullOrWhiteSpace(extension) ? "(none)" : extension.ToLowerInvariant();

                    if (!groups.TryGetValue(groupKey, out ListViewGroup? group))
                    {
                        group = new ListViewGroup(groupKey, groupKey) { Name = groupKey };
                        groups.Add(groupKey, group);
                        lv_Results.Groups.Add(group);
                    }

                    // Add to ListView if not already present
                    if (!lv_Results.Items.Cast<ListViewItem>().Any(item => string.Equals(item.Text, candidate.AbsoluteUri, StringComparison.OrdinalIgnoreCase)))
                    {
                        ListViewItem item = new ListViewItem(candidate.AbsoluteUri) { ImageKey = key, Group = group };
                        item.SubItems.Add(groupKey);
                        item.Tag = new FaviconItem(data, extension);
                        lv_Results.Items.Add(item);
                        addedCount++;
                    }

                }
                catch
                {
                    // Continue on error
                }
                finally
                {
                    processed++;
                    toolStripProgressBar.Value = processed;
                }
            }

            toolStripProgressBar.Visible = false;
            
            UpdateStatus(addedCount == 0
                ? "No new icons added."
                : $"Added {addedCount} icon(s).");

            return addedCount;
        }

        /// <summary>
        /// Updates the status bar with a message.
        /// </summary>
        /// <param name="message">The status message to display.</param>
        private void UpdateStatus(string message)
        {
            toolStripStatusLabel.Text = message;
        }

        /// <summary>
        /// Ensures the ListView has the required columns for Details view.
        /// </summary>
        private void EnsureDetailsColumns()
        {
            if (lv_Results.Columns.Count > 0)
            {
                return;
            }

            lv_Results.Columns.Add("URL", 520, HorizontalAlignment.Left);
            lv_Results.Columns.Add("Type", 80, HorizontalAlignment.Left);
        }

        /// <summary>
        /// Handles the ItemActivate event for the results ListView.
        /// Opens a save dialog to export the selected favicon.
        /// </summary>
        private void lv_Results_ItemActivate(object? sender, EventArgs e)
        {
            if (lv_Results.SelectedItems.Count == 0)
            {
                return;
            }

            if (lv_Results.SelectedItems[0].Tag is not FaviconItem favicon)
            {
                return;
            }

            string extension = string.IsNullOrWhiteSpace(favicon.Extension) ? ".png" : favicon.Extension.ToLowerInvariant();
            using SaveFileDialog dialog = new SaveFileDialog
            {
                DefaultExt = extension.TrimStart('.'),
                FileName = $"favicon{extension}",
                Filter = $"{extension} files|*{extension}|All files|*.*"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            File.WriteAllBytes(dialog.FileName, favicon.Data);
        }

        /// <summary>
        /// Handles the Refresh menu item click event.
        /// Refreshes the ListView display.
        /// </summary>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_Results.Refresh();
        }

        /// <summary>
        /// Handles the Clear menu item click event.
        /// Removes all items from the results ListView.
        /// </summary>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_Results.Items.Clear();
        }

        /// <summary>
        /// Handles the Icons view menu item click event.
        /// Switches the ListView to large icon view.
        /// </summary>
        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_Results.View = View.LargeIcon;
            lv_Results.ShowGroups = true;
            iconsToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
            lv_Results.Refresh();
        }

        /// <summary>
        /// Handles the Details view menu item click event.
        /// Switches the ListView to details (list) view.
        /// </summary>
        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_Results.View = View.Details;
            lv_Results.ShowGroups = true;
            iconsToolStripMenuItem.Checked = false;
            detailsToolStripMenuItem.Checked = true;
            lv_Results.Refresh();
        }

        /// <summary>
        /// Handles the DragEnter event for the website input field.
        /// Accepts text drag-and-drop operations.
        /// </summary>
        private void tb_WebsiteInput_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.Text) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the DragDrop event for the website input field.
        /// Processes dropped URLs and downloads their favicons.
        /// </summary>
        private async void tb_WebsiteInput_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.Text) != true)
            {
                return;
            }

            string? text = e.Data.GetData(DataFormats.Text) as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            Debug.WriteLine($"DragDrop content: {text}");
            List<string> urls = GetUrlsFromInput(text);
            if (urls.Count == 0)
            {
                UpdateStatus("Invalid URL.");
                return;
            }

            tb_WebsiteInput.Text = string.Empty;
            int totalAdded = 0;
            
            foreach (string url in urls)
            {
                int added = await GetFavicon(url);
                totalAdded += added;
            }

            if (urls.Count > 1)
            {
                UpdateStatus(totalAdded == 0
                    ? $"No new icons added from {urls.Count} links."
                    : $"Added {totalAdded} icon(s) from {urls.Count} links.");
            }
        }

        /// <summary>
        /// Handles the Selected save menu item click event.
        /// Exports all checked favicons to a folder.
        /// </summary>
        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkedItems = lv_Results.CheckedItems.Cast<ListViewItem>()
                .Where(item => item.Tag is FaviconItem)
                .ToList();

            if (checkedItems.Count == 0)
            {
                return;
            }

            SaveFavicons(checkedItems);
        }

        /// <summary>
        /// Handles the All save menu item click event.
        /// Exports all favicons to a folder.
        /// </summary>
        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allItems = lv_Results.Items.Cast<ListViewItem>()
                .Where(item => item.Tag is FaviconItem)
                .ToList();

            if (allItems.Count == 0)
            {
                return;
            }

            SaveFavicons(allItems);
        }

        /// <summary>
        /// Saves a collection of favicons to a user-selected folder.
        /// </summary>
        /// <param name="items">The list of ListView items containing favicons to save.</param>
        private void SaveFavicons(List<ListViewItem> items)
        {
            if (items.Count == 0)
            {
                return;
            }

            using FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = $"Select folder to save {items.Count} favicon(s)",
                UseDescriptionForTitle = true
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            int savedCount = 0;
            Dictionary<string, int> fileNameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (ListViewItem item in items)
            {
                if (item.Tag is not FaviconItem favicon)
                {
                    continue;
                }

                try
                {
                    string extension = string.IsNullOrWhiteSpace(favicon.Extension) ? ".png" : favicon.Extension.ToLowerInvariant();
                    if (!extension.StartsWith("."))
                    {
                        extension = "." + extension;
                    }

                    // Generate unique filenames by incrementing a counter
                    string baseFileName = "favicon";
                    if (!fileNameCounts.TryGetValue(baseFileName, out int count))
                    {
                        count = 0;
                    }

                    string fileName = count == 0 
                        ? $"{baseFileName}{extension}" 
                        : $"{baseFileName}_{count}{extension}";
                    
                    fileNameCounts[baseFileName] = count + 1;

                    string fullPath = Path.Combine(dialog.SelectedPath, fileName);
                    File.WriteAllBytes(fullPath, favicon.Data);
                    savedCount++;
                }
                catch
                {
                    continue;
                }
            }

            UpdateStatus(savedCount == 0
                ? "Failed to save icons."
                : $"Saved {savedCount} icon(s) to {dialog.SelectedPath}");
        }

        /// <summary>
        /// Handles the context menu Opening event.
        /// Updates the save menu state based on current selection.
        /// </summary>
        private void contextMenuStrip_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateSaveMenuState();
        }

        /// <summary>
        /// Handles the ItemChecked event for the results ListView.
        /// Updates the save menu state when checkbox state changes.
        /// </summary>
        private void lv_Results_ItemChecked(object? sender, ItemCheckedEventArgs e)
        {
            UpdateSaveMenuState();
        }

        /// <summary>
        /// Updates the state and labels of save menu items based on selection count.
        /// </summary>
        private void UpdateSaveMenuState()
        {
            int checkedCount = lv_Results.CheckedItems.Count;
            int totalCount = lv_Results.Items.Count;

            selectedToolStripMenuItem.Enabled = checkedCount > 0;
            selectedToolStripMenuItem.Text = checkedCount == 0
                ? "Selected"
                : $"Selected ({checkedCount})";

            allToolStripMenuItem.Enabled = totalCount > 0;
            allToolStripMenuItem.Text = totalCount == 0
                ? "All"
                : $"All ({totalCount})";
        }

        /// <summary>
        /// Represents a downloaded favicon with its binary data and file extension.
        /// </summary>
        /// <param name="Data">The binary data of the favicon image.</param>
        /// <param name="Extension">The file extension of the favicon (e.g., ".png", ".ico").</param>
        private sealed record FaviconItem(byte[] Data, string Extension);
    }
}
