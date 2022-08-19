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

**iPdfWriter** is a lightweight implementation that allows modifying a **pdf** document totally or partially by replacing tags. **Currently only works on Windows**

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
            Offset = PointF.Empty,
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
            Offset = PointF.Empty,
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

To enable or disable test mode, the **UseTestMode** property is available on all repleceable elements.

Where:

| Value | Description |
|:------|:----------|
| YesNo.Yes | Activates the test mode |
| YesNo.No | Shows the applied change |

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
            Offset = PointF.Empty,
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
                Offset = PointF.Empty,
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
            UseTestMode = YesNo.Yes,
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
            Offset = PointF.Empty,
            UseTestMode = YesNo.Yes,
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

### Sample 7 - Shows the use of save as zip a pdf input (one pdf file)

 Sometimes the result of a **pdf** is too heavy to be able to send it by email or to save it on disk, etc...

 **iPdfWriter** provides the [OutputResultConfig.cs] class, where you can define the **zip file name** as well as 
 a property indicating whether it is to be compressed. 
 
 If the file name **is not** specified, a **random name** will be automatically generated, the [sample10.cs] file show how it works 
 and the following link [sample10.zip] shows the output.

 Basic steps, for more details please see [sample07.cs] file.

1. Try to create pdf output result

     ```csharp   
     var result = doc.CreateResult(new OutputResultConfig { Filename = "Sample-07.pdf", Zipped = true });
     if (!result.Success)
     {
         // Handle errors                 
     }
     ```
