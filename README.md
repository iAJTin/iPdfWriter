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
            UseTestMode = useTestMode,
            TextOffset = PointF.Empty,
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
                UseTestMode = useTestMode,
                ImageOffset = PointF.Empty,
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
            UseTestMode = useTestMode,
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
     if (!result.Success)
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
