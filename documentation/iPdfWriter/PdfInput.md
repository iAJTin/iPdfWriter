# PdfInput class

Represents a pdf file.

```csharp
public sealed class PdfInput : ICloneable, IPdfInput
```

## Public Members

| name | description |
| --- | --- |
| [PdfInput](PdfInput/PdfInput.md)() | Initializes a new instance of the [`PdfInput`](./PdfInput.md) class. |
| static [CreateFromHtml](PdfInput/CreateFromHtml.md)(…) | Creates a new [`PdfInput`](./PdfInput.md) object from HTML code. |
| [AutoUpdateChanges](PdfInput/AutoUpdateChanges.md) { get; set; } | Gets or sets a Result indicating whether automatic updates for changes. |
| [DeletePhysicalFilesAfterMerge](PdfInput/DeletePhysicalFilesAfterMerge.md) { get; set; } | Gets or sets a Result indicating whether delete physical files after merge. |
| [Index](PdfInput/Index.md) { get; set; } | Gets or sets a Result that contains input index. |
| [Input](PdfInput/Input.md) { get; set; } | Gets or sets the input object. |
| [InputType](PdfInput/InputType.md) { get; } | Gets input type. |
| [Clone](PdfInput/Clone.md)() | Create a new object that is a copy of the current instance. |
| [CreateResult](PdfInput/CreateResult.md)(…) | Returns a new reference OutputResult that complies with what is indicated in its configuration object. By default, this [`PdfInput`](./PdfInput.md) will not be zipped. |
| [CreateResultAsync](PdfInput/CreateResultAsync.md)(…) | Returns a new reference OutputResult that complies with what is indicated in its configuration object. By default, this [`PdfInput`](./PdfInput.md) will not be zipped. |
| [ExtractPages](PdfInput/ExtractPages.md)(…) | Create a new [`PdfInput`](./PdfInput.md) containing the selected pages. |
| [ExtractPagesAsync](PdfInput/ExtractPagesAsync.md)(…) | Create a new [`PdfInput`](./PdfInput.md) asynchronously containing the selected pages. |
| [Insert](PdfInput/Insert.md)(…) |  |
| [NumberOfPages](PdfInput/NumberOfPages.md)() | Returns total pages of this [`PdfInput`](./PdfInput.md). |
| [NumberOfPagesAsync](PdfInput/NumberOfPagesAsync.md)(…) | Returns total pages of this [`PdfInput`](./PdfInput.md) asynchronously. |
| [Replace](PdfInput/Replace.md)(…) |  |
| [SaveToFile](PdfInput/SaveToFile.md)(…) | Saves this input into a file. |
| [SaveToFileAsync](PdfInput/SaveToFileAsync.md)(…) | Saves this input into a file asynchronously. |
| [SearchText](PdfInput/SearchText.md)(…) | Search specified text into this input file. |
| [SearchTextAsync](PdfInput/SearchTextAsync.md)(…) | Search asynchronously specified text into this input file. |
| [Set](PdfInput/Set.md)(…) |  |
| [TextLines](PdfInput/TextLines.md)(…) | Gets the lines of text for this [`PdfInput`](./PdfInput.md), optionally you can set both the start and end pages and a value indicating whether blank lines are included in the result. (2 methods) |
| [TextLinesAsync](PdfInput/TextLinesAsync.md)(…) | Gets the lines of text for this [`PdfInput`](./PdfInput.md) asynchronously, optionally you can set both the start and end pages and a value indicating whether blank lines are included in the result. (2 methods) |
| [ToStream](PdfInput/ToStream.md)() | Convert this input into a stream object. |
| [ToStreamAsync](PdfInput/ToStreamAsync.md)(…) | Convert this input into a stream object. |
| override [ToString](PdfInput/ToString.md)() | Returns a string that represents the current data type. |

## See Also

* interface [IPdfInput](../iPdfWriter.Input/IPdfInput.md)
* namespace [iPdfWriter](../iPdfWriter.md)

<!-- DO NOT EDIT: generated by xmldocmd for iPdfWriter.dll -->