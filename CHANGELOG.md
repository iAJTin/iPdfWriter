# Changelog

All notable changes to this project will be documented in this file.

## 1.0.3 - 

### Fixes

 - Fixes an error that occurred when trying to load a style from file and this file does not have any extension.

### Added

 1. Add support for **netstandard2.1** 
 
   - Add **SplitEnumerator** ref struct.
   
   - Add support for the use of the **~** character in the **iTin.Core.IO** library

 2. Add sample project for **net60**

 3. Added the ability to create a table from a typed enumerable and from a datatable 
    To do this, two new methods have been added to the **PdfTable** static class.
    Adding **custom styles** for the table is **currently under development**
     
	| Method | Description |
	|:------|:----------|
	| PdfTable.CreateFromEnumerable(..) | please see [sample16.cs]|
	| PdfTable.CreateFromDataTable(..) | please see [sample17.cs] |

    Here is how to use them.

	##### CreateFromEnumerable

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            Style = TableStylesDictionary["Table"],
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
            Table = PdfTable.CreateFromEnumerable(
                new List<Person>
                {
                    new Person {Name = "Name-01", Surname = "Surname-01"},
                    new Person {Name = "Name-02", Surname = "Surname-02"},
                    new Person {Name = "Name-03", Surname = "Surname-03"},
                    new Person {Name = "Name-04", Surname = "Surname-04"},
                    new Person {Name = "Name-05", Surname = "Surname-05"},
                    new Person {Name = "Name-06", Surname = "Surname-06"},
                    new Person {Name = "Name-07", Surname = "Surname-07"},
                    new Person {Name = "Name-08", Surname = "Surname-08"},
                    new Person {Name = "Name-09", Surname = "Surname-09"},
                    new Person {Name = "Name-10", Surname = "Surname-10"},
                })
        }));
    ```

	##### CreateFromDataTable

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            Style = PdfTableStyle.Default,
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
            Table = PdfTable.CreateFromDataTable(
                new List<Person>
                {
                    new Person {Name = "Name-01", Surname = "Surname-01"},
                    new Person {Name = "Name-02", Surname = "Surname-02"},
                    new Person {Name = "Name-03", Surname = "Surname-03"},
                    new Person {Name = "Name-04", Surname = "Surname-04"},
                    new Person {Name = "Name-05", Surname = "Surname-05"},
                    new Person {Name = "Name-06", Surname = "Surname-06"},
                    new Person {Name = "Name-07", Surname = "Surname-07"},
                    new Person {Name = "Name-08", Surname = "Surname-08"},
                    new Person {Name = "Name-09", Surname = "Surname-09"},
                    new Person {Name = "Name-10", Surname = "Surname-10"},
                }
                .ToDataTable<Person>("People"))
        }));
    ```

### Changed

  - The way to render the replacements has been rewritten to achieve higher processing speeds, now we should notice an improvement in general times when processing a file.
  
  - Renamed **TextOffet**, **ImageOffset** and **TableOffset** properties for **Offset** property. 
  
	##### with text

    ```csharp   
	// Before
    doc.Replace(new ReplaceText(
		new WithTextObject
		{
			Text = "#TITLE#",
			NewText = "Lorem ipsum",
			UseTestMode = useTestMode,
			TextOffset = PointF.Empty,
			ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
			Style = (PdfTextStyle) PdfTextStyle.LoadFromFile("~Resources/Sample-18/Styles/TextStyle.json", format: KnownFileFormat.Json)
		}));

	// Now
    doc.Replace(new ReplaceText(
		new WithTextObject
		{
			Text = "#TITLE#",
			NewText = "Lorem ipsum",
			UseTestMode = useTestMode,
			Offset = PointF.Empty,
			ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
			Style = (PdfTextStyle) PdfTextStyle.LoadFromFile("~Resources/Sample-18/Styles/TextStyle.json", format: KnownFileFormat.Json)
		}));
    ```

	##### with image

    ```csharp   
	// Before
	doc.Replace(new ReplaceText(
		new WithImageObject
		{
			Text = "#BAR-CHART#",
			UseTestMode = useTestMode,
			ImageOffset = PointF.Empty,
			Style = ImagesStylesTable["Default"],
			ReplaceOptions = ReplaceTextOptions.Default,
			Image = PdfImage.FromFile("~Resources/Sample-18/Images/bar-chart.png")
		}));

	// Now
    doc.Replace(new ReplaceText(
		new WithTextObject
		{
			Text = "#BAR-CHART#",
			UseTestMode = useTestMode,
			Offset = PointF.Empty,
			Style = ImagesStylesTable["Default"],
			ReplaceOptions = ReplaceTextOptions.Default,
			Image = PdfImage.FromFile("~Resources/Sample-18/Images/bar-chart.png")
		}));
    ```

	##### with table

    ```csharp   
	// Before
    doc.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            UseTestMode = useTestMode,
            TableOffset = PointF.Empty,
            Style = PdfTableStyle.Default,
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
            Table = PdfTable.CreateFromHtml(GenerateHtmlDatatable())
        }));

	// Now
    doc.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            Style = PdfTableStyle.Default,
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
            Table = PdfTable.CreateFromHtml(GenerateHtmlDatatable())
        }));
    ```

  - Library versions for this version
  
	| Library | Version | Description |
	|:------|:------|:----------|
	| iTin.Core | **2.0.0.4** | Base library containing various extensions, helpers, common constants |
	| iTin.Core.Drawing | **1.0.0.2** | Drawing objects, extension, helpers, common constants |
	| iTin.Core.IO | **1.0.0.2** | Common I/O calls |
	| iTin.Core.IO.Compression | **1.0.0.2** | Compression library |
	| iTin.Core.Models | **1.0.0.2** | Data models base |
	| iTin.Core.Models.Design.Charting | **1.0.0.2** | Base charting models |
	| iTin.Core.Models.Design.Styling | **1.0.0.2** | Base styling models |
	| iTin.Logging | **1.0.0.1** | Logging library |
	| iTin.Registry.Windows | **1.0.0.2** | Windows registry access |
	| iTin.Utilities.Pdf.Design | **1.0.0.3** | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | **1.0.0.2** | Pdf Writer |


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

[sample16.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample16.cs
[sample17.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample17.cs
