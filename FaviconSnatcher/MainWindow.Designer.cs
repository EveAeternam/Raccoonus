namespace FaviconSnatcher
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            btn_Clipboard = new Button();
            btn_Snatch = new Button();
            tb_WebsiteInput = new TextBox();
            lv_Results = new ListView();
            contextMenuStrip = new ContextMenuStrip(components);
            saveSelectedToolStripMenuItem = new ToolStripMenuItem();
            selectedToolStripMenuItem = new ToolStripMenuItem();
            allToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            viewToolStripMenuItem = new ToolStripMenuItem();
            iconsToolStripMenuItem = new ToolStripMenuItem();
            detailsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolStripProgressBar = new ToolStripProgressBar();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(lv_Results, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(612, 384);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.Controls.Add(btn_Clipboard, 0, 0);
            tableLayoutPanel2.Controls.Add(btn_Snatch, 2, 0);
            tableLayoutPanel2.Controls.Add(tb_WebsiteInput, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(606, 44);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_Clipboard
            // 
            btn_Clipboard.Dock = DockStyle.Fill;
            btn_Clipboard.Image = Properties.Resources.Clipboard;
            btn_Clipboard.Location = new Point(3, 3);
            btn_Clipboard.Name = "btn_Clipboard";
            btn_Clipboard.Size = new Size(54, 38);
            btn_Clipboard.TabIndex = 0;
            btn_Clipboard.UseVisualStyleBackColor = true;
            btn_Clipboard.Click += btn_Clipboard_Click;
            // 
            // btn_Snatch
            // 
            btn_Snatch.Dock = DockStyle.Fill;
            btn_Snatch.Image = Properties.Resources.Wand;
            btn_Snatch.Location = new Point(517, 3);
            btn_Snatch.Name = "btn_Snatch";
            btn_Snatch.Size = new Size(86, 38);
            btn_Snatch.TabIndex = 1;
            btn_Snatch.Text = "Snatch!";
            btn_Snatch.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_Snatch.UseVisualStyleBackColor = true;
            btn_Snatch.Click += btn_Snatch_Click;
            // 
            // tb_WebsiteInput
            // 
            tb_WebsiteInput.AllowDrop = true;
            tb_WebsiteInput.Dock = DockStyle.Fill;
            tb_WebsiteInput.Font = new Font("Segoe UI", 9F);
            tb_WebsiteInput.Location = new Point(63, 3);
            tb_WebsiteInput.Multiline = true;
            tb_WebsiteInput.Name = "tb_WebsiteInput";
            tb_WebsiteInput.Size = new Size(448, 38);
            tb_WebsiteInput.TabIndex = 2;
            // 
            // lv_Results
            // 
            lv_Results.CheckBoxes = true;
            lv_Results.ContextMenuStrip = contextMenuStrip;
            lv_Results.Dock = DockStyle.Fill;
            lv_Results.Location = new Point(3, 53);
            lv_Results.Name = "lv_Results";
            lv_Results.Size = new Size(606, 328);
            lv_Results.TabIndex = 1;
            lv_Results.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { saveSelectedToolStripMenuItem, toolStripSeparator1, viewToolStripMenuItem, toolStripSeparator2, refreshToolStripMenuItem, clearToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(114, 104);
            // 
            // saveSelectedToolStripMenuItem
            // 
            saveSelectedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { selectedToolStripMenuItem, allToolStripMenuItem });
            saveSelectedToolStripMenuItem.Name = "saveSelectedToolStripMenuItem";
            saveSelectedToolStripMenuItem.Size = new Size(113, 22);
            saveSelectedToolStripMenuItem.Text = "Save...";
            // 
            // selectedToolStripMenuItem
            // 
            selectedToolStripMenuItem.Name = "selectedToolStripMenuItem";
            selectedToolStripMenuItem.Size = new Size(118, 22);
            selectedToolStripMenuItem.Text = "Selected";
            selectedToolStripMenuItem.Click += selectedToolStripMenuItem_Click;
            // 
            // allToolStripMenuItem
            // 
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            allToolStripMenuItem.Size = new Size(118, 22);
            allToolStripMenuItem.Text = "All";
            allToolStripMenuItem.Click += allToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(110, 6);
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { iconsToolStripMenuItem, detailsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(113, 22);
            viewToolStripMenuItem.Text = "View...";
            // 
            // iconsToolStripMenuItem
            // 
            iconsToolStripMenuItem.Checked = true;
            iconsToolStripMenuItem.CheckState = CheckState.Checked;
            iconsToolStripMenuItem.Name = "iconsToolStripMenuItem";
            iconsToolStripMenuItem.Size = new Size(134, 22);
            iconsToolStripMenuItem.Text = "Large Icons";
            iconsToolStripMenuItem.Click += iconsToolStripMenuItem_Click;
            // 
            // detailsToolStripMenuItem
            // 
            detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            detailsToolStripMenuItem.Size = new Size(134, 22);
            detailsToolStripMenuItem.Text = "Details";
            detailsToolStripMenuItem.Click += detailsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(110, 6);
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(113, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(113, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel, toolStripProgressBar });
            statusStrip.Location = new Point(0, 362);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(612, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(96, 17);
            toolStripStatusLabel.Text = "No icons loaded.";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new Size(240, 16);
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(612, 384);
            Controls.Add(statusStrip);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainWindow";
            Text = "Favicon Snatcher";
            TopMost = true;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            contextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btn_Clipboard;
        private Button btn_Snatch;
        private TextBox tb_WebsiteInput;
        private ListView lv_Results;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem saveSelectedToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem iconsToolStripMenuItem;
        private ToolStripMenuItem detailsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripMenuItem selectedToolStripMenuItem;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripProgressBar toolStripProgressBar;
    }
}
