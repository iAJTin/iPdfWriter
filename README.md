<p align="center">
  <img src="https://github.com/iAJTin/iPdfWriter/blob/master/nuget/iPdfWriter.png" height="32"/>
</p>
<p align="center">
  <a href="https://github.com/iAJTin/iPdfWriter">
    <img src="https://img.shields.io/badge/iTin-iPdfWriter-green.svg?style=flat"/>
  </a>
</p>

***

# What is iPdfWriter?

**iPdfWriter** is a lightweight implementation that allows modifying a **pdf** document totally or partially by replacing tags.

The idea is to try to quickly and easily facilitate the task of filling in the 'typical' report file that the client wants to send by email with the data filled in from their erp, vertical application, custom development, etc... to which I am sure that you have faced each other at some point.

I hope it helps someone. :smirk:

# Install via NuGet

- From nuget gallery

<table>
  <tr>
    <td>
      <a href="https://github.com/iAJTin/iPdfWriter">
        <img src="https://img.shields.io/badge/-iPdfWriter-green.svg?style=flat"/>
      </a>
    </td>
    <td>
      <a href="https://www.nuget.org/packages/iPdfWriter/">
        <img alt="NuGet Version" 
             src="https://img.shields.io/nuget/v/iPdfWriter.svg" /> 
      </a>
    </td>  
  </tr>
</table>

- From package manager console

```PM> Install-Package iPdfWriter```

# Usage

## Samples

### Sample 1 - Shows the use of text and image replacement with styles


Basic steps, for more details please see [sample01.cs] file.

1. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-01/file-sample.pdf"
    };
    ```             
2. Define the styles to use
        
    **Text styles**
      
    ```csharp   
    private static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new Dictionary<string, PdfTextStyle>
    {
        "ReportTitle",
        new PdfTextStyle
        {
            Font =
            {
                Name = "Arial",
                Size = 28.0f,
                Bold = YesNo.Yes,
                Italic = YesNo.Yes,
                Color = "Blue"
            },
            Content =
            {
                Alignment =
                {
                    Vertical = KnownVerticalAlignment.Center,
                    Horizontal = KnownHorizontalAlignment.Center
                }
            }
        }
    };         
    ```
    **Image styles**
      
    ```csharp   
    private static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new Dictionary<string, PdfImageStyle>
    {           
        {
            "Default",
            new PdfImageStyle { Content = { Alignment = { Horizontal = KnownHorizontalAlignment.Left }}}
        }
    };                
    ```
3. Replace **#TITLE#** tag with another text

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTextObject
        {
            Text = "#TITLE#",
            NewText = "Lorem ipsum",
            Style = TextStylesTable["ReportTitle"],
            ReplaceOptions = ReplaceTextOptions.AccordingToMargins
        }));            
    ```

4. Replace **#BAR-CHART#** tag with an image

    ```csharp   
    using (var barGraph = PdfImage.FromFile("~/Resources/Sample-01/Images/bar-chart.png"))
    {
        doc.Replace(new ReplaceText(
            new WithImageObject
            {
                Text = "#BAR-CHART#",
                Style = ImagesStylesTable["Default"],
                ReplaceOptions = ReplaceTextOptions.Default,
                Image = barGraph
            }));
    }
    ```
5. Try to create pdf output result

     ```csharp   
     var result = doc.CreateResult();
     if (!result.Success)
     {
         // Handle errors                 
     }
     ```
6. Save pdf file result
 
    ```csharp   
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample01/Sample-01" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
7. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample01Page01][Sample01Page01] 

### Sample 2 - Shows the use of html table replacement in a pdf document

Basic steps, for more details please see [sample02.cs] file.

1. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-02/file-sample.pdf"
    };
    ```             
