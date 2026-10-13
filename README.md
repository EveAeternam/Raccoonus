<p align="center">
  <img src="FaviconSnatcher/Resources/Raccoonus.png" alt="Raccoonus" width="96" />
</p>

<h1 align="center">Raccoonus Favicon Snatcher</h1>

<p align="center">
  A small Windows app that grabs the favicons from any website so you can preview and save them.
</p>

<p align="center">
  <a href="https://github.com/EveAeternam/Raccoonus/actions/workflows/build.yml"><img src="https://github.com/EveAeternam/Raccoonus/actions/workflows/build.yml/badge.svg" alt="Build status" /></a>
  <img src="https://img.shields.io/badge/.NET-10-512BD4" alt=".NET 10" />
  <img src="https://img.shields.io/badge/platform-Windows-0078D6" alt="Windows" />
  <a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-green" alt="MIT license" /></a>
</p>

<!--
  Screenshot: save a capture of the main window as docs/screenshot.png,
  then uncomment the line below.
  <p align="center"><img src="docs/screenshot.png" alt="Favicon Snatcher main window" width="640" /></p>
-->

## Features

- **Paste, type or drag in URLs.** Enter one site or several separated by commas or new lines. `example.com` works without the `https://`.
- **Finds every icon a page declares.** Scans the page's `<link rel="icon">` tags (and any linked `.ico` files) and downloads them all.
- **Preview before saving.** Icons are shown as thumbnails grouped by format, with a Details view that lists each source URL.
- **Save one, some or all.** Double-click an icon to save it, or check several and use **Save… → Selected** or **Save… → All** to export them to a folder.

## Usage

1. Launch Raccoonus Favicon Snatcher.
2. Paste a website address into the box (or use the clipboard button) and click **Snatch!**.
3. Right-click the results to switch views, clear the list, or save icons.

## Building from source

Requirements: Windows and the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```powershell
git clone https://github.com/EveAeternam/Raccoonus.git
cd Raccoonus
dotnet run --project FaviconSnatcher
```

To produce a standalone build:

```powershell
dotnet publish FaviconSnatcher -c Release -o publish
```

You can also open `FaviconSnatcher.slnx` in Visual Studio 2026.

Every push to `main` is built by [GitHub Actions](.github/workflows/build.yml), and the compiled app is attached to each run as a downloadable artifact.

## Project layout

```text
FaviconSnatcher/
├── Program.cs               Entry point
├── MainWindow.cs            URL parsing, favicon discovery, download and export
├── MainWindow.Designer.cs   WinForms layout
├── Properties/              Resources and settings
├── Resources/               Toolbar icons and the Raccoonus mascot
└── racoonus.ico             App icon
```

## How it works

The app downloads the page's HTML, looks for `<link>` tags whose `rel` contains `icon` or whose `href` ends in `.ico`, and resolves each one against the page URL. If any `.ico` files are found those are preferred; otherwise every other declared icon (such as PNG) is fetched. SVG icons are skipped.

Sites that only serve `/favicon.ico` without declaring it in their HTML aren't picked up yet.

## License

[MIT](LICENSE) © 2026 Evangeline "Evie" Minet
