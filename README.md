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

I hope it helps someone. ;)

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

- Page 1

    Basic steps, for more details please see [sample01.cs] file.

    1. Load pdf file

            var doc = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-01/file-sample.pdf"
            };
            
    2. Define the styles to use

      2.1 Text styles
      
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
        
      2.2 Image styles
      
            private static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new Dictionary<string, PdfImageStyle>
            {           
                {
                    "Default",
                    new PdfImageStyle { Content = { Alignment = { Horizontal = KnownHorizontalAlignment.Left }}}
                }
            };    

    3. Replace **#TITLE#** tag with another text

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

    4. Replace **#BAR-CHART#** tag with an image

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

    5. Save pdf file result

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample01/Sample-01" });


    Below is an image showing the original pdf file and the result after applying the replacements described above

![Sample01Page01][Sample01Page01] 


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
