# TextReplacementBase class

Specialization of [`IReplacement`](../iTin.Utilities.Pdf.Writer.ComponentModel.Replacement/IReplacement.md) interface. Acts as base class for text insert actions.

```csharp
public abstract class TextReplacementBase : ITextReplacement
```

## Public Members

| name | description |
| --- | --- |
| [ReplaceOptions](TextReplacementBase/ReplaceOptions.md) { get; set; } | Gets or sets a value that represents text replace options. |
| [Style](TextReplacementBase/Style.md) { get; set; } | Gets or sets a reference to style to apply. |
| [Text](TextReplacementBase/Text.md) { get; set; } | Gets or sets the text to replace. |
| [Apply](TextReplacementBase/Apply.md)(…) | Try to execute the replacement action. (2 methods) |

## Protected Members

| name | description |
| --- | --- |
| [TextReplacementBase](TextReplacementBase/TextReplacementBase.md)() | The default constructor. |
| abstract [ReplaceImpl](TextReplacementBase/ReplaceImpl.md)(…) | Implementation to execute when replace action. |

## See Also

* interface [ITextReplacement](ITextReplacement.md)
* namespace [iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text](../iTin.Utilities.Pdf.Writer.md)

<!-- DO NOT EDIT: generated by xmldocmd for iTin.Utilities.Pdf.Writer.dll -->
