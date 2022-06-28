﻿# Changelog

All notable changes to this project will be documented in this file.

## 1.0.3 - 

### Added

 - Add support for **net60**
   
   Add support for the use of the **~** character in the **iTin.Core.IO** library
 
### Changed

  - Library versions for this version
  
	| Library | Version | Description |
	|:------|:------|:----------|
	| iTin.Core | **2.0.0.4** | Base library containing various extensions, helpers, common constants |
	| iTin.Core.Drawing | 1.0.0.1 | Drawing objects, extension, helpers, common constants |
	| iTin.Core.IO | **1.0.0.2** | Common I/O calls |
	| iTin.Core.IO.Compression | 1.0.0.1 | Compression library |
	| iTin.Core.Models | 1.0.0.1 | Data models base |
	| iTin.Core.Models.Design.Charting | 1.0.0.1 | Base charting models |
	| iTin.Core.Models.Design.Styling | 1.0.0.1 | Base styling models |
	| iTin.Logging | 1.0.0.0 | Logging library |
	| iTin.Registry.Windows | 1.0.0.1 | Windows registry access |
	| iTin.Utilities.Pdf.Design | 1.0.0.2 | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | 1.0.0.1 | Pdf Writer |


## [1.0.2] - 2022-24-06

## Critical

- **Important!!!**

  **Fixes an error caused in the previous version, the nuget packages were not updated correctly when creating the version**

  **I'm sorry for the inconveniences...**

### Fixes

 - Upgrade Newtonsoft.Json nuget package to version 13.0.1 (without critical errors)

 - Fix an error that occurs when trying to create an html table with html5 tags (not supported)

### Changed
 
 - Change license type to GNU

 - Library versions for this version
  
	| Library | Version | Description |
	|:------|:------|:----------|
	| iTin.Core | 2.0.0.3 | Base library containing various extensions, helpers, common constants |
	| iTin.Core.Drawing | 1.0.0.1 | Drawing objects, extension, helpers, common constants |
	| iTin.Core.IO | 1.0.0.0 | Common I/O calls |
	| iTin.Core.IO.Compression | 1.0.0.1 | Compression library |
	| iTin.Core.Models | 1.0.0.1 | Data models base |
	| iTin.Core.Models.Design.Charting | 1.0.0.1 | Base charting models |
	| iTin.Core.Models.Design.Styling | 1.0.0.1 | Base styling models |
	| iTin.Logging | 1.0.0.0 | Logging library |
	| iTin.Registry.Windows | 1.0.0.1 | Windows registry access |
	| iTin.Utilities.Pdf.Design | **1.0.0.2** | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | 1.0.0.1 | Pdf Writer |

## [1.0.1] - 2022-23-06

### Added

 - Library documentation.
 
 - ```tools``` folder in solution root. Contains a script for update help md files.

### Changed
  
 - Changed **```IResultGeneric```** interface. Changed **```Value```** property name by **```Result```** (for code clarify).
 
       This change may have implications in your final code, it is resolved by changing Value to Result

 - Update result classes for support more scenaries.
 
 - Library versions for this version
  
	| Library | Version | Description |
	|:------|:------|:----------|
	| iTin.Core | **2.0.0.3** | Base library containing various extensions, helpers, common constants |
	| iTin.Core.Drawing | **1.0.0.1** | Drawing objects, extension, helpers, common constants |
	| iTin.Core.IO | **1.0.0.0** | Common I/O calls |
	| iTin.Core.IO.Compression | **1.0.0.1** | Compression library |
	| iTin.Core.Models | **1.0.0.1** | Data models base |
	| iTin.Core.Models.Design.Charting | **1.0.0.1** | Base charting models |
	| iTin.Core.Models.Design.Styling | **1.0.0.1** | Base styling models |
	| iTin.Logging | **1.0.0.0** | Logging library |
	| iTin.Registry.Windows | **1.0.0.1** | Windows registry access |
	| iTin.Utilities.Pdf.Design | **1.0.0.1** | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | **1.0.0.1** | Pdf Writer |

## [1.0.0] - 2020-29-09

### Added

 - Create project and first commit

 - Library versions for this version
  
	|Library|Version|Description|
	|:------|:------|:----------|
	|iTin.Core| 2.0.0 | Common calls |
	|iTin.Core.Drawing| 1.0.0 | Drawing calls |
	|iTin.Core.IO| 1.0.0 | Common I/O calls |
	|iTin.Core.IO.Compression| 1.0.0 | Compression library |
	|iTin.Core.Models| 1.0.0 | Data models base |
	|iTin.Core.Models.Design.Charting| 1.0.0 | Base charting models |
	|iTin.Core.Models.Design.Styling| 1.0.0 | Base styling models |
	|iTin.Logging| 1.0.0 | Logging library |
	|iTin.Registry.Windows| 1.0.0 | Windows registry access |
	|iTin.Utilities.Pdf.Design| 1.0.0 | Pdf design objects |
	|iTin.Utilities.Pdf.Writer| 1.0.0 | Pdf Writer |

[1.0.3]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.3
[1.0.2]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.2
[1.0.1]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.1
[1.0.0]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.0