2. Save pdf file result
 
    ```csharp   
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample07/Sample-07" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
3. Output

    You can see the result following the following link [Sample07.zip].

### Sample 8 - Shows the use of save as zip a merged output (many files)

 **iPdfWriter** provides the class [PdfObjectConfig.cs], where you can indicate if the result of merging the different pdf files is going to be compressed (for more information, please see **AllowCompression** property), 
 you can also indicate a threshold (for more information, please see **CompressionThreshold** property), that indicates from what size it is going to be compressed, for example, 
 you can indicate that you want the result to be compressed if the result of the merge is greater than 2Mb Basic steps, 
 
 For more details please see [sample08.cs] file.

1. Create a list of elements to merge

    **Without CompressionThreshold**

    ```csharp   
    var files = new PdfObject(new PdfObjectConfig { Tags = systemTags, GlobalReplacements = globalReplacements, AllowCompression = true })
    {
        Items = new List<PdfInput>
        {
            new PdfInput {Index = 0, Input = page1},
            new PdfInput {Index = 1, Input = page2},
            new PdfInput {Index = 2, Input = page3},
            new PdfInput {Index = 3, Input = page4},
        }
    };
    ```
    **With CompressionThreshold**

    ```csharp   
    var files = new PdfObject(new PdfObjectConfig { Tags = systemTags, GlobalReplacements = globalReplacements, AllowCompression = true, CompressionThreshold = 2 })
    {
        Items = new List<PdfInput>
        {
            new PdfInput {Index = 0, Input = page1},
            new PdfInput {Index = 1, Input = page2},
            new PdfInput {Index = 2, Input = page3},
            new PdfInput {Index = 3, Input = page4},
        }
    };
    ```
2. Try to merge into a pdf output result

    ```csharp   
    var mergeResult = files.TryMergeInputs();
    if (!mergeResult.Success)
    {
         // Handle errors
    }
    ```

3. Save pdf file result
 
     **Without CompressionThreshold**

    ```csharp   
    var saveResult = mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample08/Sample-08" });
    if (!saveResult.Success)
    {
         // Handle errors
    }
    ```

    **With CompressionThreshold**

    ```csharp   
    var saveResult = mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample09/Sample-09" });
    if (!saveResult.Success)
    {
         // Handle errors
    }
    ```
4. Output

    You can see the result (**Without CompressionThreshold**) following the following link [Sample08.zip] or you can see the result (**With CompressionThreshold**) following the following link [Sample09.zip].

### Sample 9 - Shows the use of how serialize and deserialize text, image and table styles

 Ok, it's nice to be able to use styles, but in the end if we need to modify the color, or the font, etc..., it would be good if it was static!!!

 Well, **iPdfWriter** provides a mechanism to be able to serialize and deserialize any style, supported formats are **XML** and **Json**, 
 you can decide this by using a parameter when saving the style. 
 
 By default it is saved in **XML** format.
 
 For more details please see [sample12.cs] file.

1. Create styles

    **PdfImageStyle**

    ```csharp   
    var imageStyle = new PdfImageStyle
    {
        Name = "ImageStyle",
        Borders =
        {
            new BaseBorder {Color = "Red", Show = YesNo.Yes, Position = KnownBorderPosition.Right},
            new BaseBorder {Color = "Yellow", Show = YesNo.Yes, Position = KnownBorderPosition.Top}
        },
        Content =
        {
            Color = "Blue",
            Alignment =
            {
                Horizontal = KnownHorizontalAlignment.Right
            },
            Properties = new Properties
            {
                new Property {Name = "p001", Value = "v001"},
                new Property {Name = "p002", Value = "v002"}
            }
        }
    };
    ```
    **PdfTextStyle**

    ```csharp   
    var textStyle = new PdfTextStyle
    {
        Name = "NormalStyle",
        Font =
        {
            Bold = YesNo.Yes,
            Italic = YesNo.Yes,
            Color = "Yellow",
            Underline = YesNo.No
        },
        Borders =
        {
            new BaseBorder {Color = "Red", Show = YesNo.Yes, Position = KnownBorderPosition.Right},
            new BaseBorder {Color = "Yellow", Show = YesNo.Yes, Position = KnownBorderPosition.Top}
        },
        Content =
        {
            Color = "Blue",
            Alignment =
            {
                Vertical = KnownVerticalAlignment.Top,
                Horizontal = KnownHorizontalAlignment.Right
            },
            Properties = new Properties
            {
                new Property {Name = "p001", Value = "v001"},
                new Property {Name = "p002", Value = "v002"}
            }
        }
    };
    ```
    **PdfTableStyle**

    ```csharp   
    var tableStyle = new PdfTableStyle
    {
        Name = "NormalStyle",
        Alignment =
        {
            Vertical = KnownVerticalAlignment.Top
        },
        Borders =
        {
            new BaseBorder {Color = "Red", Show = YesNo.Yes, Position = KnownBorderPosition.Right},
            new BaseBorder {Color = "Yellow", Show = YesNo.Yes, Position = KnownBorderPosition.Top}
        },
        Content =
        {
            Color = "Blue",
            Show = YesNo.Yes,
            Properties = new Properties
            {
                new Property {Name = "p001", Value = "v001"},
                new Property {Name = "p002", Value = "v002"}
            }
        }
    };
    ```
2. Try to save styles

    **PdfImageStyle**
    ***

    **XML**
    ```csharp   
    var imageStyleAsXmlResult = imageStyle.SaveToFile("~/Output/Sample12/ImageStyle");
    if (!imageStyleAsXmlResult.Success)
    {
         // Handle errors
    }
    ```

    **Json**
    ```csharp   
    var imageStyleAsJsonResult = imageStyle.SaveToFile("~/Output/Sample12/ImageStyle", KnownFileFormat.Json);
    if (!tableStyleAsJsonResult.Success)
    {
         // Handle errors
    }
    ```
    **PdfTextStyle**
    ***

    **XML**
    ```csharp   
    var textStyleAsXmlResult = textStyle.SaveToFile("~/Output/Sample12/TextStyle");
    if (!textStyleAsXmlResult.Success)
    {
         // Handle errors
    }
    ```

    **Json**
    ```csharp   
    var textStyleAsJsonResult = textStyle.SaveToFile("~/Output/Sample12/TextStyle", KnownFileFormat.Json);
    if (!textStyleAsJsonResult.Success)
    {
         // Handle errors
    }
    ```

    **PdfTableStyle**
    ***

    **XML**
    ```csharp   
    var tableStyleAsXmlResult = tableStyle.SaveToFile("~/Output/Sample12/TableStyle");
    if (!tableStyleAsXmlResult.Success)
    {
         // Handle errors
    }
    ```

    **Json**
    ```csharp   
    var tableStyleAsJsonResult = tableStyle.SaveToFile("~/Output/Sample12/TableStyle", KnownFileFormat.Json);
    if (!tableStyleAsJsonResult.Success)
    {
         // Handle errors
    }
    ```
3. Output

    As an example, the result of serializing an image style is shown. 
    You can see all the results by following the following link [Sample12].

<table>
 <tr>
  <td><strong>XML</strong></td>
 </tr>
 <tr>
  <td style="vertical-align: top">

```XML
<?xml version="1.0" encoding="utf-8"?>
<PdfBaseStyle xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:q1="http://schemas.iTin.com/pdf/style/v1.0" xsi:type="q1:PdfImageStyle" Name="ImageStyle">
	<q1:Borders Color="Red" Position="Right" Show="Yes" />
	<q1:Borders Color="Yellow" Position="Top" Show="Yes" />
	<q1:Content AlternateColor="Blue" Color="Blue">
		<Properties xmlns="http://schemas.iTin.com/style/v1.0">
			<Property Name="p001" Value="v001" />
			<Property Name="p002" Value="v002" />
		</Properties>
		<q1:Alignment Horizontal="Right" />
	</q1:Content>
