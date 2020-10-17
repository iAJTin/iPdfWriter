# iTin.Utilities.Pdf.Writer assembly

## iTin.Utilities.Pdf.Writer namespace

| public type | description |
| --- | --- |
| static class [PdfFonts](iTin.Utilities.Pdf.Writer/PdfFonts.md) | Represents |
| class [PdfInput](iTin.Utilities.Pdf.Writer/PdfInput.md) | Represents a pdf file. |
| static class [PdfInputExtensions](iTin.Utilities.Pdf.Writer/PdfInputExtensions.md) | Static class than contains common extension methods for objects of type [`PdfInput`](iTin.Utilities.Pdf.Writer/PdfInput.md). |
| class [PdfObject](iTin.Utilities.Pdf.Writer/PdfObject.md) | Represents a generic pdf object, this allows add pdf files to [`Items`](iTin.Utilities.Pdf.Writer/PdfObject/Items.md) property and specify a user custom configuration. |

## iTin.Utilities.Pdf.Writer.ComponentModel namespace

| public type | description |
| --- | --- |
| enum [EndLocationStrategy](iTin.Utilities.Pdf.Writer.ComponentModel/EndLocationStrategy.md) | Defines the strategy of the end point of the element to be inserted. |
| interface [IInput](iTin.Utilities.Pdf.Writer.ComponentModel/IInput.md) | Defines a generic input |
| interface [IInsert](iTin.Utilities.Pdf.Writer.ComponentModel/IInsert.md) | Defines allowed actions for insert objects |
| abstract class [InsertBase](iTin.Utilities.Pdf.Writer.ComponentModel/InsertBase.md) | Specialization of [`IInsert`](iTin.Utilities.Pdf.Writer.ComponentModel/IInsert.md) interface. Acts as base class for insert actions. |
| class [InsertImage](iTin.Utilities.Pdf.Writer.ComponentModel/InsertImage.md) | A Specialization of [`InsertBase`](iTin.Utilities.Pdf.Writer.ComponentModel/InsertBase.md) class. Allows insert an image object. |
| interface [IPdfObjectConfig](iTin.Utilities.Pdf.Writer.ComponentModel/IPdfObjectConfig.md) | Represents configuration information for an object [`PdfObject`](iTin.Utilities.Pdf.Writer/PdfObject.md). |
| interface [IReplace](iTin.Utilities.Pdf.Writer.ComponentModel/IReplace.md) | Defines allowed actions for replacement objects |
| interface [ISet](iTin.Utilities.Pdf.Writer.ComponentModel/ISet.md) | Defines allowed actions for set features to pdf document. |
| interface [ISystemTag](iTin.Utilities.Pdf.Writer.ComponentModel/ISystemTag.md) | Defines a generic System Tag. |
| enum [KnownInputType](iTin.Utilities.Pdf.Writer.ComponentModel/KnownInputType.md) | Specifies the known input type. |
| enum [KnownOutputType](iTin.Utilities.Pdf.Writer.ComponentModel/KnownOutputType.md) | Specifies the known output type. |
| class [LocationTextExtractionStrategy](iTin.Utilities.Pdf.Writer.ComponentModel/LocationTextExtractionStrategy.md) | Class LocationTextExtractionStrategy. |
| class [OutputResultConfig](iTin.Utilities.Pdf.Writer.ComponentModel/OutputResultConfig.md) | Represents configuration information for an object [`PdfObject`](iTin.Utilities.Pdf.Writer/PdfObject.md). |
| class [PageNumberSystemTag](iTin.Utilities.Pdf.Writer.ComponentModel/PageNumberSystemTag.md) | Specialization of the [`ISystemTag`](iTin.Utilities.Pdf.Writer.ComponentModel/ISystemTag.md) interface that represents the system tag that allows to replace the page number in a document when merging documents. |
| class [PdfObjectConfig](iTin.Utilities.Pdf.Writer.ComponentModel/PdfObjectConfig.md) | Represents configuration information for an object [`PdfObject`](iTin.Utilities.Pdf.Writer/PdfObject.md). |
| abstract class [ReplacementBase](iTin.Utilities.Pdf.Writer.ComponentModel/ReplacementBase.md) | Specialization of [`IReplacement`](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement/IReplacement.md) interface. Acts as base class for replacement actions. |
| class [ReplaceSystemTag](iTin.Utilities.Pdf.Writer.ComponentModel/ReplaceSystemTag.md) | Specialization of [`IReplace`](iTin.Utilities.Pdf.Writer.ComponentModel/IReplace.md) interface. Contains the information for replace a text object. |
| class [ReplaceText](iTin.Utilities.Pdf.Writer.ComponentModel/ReplaceText.md) | Specialization of [`IReplace`](iTin.Utilities.Pdf.Writer.ComponentModel/IReplace.md) interface that contains the information for replace a text object. |
| class [ReplaceTextOptions](iTin.Utilities.Pdf.Writer.ComponentModel/ReplaceTextOptions.md) | Represents replace text options |
| abstract class [SetBase](iTin.Utilities.Pdf.Writer.ComponentModel/SetBase.md) | Specialization of [`ISet`](iTin.Utilities.Pdf.Writer.ComponentModel/ISet.md) interface. Acts as base class for set actions |
| enum [StartLocationStrategy](iTin.Utilities.Pdf.Writer.ComponentModel/StartLocationStrategy.md) | Defines the strategy of the start point of the element to be inserted. |
| enum [SystemTags](iTin.Utilities.Pdf.Writer.ComponentModel/SystemTags.md) | Defines the system replaceable tags. |
| class [SystemTagsCollection](iTin.Utilities.Pdf.Writer.ComponentModel/SystemTagsCollection.md) | Represents a collection of system tags |
| class [TotalPagesSystemTag](iTin.Utilities.Pdf.Writer.ComponentModel/TotalPagesSystemTag.md) | Specialization of the [`ISystemTag`](iTin.Utilities.Pdf.Writer.ComponentModel/ISystemTag.md) interface that represents the system tag that allows to replace the total page number in a document when merging documents. |
| enum [VerticalFineStrategy](iTin.Utilities.Pdf.Writer.ComponentModel/VerticalFineStrategy.md) | Defines the strategy of the fine vertical point of the element to be inserted. |

