# Changelog

All notable changes to this project will be documented in this file.

## 1.0.5 - 

### Added

 1. Add sample25, shows how to create a **PdfInput** from HTML code.

    ```csharp   
    // Creates pdf input from HTML
    var doc = PdfInput.CreateFromHtml(
        html: @"
        <table border='1' cellspacing='0' cellpadding='6' style='width:100%'>
          <tbody>
            <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
              <td>&nbsp;</td>
              <td>Lorem ipsum</td>
              <td>Lorem ipsum</td>
              <td>Lorem ipsum</td>
            </tr>
            <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
              <td>1</td>
              <td>In eleifend velit vitae libero sollicitudin euismod.</td>
              <td>Lorem</td>
              <td>&nbsp;</td>
            </tr>
            <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
              <td>2</td>
              <td>Cras fringilla ipsum magna, in fringilla dui commodo a.</td>
              <td>Lorem</td>
              <td>&nbsp;</td>
            </tr>
            <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
              <td>3</td>
              <td>LAliquam erat volutpat.</td>
              <td>Lorem</td>
              <td>&nbsp;</td>
            </tr>
            <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
              <td>4</td>
              <td>Fusce vitae vestibulum velit. </td>
              <td>Lorem</td>
              <td>&nbsp;</td>
            </tr>
            <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
              <td>5</td>
              <td>Etiam vehicula luctus fermentum.</td>
              <td>Ipsum</td>
              <td>&nbsp;</td>
            </tr>
          </tbody>
        </table>");

    // Create output result
    var result = doc.CreateResult();
    if (!result.Success)
    {
        // Handle errors
    }

    // Saves output result
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/SampleHtml/Sample-Html" });
    if (!saveResult.Success)
    {
        // Handle errors
    }
    ```

 2. Add sample26, shows how to add or modify pdf metadata information
 
    ```csharp   
    // Creates pdf file reference
    var doc = new PdfInput
    {
        Input = "~/Resources/Sample-26/file-sample.pdf"
    };

    // Set pdf metadata information
    doc
        .Set(new SetCreator { Value = "iPdfWriter" })
        .Set(new SetTitle { Value = "Hello from iPdfWriter" })
        .Set(new SetSubject { Value = "Subject changed from iPdfWriter" })
        .Set(new SetAuthor { Value = "iPdfWriter" })
        .Set(new SetKeywords { Value = "Samples, iPdfWriter, pdf" });

    // Create output result
    var result = doc.CreateResult();
    if (!result.Success)
    {
        // Handle errors
    }

    // Saves output result
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample26/Sample-26" });
    if (!saveResult.Success)
    {
        // Handle errors
    }
    ```

### Changed

  - Library versions for this version
  
	| Library | Version | Description |
	|:------|:------|:----------|
	| iTin.Core | **2.0.0.5** | Base library containing various extensions, helpers, common constants |
	| iTin.Core.Drawing | 1.0.0.2 | Drawing objects, extension, helpers, common constants |
    | iTin.Core.Hardware.Common | 1.0.0.3 | Common Hardware Infrastructure |
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font | 1.0.0.0 | Linux Hardware Infrastructure |
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font | 1.0.0.0 | MacOS Hardware Infrastructure |
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font | 1.0.0.0 | Windows Hardware Infrastructure |
	| iTin.Core.IO | 1.0.0.2 | Common I/O calls |
	| iTin.Core.IO.Compression | 1.0.0.2 | Compression library |
    | iTin.Core.Interop.Shared | 1.0.0.2 | Generic Shared Interop Definitions |
    | iTin.Core.Interop.Windows.Devices | 1.0.0.0 | Win32 Generic Interop Calls |
	| iTin.Core.Models | 1.0.0.2 | Data models base |
	| iTin.Core.Models.Design.Charting | 1.0.0.2 | Base charting models |
	| iTin.Core.Models.Design.Styling | 1.0.0.2 | Base styling models |
	| iTin.Logging | 1.0.0.1 | Logging library |
    | iTin.Hardware.Abstractions.Devices.Graphics.Font | 1.0.0.0 | Generic Common Hardware Abstractions |
	| iTin.Registry.Windows | 1.0.0.2 | Windows registry access |
	| iTin.Utilities.Pdf.Design | **1.0.0.5** | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | **1.0.0.6** | Pdf Writer |