</PdfBaseStyle>
```

  </td>
 </tr>
</table>

<table>
 <tr>
  <td><strong>Json</strong></td>
 </tr>
 <tr>
  <td style="vertical-align: top">

```json
{
  "$type": "iTin.Utilities.Pdf.Design.Styles.PdfImageStyle, iTin.Utilities.Pdf.Design",
  "content": {
    "$type": "iTin.Utilities.Pdf.Design.Styles.PdfImageContent, iTin.Utilities.Pdf.Design",
    "alignment": {
      "$type": "iTin.Utilities.Pdf.Design.Styles.PdfImageContentAlignment, iTin.Utilities.Pdf.Design",
      "horizontal": "Right"
    },
    "alternate-color": "Blue",
    "color": "Blue",
    "properties": {
      "$type": "iTin.Core.Models.Properties, iTin.Core.Models",
      "$values": [
        {
          "$type": "iTin.Core.Models.Property, iTin.Core.Models",
          "name": "p001",
          "value": "v001"
        },
        {
          "$type": "iTin.Core.Models.Property, iTin.Core.Models",
          "name": "p002",
          "value": "v002"
        }
      ]
    }
  },
  "name": "ImageStyle",
  "inherits": null,
  "borders": {
    "$type": "iTin.Core.Models.Design.Styling.BordersCollection, iTin.Core.Models.Design.Styling",
    "$values": [
      {
        "$type": "iTin.Core.Models.Design.Styling.BaseBorder, iTin.Core.Models.Design.Styling",
        "color": "Red",
        "position": "Right",
        "show": "Yes"
      },
      {
        "$type": "iTin.Core.Models.Design.Styling.BaseBorder, iTin.Core.Models.Design.Styling",
        "color": "Yellow",
        "position": "Top",
        "show": "Yes"
      }
    ]
  }
}
```

  </td>
 </tr>
</table>

4. Deserialize styles

    Below is how to deserialize an image style

    **XML**
    ```csharp   
    var imageStyleFromXml = PdfImageStyle.LoadFromFile("~/Output/Sample12/ImageStyle.xml");
    if (imageStyleFromXml == null)
    {
         // Handle errors
    }
    ```

     **Json**
   ```csharp   
    var imageStyleFromJson = PdfImageStyle.LoadFromFile("~/Output/Sample12/ImageStyle.json", KnownFileFormat.Json);
    if (imageStyleFromJson == null)
    {
         // Handle errors
    }
    ```

### Sample 10 - Show a more complete example

 Basic steps, for more details please see [sample13.cs] file.

![Sample13AllPages][Sample13AllPages] 

### Sample 11 - Show the use of add an enumerable (render as HTML)in a pdf document

Basic steps, for more details please see [sample16.cs] file.

1. Defines a **Person** model

    ```csharp   
    public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }    
    ```             

2. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-16/file-sample.pdf"
    };
    ```             

3. Replace **#DATA-TABLE#** tag with an typed enumerable

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTableObject
        {
            Text = "#DATA-TABLE#",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            Style = PdfTableStyle.Default,
            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
            Table = PdfTable.CreateFromEnumerable(
                data: 
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
                    },
                css: @"
                    table { 
                        border-spacing: 0px;
                        border-collapse: collapse;  
                    }

                    tr {
                        font-size: 9pt;
                        font-family: Arial; 
                        color: #AC1198;
                        text-align: left;
                        overflow: hidden;
                    }

                    td {
                        padding: 6px;
                    }")
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
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample16/Sample-16" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
6. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample16AllPages][Sample16AllPages] 