2. Define HTML table with embbebed styles

    ```csharp   
    private static string GenerateHtmlDatatable()
    {
        var result = new StringBuilder();
        result.AppendLine("<table border='1' cellspacing='0' cellpadding='6' style='width:100%'>");
        result.AppendLine(" <tbody>");
        result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
        result.AppendLine("    <td>&nbsp;</td>");
        result.AppendLine("    <td>Lorem ipsum</td>");
        result.AppendLine("    <td>Lorem ipsum</td>");
        result.AppendLine("    <td>Lorem ipsum</td>");
        result.AppendLine("  </tr>");
        result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
        result.AppendLine("    <td>1</td>");
        result.AppendLine("    <td>In eleifend velit vitae libero sollicitudin euismod.</td>");
        result.AppendLine("    <td>Lorem</td>");
        result.AppendLine("    <td>&nbsp;</td>");
        result.AppendLine("  </tr>");
        result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
        result.AppendLine("    <td>2</td>");
        result.AppendLine("    <td>Cras fringilla ipsum magna, in fringilla dui commodo a.</td>");
        result.AppendLine("    <td>Lorem</td>");
        result.AppendLine("    <td>&nbsp;</td>");
        result.AppendLine("  </tr>");
        result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
        result.AppendLine("    <td>3</td>");
        result.AppendLine("    <td>LAliquam erat volutpat.</td>");
        result.AppendLine("    <td>Lorem</td>");
        result.AppendLine("    <td>&nbsp;</td>");
        result.AppendLine("  </tr>");
        result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
        result.AppendLine("    <td>4</td>");
        result.AppendLine("    <td>Fusce vitae vestibulum velit. </td>");
        result.AppendLine("    <td>Lorem</td>");
        result.AppendLine("    <td>&nbsp;</td>");
        result.AppendLine("  </tr>");
        result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
        result.AppendLine("    <td>5</td>");
        result.AppendLine("    <td>Etiam vehicula luctus fermentum.</td>");
        result.AppendLine("    <td>Ipsum</td>");
        result.AppendLine("    <td>&nbsp;</td>");
        result.AppendLine(" </tr>");
        result.AppendLine(" </tbody>");
        result.AppendLine("</table>");

        return result.ToString();
    }
    ```             
3. Replace **#DATA-TABLE#** tag with a **HTML** table

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            TableOffset = PointF.Empty,
            Style = PdfTableStyle.Default,
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
            Table = PdfTable.CreateFromHtml(GenerateHtmlDatatable())
        }));
    ```
4. Try to create pdf output result

     ```csharp   
     var result = doc.CreateResult();
     if (!result.Success)
     {
         // Handle errors                 
     }
     ```
5. Save pdf result to file
    
    ```csharp   
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample02/Sample-02" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
6. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample02Page02][Sample02Page02] 

### Sample 3 - Shows the use of merge action

Basic steps, for more details please see [sample03.cs] file.

1. Load pdf pages 

    **Page 1**
     
    ```csharp   
    var page1 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-03/file-sample-1.pdf"
    };
    ```             
    **Page 2**
     
    ```csharp   
    var page2 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-03/file-sample-2.pdf"
    };
    ```             
    **Page 3**
     
    ```csharp   
    var page3 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-03/file-sample-3.pdf"
    };
    ```             
    **Page 4**
     
    ```csharp   
    var page4 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-03/file-sample-4.pdf"
    };
    ```             

2. Replace Tags

    Replace the elements in the pages, for reasons of space I omit this step,
    We would do it as we have seen in examples 1 and 2.

3. Create a list of elements to merge

    Note that you can set the order in which they will be merged.

    ```csharp   
    var files = new PdfObject
    {
        Items = new List<PdfInput>
        {
            new PdfInput {Index = 0, Input = page1},
            new PdfInput {Index = 1, Input = page2},
            new PdfInput {Index = 2, Input = page3},
            new PdfInput {Index = 3, Input = page4}
        }
    };
    ```
4. Try to merge into a pdf output result

     ```csharp   
     var mergeResult = files.TryMergeInputs();
     if (!mergeResult.Success)
     {
         // Handle errors                 
     }
     ```