## [1.0.4] - 2022-08-04

### Added

 1. Add **TextLines** method to **PdfInput** class, for get the lines of text for an **PdfInput**, optionally you can set both the start and end pages and a value indicating whether blank lines are included in the result or uses a predicate for filtering
 
    ```csharp   
    // Creates pdf file reference
    var doc = new PdfInput
    {
        Input = "~/Resources/Sample-24/file-sample.pdf"
    };

    // Extract text lines (Remove empty lines)
    try
    {
        var textLines = doc.TextLines();

        logger.Info("   > Document lines (Remove empty lines)");
        logger.Info($"     > Count: {textLines.Count()}");
    }
    catch
    {
        logger.Info("   > Error while extract text lines");
    }

    // Extract text lines (Include empty lines)
    try
    {
        var textLines = doc.TextLines(removeEmptyLines: false);

        logger.Info("   > Document lines (Include empty lines)");
        logger.Info($"     > Count: {textLines.Count()}");
    }
    catch
    {
        logger.Info("   > Error while extract text lines");
    }

    // Extract text lines (predicate)
    try
    {
        var textLines = doc.TextLines(line => line.Text.Trim() == "#TITLE#");

        logger.Info("   > Document lines (Predicate)");
        logger.Info($"     > Count: {textLines.Count()}");
    }
    catch
    {
        logger.Info("   > Error while extract text lines");
    }
    ```

### Changed

  - Library versions for this version
  
	| Library | Version | Description |
	|:------|:------|:----------|
	| iTin.Core | 2.0.0.4 | Base library containing various extensions, helpers, common constants |
	| iTin.Core.Drawing | 1.0.0.2 | Drawing objects, extension, helpers, common constants |
    | iTin.Core.Hardware.Common | 1.0.0.3 | Common Hardware Infrastructure |
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font | 1.0.0.0 | Linux Hardware Infrastructure |
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font | 1.0.0.0 | MacOS Hardware Infrastructure |
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font | 1.0.0.0 | Windows Hardware Infrastructure |
	| iTin.Core.IO | 1.0.0.2 | Common I/O calls |
	| iTin.Core.IO.Compression | 1.0.0.2 | Compression library |
    | iTin.Core.Interop.Shared | 1.0.0.2 | Generic Shared Interop Definitions |
    | iTin.Core.Interop.Windows.Devices | 1.0.0.0 | Win32 Generic Interop Calls |
	| iTin.Core.Models | 1.0.0.2 | Data models base |
	| iTin.Core.Models.Design.Charting | 1.0.0.2 | Base charting models |
	| iTin.Core.Models.Design.Styling | 1.0.0.2 | Base styling models |
	| iTin.Logging | 1.0.0.1 | Logging library |
    | iTin.Hardware.Abstractions.Devices.Graphics.Font | 1.0.0.0 | Generic Common Hardware Abstractions |
	| iTin.Registry.Windows | 1.0.0.2 | Windows registry access |
	| iTin.Utilities.Pdf.Design | **1.0.0.4** | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | **1.0.0.3** | Pdf Writer |

## [1.0.3] - 2022-08-03

### Fixes

 - Fixes an error that occurred when trying to load a style from file and this file does not have any extension.

