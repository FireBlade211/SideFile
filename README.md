# SideFile
A side-by-side file manager.<br><br>

SideFile is still in beta, so bugs may occur, and you may find unfinished parts of the application.

## Versions
SideFile comes in 2 versions:
- **SideFile**<br>
  The modern version, built in WinUI. This version still hasn't started development, so there are no installation instructions.
- **SideFile Classic**<br>
  A classic version made using WinForms. This is the current focus, until it gets finished and development moves to modern **SideFile**.

## Installation
<!-- technically not installation since there's no installer but heck you -->

Download the latest ZIP file for your architecture from the [Releases](https://github.com/fireblade211/sidefile/releases) page (e.g., x64.zip) and run the executable inside.

## Development notes
### Why does SideFile Classic use the RCW version of WinForms-Ribbon?
**SideFile Classic** uses the Runtime-Callable Wrapper version of [WinForms-Ribbon](https://github.com/harborsiem/winforms-ribbon) because the COM-Callable Wrapper (CCW) version currently causes an exception to be thrown in `OnHandleDestroyed` when exiting the designer.

## Using SideFile Classic on Windows 7
I originally thought that .NET 10 meant the app could run only on Windows 10/11, however turns out you can run the app on Windows 7 with some elbow grease. This took me a few hours to find out, but here's a guide:

> [!NOTE]
> Skip any installation steps if the update mentioned is already installed.
<!-- update catalog search links are provided because there are different update releases for different architectures -->

1. Install [SP1](https://www.catalog.update.microsoft.com/Search.aspx?q=KB976932).
2. Install the [April 2015 Servicing Stack update](https://support.microsoft.com/en-us/topic/april-2015-servicing-stack-update-for-windows-7-and-windows-server-2008-r2-437ee749-51e0-6265-be9e-73fbd115d315).
3. Install the [Convenience Rollup update](https://catalog.update.microsoft.com/search.aspx?q=kb3125574).
4. Install the *Microsoft Visual C++ Redistributable 2015-2022* ([x64](https://download.visualstudio.microsoft.com/download/pr/40b59c73-1480-4caf-ab5b-4886f176bf71/D62841375B90782B1829483AC75695CCEF680A8F13E7DE569B992EF33C6CD14A/VC_redist.x64.exe) / [x86](https://download.visualstudio.microsoft.com/download/pr/40b59c73-1480-4caf-ab5b-4886f176bf71/435A0DE411B991E2BFC7FD1D5439639E7B32206960D3099370E36172018F52FE/VC_redist.x86.exe)) (*i know, this is a C# app, but it's needed anyway*)
5. Install the [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0/runtime).
6. Follow the steps in the [Installation](#installation) section to run the app.