5. Save merged result to file
    
    ```csharp   
    var saveResult =  mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample03/Sample-03" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
6. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample03AllPages][Sample03AllPages] 

### Sample 4 - Shows use global replacements

On many occasions we must make repetitive replacements such as the header or footer, or any other text or image across the entire report.

For these cases we have global replacements.

Basic steps, for more details please see [sample04.cs] file.

1. Load pdf pages 

    **Page 1**
     
    ```csharp   
    var page1 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-04/file-sample-1.pdf"
    };
    ```             
    **Page 2**
     
    ```csharp   
    var page2 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-04/file-sample-2.pdf"
    };
    ```             
    **Page 3**
     
    ```csharp   
    var page3 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-04/file-sample-3.pdf"
    };
    ```             
    **Page 4**
     
    ```csharp   
    var page4 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-04/file-sample-4.pdf"
    };
    ```             
2. Define the styles to use
      
    ```csharp   
    private static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new Dictionary<string, PdfTextStyle>
    {
        "Header",
        new PdfTextStyle
        {
            Font =
            {
                Name = "Verdana",
                Size = 8.0f,
                Bold = YesNo.Yes,
                Color = "Gray"
            },
            Content =
            {
                Alignment =
                {
                    Vertical = KnownVerticalAlignment.Center,
                    Horizontal = KnownHorizontalAlignment.Left
                }
            }
        },
        // Other tag styles here!!
    };             
    ```
3. Replace Tags

    Replace the elements in the pages, for reasons of space I omit this step,
    We would do it as we have seen in examples 1 and 2.

4. Create a special object **GlobalReplacementsCollection**

    ```csharp   
    var globalReplacements = new GlobalReplacementsCollection
    {
        new WithTextObject
        {
            Text = "#HEADER-TEXT#",
            NewText = "Report Name - Lorem ipsum dolor",
            Style = TextStylesTable["Header"],
            ReplaceOptions = ReplaceTextOptions.FromLeftMarginToNextElement
        }
    };
    ```
5. Create a list of elements to merge

    Note that you can set the order in which they will be merged.

    ```csharp   
    var files = new PdfObject(new PdfObjectConfig { GlobalReplacements = globalReplacements })
    {
        Items = new List<PdfInput>
        {
            new PdfInput {Index = 0, Input = page1},
            new PdfInput {Index = 1, Input = page2},
            new PdfInput {Index = 2, Input = page3},
            new PdfInput {Index = 3, Input = page4}
        }
    };
    ```
6. Try to merge into a pdf output result

     ```csharp   
     var mergeResult = files.TryMergeInputs();
     if (!mergeResult.Success)
     {
         // Handle errors                 
     }
     ```
7. Save merged result to file
    
    ```csharp   
    var saveResult =  mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample04/Sample-04" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
8. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample04AllPages][Sample04AllPages] 

### Sample 5 - Shows use System tags

Another repetitive task when we create or modify reports is the page numbers to try to speed up this, the **SystemTags** appear.

Currently we natively have the PageNumberSystemTag and TotalPagesSystemTag objects for this purpose.

Remember that you can create the ones you need, for this you have to implement the **ISystemTag** interface

Basic steps, for more details please see [sample05.cs] file.

1. Load pdf pages 

    **Page 1**
     
    ```csharp   
    var page1 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-05/file-sample-1.pdf"
    };
    ```             
    **Page 2**
     
    ```csharp   
    var page2 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-05/file-sample-2.pdf"
    };
    ```             
    **Page 3**
     
    ```csharp   
    var page3 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-05/file-sample-3.pdf"
    };
    ```             
    **Page 4**
     
    ```csharp   
    var page4 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-05/file-sample-4.pdf"
    };
    ```             
2. Define the styles to use
      
    ```csharp   
    private static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new Dictionary<string, PdfTextStyle>
    {
        "Header",
        new PdfTextStyle
        {
            Font =
            {
                Name = "Verdana",
                Size = 8.0f,
                Bold = YesNo.Yes,
                Color = "Gray"
            },
            Content =
            {
                Alignment =
                {
                    Vertical = KnownVerticalAlignment.Center,
                    Horizontal = KnownHorizontalAlignment.Left
                }
            }
        },
        "PageNumber",
        new PdfTextStyle
        {
            Font =
            {
                Name = "Verdana",
                Size = 8.0f,
                Bold = YesNo.No,
                Color = "Gray"
            },
            Content =
            {
                Alignment =
                {
                    Vertical = KnownVerticalAlignment.Center,
                    Horizontal = KnownHorizontalAlignment.Right
                }
            }
        },
        // Other tag styles here!!
    };             
    ```