## iTin.Utilities.Pdf.Writer.ComponentModel.Replacement namespace

| public type | description |
| --- | --- |
| interface [IReplacement](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement/IReplacement.md) | Defines allowed actions for bookmark replacement object |

## iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text namespace

| public type | description |
| --- | --- |
| class [GlobalReplacementsCollection](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/GlobalReplacementsCollection.md) | Represents a collection of system tags |
| interface [ISystemTagTextReplacement](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/ISystemTagTextReplacement.md) | Defines allowed actions for text replacement object |
| interface [ITextReplacement](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/ITextReplacement.md) | Defines allowed actions for text replacement object |
| abstract class [SystemTagTextReplacementBase](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/SystemTagTextReplacementBase.md) | Specialization of [`IReplacement`](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement/IReplacement.md) interface. Acts as base class for system tags replacement actions. |
| abstract class [TextReplacementBase](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/TextReplacementBase.md) | Specialization of [`IReplacement`](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement/IReplacement.md) interface. Acts as base class for text insert actions. |
| class [WithImageObject](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/WithImageObject.md) | Specialization of [`TextReplacementBase`](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with an image. |
| class [WithTableObject](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/WithTableObject.md) | Specialization of [`TextReplacementBase`](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with a pdf table. |
| class [WithTextObject](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/WithTextObject.md) | Specialization of [`TextReplacementBase`](iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with another text. |

## iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action namespace

| public type | description |
| --- | --- |
| interface [IOutputAction](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action/IOutputAction.md) | Defines allowed actions for output result. |

## iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save namespace

| public type | description |
| --- | --- |
| class [SaveToFile](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save/SaveToFile.md) | Defines allowed actions for output result data |

## iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert namespace

| public type | description |
| --- | --- |
| class [InsertResult](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert/InsertResult.md) | Specialization of ResultBase interface. Represents result after insert an element in pdf document. |
| class [InsertResultData](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert/InsertResultData.md) | Represents insert data for an object [`InsertResult`](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert/InsertResult.md). |

## iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output namespace

| public type | description |
| --- | --- |
| class [OutputResult](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output/OutputResult.md) | Specialization of ResultBase interface. Represents configuration information for an object [`OutputResult`](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output/OutputResult.md). |
| class [OutputResultData](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output/OutputResultData.md) | Represents configuration information for an object [`OutputResultData`](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output/OutputResultData.md). |

## iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace namespace

| public type | description |
| --- | --- |
| class [ReplaceResult](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace/ReplaceResult.md) | Specialization of ResultBase interface. Represents result after insert an element in pdf document. |
| class [ReplaceResultData](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace/ReplaceResultData.md) | Represents insert data for an object [`ReplaceResult`](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace/ReplaceResult.md). |

## iTin.Utilities.Pdf.Writer.ComponentModel.Result.Set namespace

| public type | description |
| --- | --- |
| class [SetResult](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Set/SetResult.md) | Specialization of ResultBase interface. Represents result after set an element in pdf document. |
| class [SetResultData](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Set/SetResultData.md) | Represents set data for an object [`SetResult`](iTin.Utilities.Pdf.Writer.ComponentModel.Result.Set/SetResult.md). |

<!-- DO NOT EDIT: generated by xmldocmd for iTin.Utilities.Pdf.Writer.dll -->