### Sample 12 - Show the use of add an DataTable (render as HTML)in a pdf document

Basic steps, for more details please see [sample18.cs] file.

1. Defines a **Person** model

    ```csharp   
    public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }    
    ```             

2. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-18/file-sample.pdf"
    };
    ```             

3. Replace **#DATA-TABLE#** tag with a DataTable object

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
                data: 
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
                    }
                    .ToDataTable<Person>("People"),
                css: @"
                    table { 
                        border-spacing: 0px;
                        border-collapse: collapse;  
                    }

                    tr {
                        font-size: 9pt;
                        font-family: Arial; 
                        color: #AC1198;
                        text-align: left;
                        overflow: hidden;
                    }

                    td {
                        padding: 6px;
                    }")
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
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample18/Sample-18" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
6. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample18AllPages][Sample18AllPages] 

### Sample 13 - Shows the use of text and image replacement with styles from file

Basic steps, for more details please see [sample20.cs] file.

1. Creates Style file

   Remember you can use both **XML** and **Json** formats.

    ##### TextStyle
    ```XML   
    <?xml version="1.0" encoding="utf-8"?>
    <PdfBaseStyle xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:q1="http://schemas.iTin.com/pdf/style/v1.0" xsi:type="q1:PdfTextStyle" Name="ReportTitle">
      <q1:Font Name="Arial" Size="28" Color="Blue" Bold="Yes" Italic="Yes" />
      <q1:Content AlternateColor="Transparent">
        <q1:Alignment Horizontal="Center" />
      </q1:Content>
    </PdfBaseStyle>  
    ```

    ##### ImageStyle
    ```Json   
    {
        "$type": "iTin.Utilities.Pdf.Design.Styles.PdfImageStyle, iTin.Utilities.Pdf.Design",
        "content": {
        "$type": "iTin.Utilities.Pdf.Design.Styles.PdfImageContent, iTin.Utilities.Pdf.Design",
        "alignment": {
            "$type": "iTin.Utilities.Pdf.Design.Styles.PdfImageContentAlignment, iTin.Utilities.Pdf.Design",
            "horizontal": "Center"
        },
        "alternate-color": "Transparent"
        },
        "name": "Center",
        "inherits": null
    }
    ```             

2. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-18/file-sample.pdf"
    };
    ```             

3. Replace **#TITLE#** tag with another text but using a style that is loaded from a file

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTextObject
        {
            Text = "#TITLE#",
            NewText = "Lorem ipsum",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
            Style = (PdfTextStyle) PdfTextStyle.LoadFromFile("~Resources/Sample-18/Styles/TextStyle.Xml", format: KnownFileFormat.Xml)
        }));        
    ```

4. Replace **#IMAGE1#** tag with an image but using a style that is loaded from a file

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithImageObject
        {
            Text = "#IMAGE1#",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
            Image = PdfImage.FromFile("~/Resources/Sample-18/Images/image-1.jpg"),
            Style = (PdfImageStyle) PdfImageStyle.LoadFromFile("~Resources/Sample-18/Styles/ImageStyle.Json", format: KnownFileFormat.Json)
        }));   
    ```

5. Try to create pdf output result

     ```csharp   
     var result = doc.CreateResult();
     if (!result.Success)
     {
         // Handle errors                 
     }
     ```