### Added

 1. Add support for **netstandard2.1** 
 
   - Add **SplitEnumerator** ref struct.
   
   - Add support for the use of the **~** character in the **iTin.Core.IO** library

   - **ByteReader** class rewritten to work with **Span** in net core projects.

 2. Add sample project for **net60**

 3. Added the ability to create a table from a typed enumerable and from a datatable 
    To do this, two new methods have been added to the **PdfTable** static class.
    There are two overloads for each method, one to render the table applying **ccs** styles, 
    this functionality is implemented and the other rendering the table natively, 
    it is **currently under development**
     
	| Method | Description |
	|:------|:----------|
	| PdfTable.CreateFromEnumerable(..) | please see [sample16.cs], [sample17.cs] |
	| PdfTable.CreateFromDataTable(..) | please see [sample18.cs], [sample19.cs] |

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


 4. Unify calls to handle font from file, currently this functionality is only available for Windows systems,
    The logic of each platform is in its own assembly *iTin.Core.Hardware.**Target-System**.Devices.Graphics.Font*.

    *Where:*

    **Target-System**, it can be *Linux*, *Windows* or *MacOS* and the platform independent logic is found in the 
    **iTin.Hardware.Abstractions.Devices.Graphics.Font** assembly, so that a call is made independent of the target platform and this 
    assembly has the responsibility of managing the final call to the platform destination.

 5. Add **ExtractPages** method to **PdfInput** class for extract pages from an input.

    ```csharp   
    // Creates pdf file reference
    var doc = new PdfInput
    {
        Input = "~/Resources/Sample-22/file-sample.pdf"
    };

    // Extract pages
    var partialInput = doc.ExtractPages(1, 2);

    // Create output result
    var result = partialInput.CreateResult();
    if (!result.Success)
    {
        // Handle errors
    }

    // Saves output result
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample22/Sample-22" });
    if (!saveResult.Success)
    {
        // Handle errors
    }
    ```

 6. Add **NumberOfPages** method to **PdfInput** class, for get number of pages of an input.
 
    ```csharp   
    // Creates pdf file reference
    var doc = new PdfInput
    {
        Input = "~/Resources/Sample-22/file-sample.pdf"
    };

    // Total pages of this reference document
    var pages = doc.NumberOfPages();
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
    | iTin.Core.Hardware.Common | **1.0.0.3** | Common Hardware Infrastructure |
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font | **1.0.0.0** | Linux Hardware Infrastructure |
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font | **1.0.0.0** | MacOS Hardware Infrastructure |
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font | **1.0.0.0** | Windows Hardware Infrastructure |
	| iTin.Core.IO | **1.0.0.2** | Common I/O calls |
	| iTin.Core.IO.Compression | **1.0.0.2** | Compression library |
    | iTin.Core.Interop.Shared | **1.0.0.2** | Generic Shared Interop Definitions |
    | iTin.Core.Interop.Windows.Devices | **1.0.0.0** | Win32 Generic Interop Calls |
	| iTin.Core.Models | **1.0.0.2** | Data models base |
	| iTin.Core.Models.Design.Charting | **1.0.0.2** | Base charting models |
	| iTin.Core.Models.Design.Styling | **1.0.0.2** | Base styling models |
	| iTin.Logging | **1.0.0.1** | Logging library |
    | iTin.Hardware.Abstractions.Devices.Graphics.Font | **1.0.0.0** | Generic Common Hardware Abstractions |
	| iTin.Registry.Windows | **1.0.0.2** | Windows registry access |
	| iTin.Utilities.Pdf.Design | **1.0.0.3** | Pdf design objects |
	| iTin.Utilities.Pdf.Writer | **1.0.0.2** | Pdf Writer |


## [1.0.2] - 2022-06-24

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

## [1.0.1] - 2022-06-23

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

## [1.0.0] - 2020-09-29

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

[1.0.5]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.4
[1.0.4]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.4
[1.0.3]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.3
[1.0.2]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.2
[1.0.1]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.1
[1.0.0]: https://github.com/iAJTin/iPdfWriter/releases/tag/v1.0.0

[sample16.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample16.cs
[sample17_.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample17.cs
[sample18.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample18.cs
[sample19_.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample19.cs
