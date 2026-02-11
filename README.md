# Raccoonus Favicon Snatcher

A Windows desktop application for downloading and managing favicons from websites.

## Application Details

| Property | Value |
|----------|-------|
| **Name** | Raccoonus Favicon Snatcher |
| **Version** | 1.0.0 |
| **Build Date** | 2026 |
| **Platform** | Windows |
| **Framework** | .NET 10 |
| **Language** | C# 14.0 |
| **Type** | Windows Forms (WinForms) Application |
| **Icon** | racoonus.ico |

## Description

Raccoonus Favicon Snatcher is a utility application designed for developers and web enthusiasts who need to download and manage favicon files from websites. The application provides an intuitive graphical interface to extract and view favicons from any web URL.

## Key Features

- **URL Processing**: Extract favicons from any website URL
- **Image Viewer**: View large favicon images with a dedicated viewer window
- **HTTP Support**: Download favicons using HTTP/HTTPS requests
- **File Management**: Organize and manage downloaded favicon files
- **Regular Expression Parsing**: Extract favicon references from HTML content

## Technical Stack

- **Framework**: .NET 10 with Windows Forms
- **Language**: C# 14.0
- **UI Framework**: Windows Forms (WinForms)
- **HTTP Client**: System.Net.Http
- **File I/O**: System.IO

## Project Structure

```
FaviconSnatcher/
├── Program.cs                 # Application entry point
├── MainWindow.cs             # Main application window logic
├── MainWindow.Designer.cs    # UI component design
├── Properties/
│   ├── Resources.resx        # Application resources
│   └── Settings.settings     # Application settings
├── FaviconSnatcher.csproj    # Project file
└── racoonus.ico              # Application icon
```

## System Requirements

- Windows operating system
- .NET 10 Runtime

## Getting Started

To build and run the application:

1. Ensure .NET 10 SDK is installed
2. Navigate to the project directory
3. Run `dotnet build` to compile
4. Run `dotnet run` to execute the application

## Development Notes

- The application uses .NET 10's latest features
- Nullable reference types are enabled for improved type safety
- Implicit using statements are enabled
- Windows Forms is configured for this Windows-only desktop application

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Last Updated**: 2026
