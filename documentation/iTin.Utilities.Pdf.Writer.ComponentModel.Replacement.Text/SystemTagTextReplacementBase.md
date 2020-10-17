# SystemTagTextReplacementBase class

Specialization of [`IReplacement`](../iTin.Utilities.Pdf.Writer.ComponentModel.Replacement/IReplacement.md) interface. Acts as base class for system tags replacement actions.

```csharp
public abstract class SystemTagTextReplacementBase : ISystemTagTextReplacement
```

## Public Members

| name | description |
| --- | --- |
| [ReplaceOptions](SystemTagTextReplacementBase/ReplaceOptions.md) { get; set; } | Gets or sets a value that represents text replace options. |
| [Style](SystemTagTextReplacementBase/Style.md) { get; set; } | Gets or sets a reference to style to apply. |
| [Tag](SystemTagTextReplacementBase/Tag.md) { get; set; } | Gets or sets a value that contains the system tag to replace. |
| [Apply](SystemTagTextReplacementBase/Apply.md)(…) | Try to execute the replacement action. (2 methods) |

## Protected Members

| name | description |
| --- | --- |
| [SystemTagTextReplacementBase](SystemTagTextReplacementBase/SystemTagTextReplacementBase.md)() | The default constructor. |
| abstract [ReplaceImpl](SystemTagTextReplacementBase/ReplaceImpl.md)(…) | Implementation to execute when replace action. |

## See Also

* interface [ISystemTagTextReplacement](ISystemTagTextReplacement.md)
* namespace [iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text](../iTin.Utilities.Pdf.Writer.md)

<!-- DO NOT EDIT: generated by xmldocmd for iTin.Utilities.Pdf.Writer.dll -->