3. Replace Tags

    Replace the elements in the pages, for reasons of space I omit this step,
    We would do it as we have seen in examples 1 and 2.

4. Create a special object **GlobalReplacementsCollection**

    ```csharp   
    var globalReplacements = new GlobalReplacementsCollection
    {
        new WithTextObject
        {
            Text = "#HEADER-TEXT#",
            NewText = "Report Name - Lorem ipsum dolor",
            Style = TextStylesTable["Header"],
            ReplaceOptions = ReplaceTextOptions.FromLeftMarginToNextElement
        }
    };
    ```
5. Create a special object **SystemTagsCollection**

    ```csharp   
    var systemTags = new SystemTagsCollection
    {
        new PageNumberSystemTag
        {
            TextOffset = PointF.Empty,
            Style = TextStylesTable["PageNumber"],
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
        }
    };
    ```
6. Create a list of elements to merge

    Note that you can set the order in which they will be merged.

    ```csharp   
    var files = new PdfObject(new PdfObjectConfig { Tags = systemTags, GlobalReplacements = globalReplacements })
    {
        Items = new List<PdfInput>
        {
            new PdfInput {Index = 0, Input = page1},
            new PdfInput {Index = 1, Input = page2},
            new PdfInput {Index = 2, Input = page3},
            new PdfInput {Index = 3, Input = page4}
        }
    };
    ```
7. Try to merge into a pdf output result

     ```csharp   
     var mergeResult = files.TryMergeInputs();
     if (!mergeResult.Success)
     {
         // Handle errors                 
     }
     ```
