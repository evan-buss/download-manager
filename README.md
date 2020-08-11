# download-manager
Self hosted application that handles a variety of file downloads.

![.NET Core](https://github.com/evan-buss/download-manager/workflows/.NET%20Core/badge.svg?branch=master)

## Intial Features
- Initial support for MEGA.NZ downloads
- Automatic base64 decoding of URLS
- Download management via web interface
- Live download progress updates
- Deploy via Docker container


## Possible Features
- Plugin support for different download types
  - Torrent
  - Website downloads via browser automation
  - etc.
- Browser extension to quickly add new downloads and monitor progress
- Advanced file management
  - Renaming
  - Automatically move files on completion
- VPN management
  - Automatically switch VPN locations when we get a download error (509 over quota)
  - Possibly exposed via a named pipe to the container
- Email on download completion

## Technology
- Angular 10
- Tailwind CSS
- .Net Core 3.1
  - Dapper
  - EF Core
  - SQLite
  - SignalR
