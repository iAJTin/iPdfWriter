# PdfOutputResultData class

Represents configuration information for an object [`PdfOutputResultData`](./PdfOutputResultData.md).

```csharp
public class PdfOutputResultData : IPdfOutputResultData
```

## Public Members

| name | description |
| --- | --- |
| [PdfOutputResultData](PdfOutputResultData/PdfOutputResultData.md)() | Initializes a new instance of the [`PdfOutputResultData`](./PdfOutputResultData.md) class. |
| [IsZipped](PdfOutputResultData/IsZipped.md) { get; } | Gets or sets a value indicating whether output file has been zipped. |
| [OutputType](PdfOutputResultData/OutputType.md) { get; } | Gets a value indicating type of output file. |
| [Action](PdfOutputResultData/Action.md)(…) | Executes specified action for this output result instance. (2 methods) |
| [GetOutputStream](PdfOutputResultData/GetOutputStream.md)() | Returns a reference to the output stream. |
| [GetOutputStreamAsync](PdfOutputResultData/GetOutputStreamAsync.md)(…) | Returns a reference to the output stream asynchronously. |
| [GetUnCompressedOutputStream](PdfOutputResultData/GetUnCompressedOutputStream.md)() | Returns a reference to the uncompressed output stream. If [`OutputType`](./PdfOutputResultData/OutputType.md) is Pdf this value is equals to OutputStream value property. |
| [GetUnCompressedOutputStreamAsync](PdfOutputResultData/GetUnCompressedOutputStreamAsync.md)(…) | Returns a reference to the uncompressed output stream. If [`OutputType`](./PdfOutputResultData/OutputType.md) is Pdf this value is equals to OutputStream value property asynchronously. |
| override [ToString](PdfOutputResultData/ToString.md)() | Returns a string that represents the current data type. |

## See Also

* interface [IPdfOutputResultData](./IPdfOutputResultData.md)
* namespace [iPdfWriter.Operations.Result.Output](../iPdfWriter.md)

<!-- DO NOT EDIT: generated by xmldocmd for iPdfWriter.dll -->