8. Save merged result to file
    
    ```csharp   
    var saveResult =  mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample05/Sample-05" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
9. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample05AllPages][Sample05AllPages] 

### Sample 6 - Shows use the test mode

The **test mode** represents a special mode for use at **development time** to see if the changes are adjusted correctly.

During the test mode, both in the case of text and table, it appears with a **red** outline, in the case of images, 
it is not drawn and the outline also appears in **red**.

Basic steps, for more details please see [sample06.cs] file.

1. Load pdf pages 

    **Page 1**
     
    ```csharp   
    var page1 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-06/file-sample-1.pdf"
    };
    ```             
    **Page 2**
     
    ```csharp   
    var page2 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-06/file-sample-2.pdf"
    };
    ```             
    **Page 3**
     
    ```csharp   
    var page3 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-06/file-sample-3.pdf"
    };
    ```             
    **Page 4**
     
    ```csharp   
    var page4 = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-06/file-sample-4.pdf"
    };
    ```
2. Define styles

    Please see sample 1.

3. Replace Tags

    **Page 1**

    ```csharp   
    page1.Replace(new ReplaceText(
        new WithTextObject
        {
            Text = "#TITLE#",
            NewText = "Lorem ipsum",
            UseTestMode = YesNo.Yes,
            Style = TextStylesTable["ReportTitle"],
            ReplaceOptions = ReplaceTextOptions.AccordingToMargins
        }));            
    ```

    ```csharp   
    using (var barGraph = PdfImage.FromFile("~/Resources/Sample-06/Images/bar-chart.png"))
    {
        page1.Replace(new ReplaceText(
            new WithImageObject
            {
                Text = "#BAR-CHART#",
                UseTestMode = YesNo.Yes,
                Style = ImagesStylesTable["Default"],
                ReplaceOptions = ReplaceTextOptions.Default,
                Image = barGraph
            }));
    }
    ```
    **Page 2**

    ```csharp   
    page2.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            UseTestMode = YesNo.Yes,
            TableOffset = PointF.Empty,
            Style = PdfTableStyle.Default,
            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement,
            Table = PdfTable.CreateFromHtml(GenerateHtmlDatatable(), config: new PdfTableConfig { HeightStrategy = TableHeightStrategy.Exact })
        }));
    ```
    **Page 3**

    Nothing to do

    **Page 4**

    ```csharp   
    using (var image = PdfImage.FromFile("~/Resources/Sample-06/Images/image-1.jpg"))
    {
        page4.Replace(new ReplaceText(
            new WithImageObject
            {
                Text = "#IMAGE1#",
                UseTestMode = YesNo.Yes,
                ImageOffset = PointF.Empty,
                Style = PdfImageStyle.Default,
                ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                Image = image
            }));
    }
    ```
4. Create a special object **GlobalReplacementsCollection**

    ```csharp   
    var globalReplacements = new GlobalReplacementsCollection
    {
        new WithTextObject
        {
            Text = "#HEADER-TEXT#",
            NewText = "Report Name - Lorem ipsum dolor",
            Style = TextStylesTable["Header"],
            ReplaceOptions = ReplaceTextOptions.FromLeftMarginToNextElement
        }
    };
    ```
5. Create a special object **SystemTagsCollection**

    ```csharp   
    var systemTags = new SystemTagsCollection
    {
        new PageNumberSystemTag
        {
            TextOffset = PointF.Empty,
            Style = TextStylesTable["PageNumber"],
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
        }
    };
    ```
6. Create a list of elements to merge

    Note that you can set the order in which they will be merged.

    ```csharp   
    var files = new PdfObject(new PdfObjectConfig { Tags = systemTags, GlobalReplacements = globalReplacements })
    {
        Items = new List<PdfInput>
        {
            new PdfInput {Index = 0, Input = page1},
            new PdfInput {Index = 1, Input = page2},
            new PdfInput {Index = 2, Input = page3},
            new PdfInput {Index = 3, Input = page4}
        }
    };
    ```
7. Try to merge into a pdf output result

     ```csharp   
     var mergeResult = files.TryMergeInputs();
     if (!mergeResult.Success)
     {
         // Handle errors                 
     }
     ```
8. Save merged result to file
    
    ```csharp   
    var saveResult =  mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample06/Sample-06" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
9. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample06AllPages][Sample06AllPages] 

# Documentation

 - For **Writer** code documentation, please see next link [documentation].

# How can I send feedback!!!

If you have found **iPdfWriter** useful at work or in a personal project, I would love to hear about it. If you have decided not to use **iPdfWriter**, please send me and email stating why this is so. I will use this feedback to improve **iPdfWriter** in future releases.

My email address is 

![email.png][email] 

[email]: ./assets/email.png "email"
[documentation]: ./documentation/iTin.Utilities.Pdf.Writer.md
[Test samples]: https://github.com/iAJTin/iPdfWriter/tree/v1.0.2/src/test/iPdfWriter.ConsoleAppCore

[sample01.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/iPdfWriter.ConsoleAppCore/Code/Sample01.cs
[Sample01Page01]: ./assets/samples/sample1/page1.png "sample01 - page01"

[sample02.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/iPdfWriter.ConsoleAppCore/Code/Sample02.cs
[Sample02Page02]: ./assets/samples/sample2/page2.png "sample02 - page02"

[sample03.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/iPdfWriter.ConsoleAppCore/Code/Sample03.cs
[Sample03AllPages]: ./assets/samples/sample3/mergeresult.png "sample03 - merge"

[sample04.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/iPdfWriter.ConsoleAppCore/Code/Sample04.cs
[Sample04AllPages]: ./assets/samples/sample4/globalreplacements.png "sample04 - global header"

[sample05.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/iPdfWriter.ConsoleAppCore/Code/Sample05.cs
[Sample05AllPages]: ./assets/samples/sample5/systemtags.png "sample05 - system tags - page numbers"

[sample06.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/iPdfWriter.ConsoleAppCore/Code/Sample06.cs
[Sample06AllPages]: ./assets/samples/sample6/testmode.png "sample06 - test mode"