6. Save pdf result to file
    
    ```csharp   
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample18/Sample-18" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
7. Output

   The result is the same as in sample 1.

### Sample 14 - Shows how to work with fonts

Basic steps, for more details please see [sample21.cs] file.

1. Define the styles to use. notice that now the dictionary is of type **PdfBaseStyle**.
              
    ```csharp   
    private static readonly Dictionary<string, PdfBaseStyle> StylesTable = new()
    {
        {
            "ReportTitle",
            new PdfTextStyle
            {
                Font =
                {
                    Name = "Pacifico",
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
        },
        {
            "Center",
            new PdfImageStyle
            {
                Content =
                {
                    Alignment =
                    {
                        Horizontal = KnownHorizontalAlignment.Center
                    }
                }
            }
        },
        {
            "Default",
            new PdfImageStyle
            {
                Content =
                {
                    Alignment =
                    {
                        Horizontal = KnownHorizontalAlignment.Left
                    }
                }
            }
        }
    };

2. Register font from file

    ```csharp   
    var registerResult = PdfFonts.RegisterFont("Pacifico", @"~Resources/Sample-21/Fonts/Pacifico/Pacifico.ttf");
    if (!registerResult.Success)
    {
         // Handle errors                 
    }
    ```             

3. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-21/file-sample.pdf"
    };
    ```             

4. Replace **#TITLE#** tag with another text but using a style that is loaded from a file

    ```csharp   
    doc.Replace(new ReplaceText(
        new WithTextObject
        {
            Text = "#TITLE#",
            NewText = "Lorem ipsum",
            UseTestMode = useTestMode,
            Offset = PointF.Empty,
            Style = (PdfTextStyle) StylesTable["ReportTitle"],
            ReplaceOptions = ReplaceTextOptions.AccordingToMargins
        }));        
    ```

5. Try to create pdf output result

     ```csharp   
     var result = doc.CreateResult();
     if (!result.Success)
     {
         // Handle errors                 
     }
     ```
6. Save pdf result to file
    
    ```csharp   
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample21/Sample-21" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```
7. Output

   ###### Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample21AllPages][Sample21AllPages] 

### Sample 15 - Shows how to create a new PdfInput instance from HTML code

Basic steps, for more details please see [sample25.cs] file.

1. Creates **PdfInput** from **HTML** code.
    ```csharp   
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
    ```   
          
2. Try to create pdf output result
     ```csharp   
     var result = doc.CreateResult();
     if (!result.Success)
     {
         // Handle errors                 
     }
     ```

3. Save pdf result to file
    ```csharp   
    var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample25/Sample-25" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```

4. Output

   ###### Below is an image showing the result

![Sample25][sample25] 

### Sample 16 - Shows how to add or modify the metadata information

Basic steps, for more details please see [sample26.cs] file.

1. Load pdf file

    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-26/file-sample.pdf"
    };
    ```    

 2. Set metadata information
    ```csharp   
    doc
        .Set(new SetCreator { Value = "iPdfWriter" })
        .Set(new SetTitle { Value = "Hello from iPdfWriter" })
        .Set(new SetSubject { Value = "Subject changed from iPdfWriter" })
        .Set(new SetAuthor { Value = "iPdfWriter" })
        .Set(new SetKeywords { Value = "Samples, iPdfWriter, pdf" });
    ```
        
3. Try to create pdf output result
     ```csharp   
     var outputResult = doc.CreateResult();
     if (!outputResult.Success)
     {
         // Handle errors                 
     }
     ```

4. Save pdf result to file   
    ```csharp   
    var saveResult = outputResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample26/Sample-26" });
    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```

5. Output

   ###### Below is an image showing the result

    ![Sample26][sample26] 

### Sample 17 - Shows how to add a password to pdf file

Basic steps, for more details please see [sample27.cs] file.

1. Load pdf file
    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-27/file-sample.pdf"
    };
    ```             
2. Replace, insert or set actions
    ```csharp   
    //
    // Replace, insert or set actions here!
    //      
    ```             

3. Try to create pdf output result
     ```csharp   
     var outputResult = doc.CreateResult();
     if (!outputResult.Success)
     {
         // Handle errors                 
     }
     ```

4. Save result with **password** to file
    ```csharp   
    var saveResult = outputResult.Result.Action(new SaveToFile 
    { 
        Password = "iPdfWriter",
        OutputPath = "~/Output/Sample27/Sample-27" 
    });

    if (!saveResult.Success)
    {
         // Handle errors                 
    }
     ```

5. Output

   ###### Below is an image showing the result

    ![Sample27][sample27] 

### Sample 18 - Shows how to insert an image into a pdf file

Basic steps, for more details please see [sample28.cs] file.

1. Load pdf file
    ```csharp   
    var doc = new PdfInput
    {
        AutoUpdateChanges = true,
        Input = "~/Resources/Sample-28/file-sample.pdf"
    };
    ```             

2. Insert actions
    ```csharp   
    doc.Insert(new InsertImage
    {
        Page = 1, 
        UseTestMode = YesNo.No,
        Offset = new PointF(450.0f, 200.0f),
        Image = PdfImage.FromFile("~/Resources/Sample-28/Images/pirate.png")
    });
    ```             

3. Try to create pdf output result
    ```csharp   
    var outputResult = doc.CreateResult();
    if (!outputResult.Success)
    {
        // Handle errors                 
    }
    ```

4. Save result to file
    ```csharp   
    var saveResult = outputResult.Result.Action(new SaveToFile 
    { 
        OutputPath = "~/Output/Sample28/Sample-28" 
    });

    if (!saveResult.Success)
    {
         // Handle errors                 
    }
    ```

5. Output

   ###### Below is an image showing the result

    ![Sample28][sample28] 

# Documentation

 - For **Writer** code documentation, please see next link [documentation].

# How can I send feedback!!!

If you have found **iPdfWriter** useful at work or in a personal project, I would love to hear about it. If you have decided not to use **iPdfWriter**, please send me and email stating why this is so. I will use this feedback to improve **iPdfWriter** in future releases.

My email address is 

![email.png][email] 


[email]: ./assets/email.png "email"
[documentation]: ./documentation/iTin.Utilities.Pdf.Writer.md
[Test samples]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60

[sample01.cs]: ./src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample01.cs"
[Sample01Page01]: ./assets/samples/sample1/page1.png "sample01 - page01"

[sample02.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample02.cs
[Sample02Page02]: ./assets/samples/sample2/page2.png "sample02 - page02"

[sample03.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample03.cs
[Sample03AllPages]: ./assets/samples/sample3/mergeresult.png "sample03 - merge"

[sample04.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample04.cs
[Sample04AllPages]: ./assets/samples/sample4/globalreplacements.png "sample04 - global header"

[sample05.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample05.cs
[Sample05AllPages]: ./assets/samples/sample5/systemtags.png "sample05 - system tags - page numbers"

[sample06.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample06.cs
[Sample06AllPages]: ./assets/samples/sample6/testmode.png "sample06 - test mode"

[sample07.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample07.cs
[Sample07.zip]: https://github.com/iAJTin/iPdfWriter/tree/master/src/test/iPdfWriter.ConsoleAppCore/Output/Sample07
[OutputResultConfig.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/lib/iTin.Utilities/iTin.Utilities.Pdf/iTin.Utilities.Pdf.Writer/ComponentModel/Config/OutputResultConfig.cs

[sample08.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample08.cs
[Sample08.zip]: https://github.com/iAJTin/iPdfWriter/tree/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Output/Sample08
[PdfObjectConfig.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/lib/iTin.Utilities/iTin.Utilities.Pdf/iTin.Utilities.Pdf.Writer/ComponentModel/Config/OutputResultConfig.cs

[Sample09.zip]: https://github.com/iAJTin/iPdfWriter/tree/master/src/test/iPdfWriter.ConsoleAppCore/Output/Sample09

[Sample10.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample10.cs
[Sample10.zip]: https://github.com/iAJTin/iPdfWriter/tree/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Output/Sample10

[sample12.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample12.cs
[Sample12]: https://github.com/iAJTin/iPdfWriter/tree/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Output/Sample12

[sample13.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample13.cs
[Sample13AllPages]: ./assets/samples/sample13/sample13.png "sample13"

[sample16.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample16.cs
[Sample16AllPages]: ./assets/samples/sample16/sample16.png "sample16"

[sample17.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample17.cs
[Sample17AllPages]: ./assets/samples/sample17/sample17.png "sample17"

[sample18.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample18.cs
[Sample18AllPages]: ./assets/samples/sample18/sample18.png "sample18"

[sample20.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample20.cs

[sample21.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample21.cs
[Sample21AllPages]: ./assets/samples/sample21/sample21.png "sample21"

[sample25.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample25.cs
[sample25]: ./assets/samples/sample25/sample25.png "sample25"

[sample26.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample26.cs
[sample26]: ./assets/samples/sample26/sample26.png "sample26"

[sample27.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample27.cs
[sample27]: ./assets/samples/sample27/sample27.png "sample27"

[sample28.cs]: https://github.com/iAJTin/iPdfWriter/blob/master/src/test/NetCore/iPdfWriter.ConsoleAppCore60/Code/Sample28.cs
[sample28]: ./assets/samples/sample28/sample28.png "sample28"
