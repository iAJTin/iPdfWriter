# iTin.Utilities.Pdf.Writer assembly

## iTin.Utilities.Pdf.Writer namespace

| public type | description |
| --- | --- |
| static class [PdfFonts](./iTin.Utilities.Pdf.Writer/PdfFonts.md) | Represents |
| class [PdfInput](./iTin.Utilities.Pdf.Writer/PdfInput.md) | Represents a pdf file. |
| static class [PdfInputExtensions](./iTin.Utilities.Pdf.Writer/PdfInputExtensions.md) | Static class than contains common extension methods for objects of type [`PdfInput`](./iTin.Utilities.Pdf.Writer/PdfInput.md). |
| class [PdfObject](./iTin.Utilities.Pdf.Writer/PdfObject.md) | Represents a generic pdf object, this allows add pdf files to [`Items`](./iTin.Utilities.Pdf.Writer/PdfObject/Items.md) property and specify a user custom configuration. |

## iTin.Utilities.Pdf.Writer.ComponentModel namespace

| public type | description |
| --- | --- |
| enum [KnownInputType](./iTin.Utilities.Pdf.Writer.ComponentModel/KnownInputType.md) | Specifies the known input type. |
| enum [KnownOutputType](./iTin.Utilities.Pdf.Writer.ComponentModel/KnownOutputType.md) | Specifies the known output type. |
| class [PdfObjectConfig](./iTin.Utilities.Pdf.Writer.ComponentModel/PdfObjectConfig.md) | Represents configuration information for an object [`PdfObject`](./iTin.Utilities.Pdf.Writer/PdfObject.md). |

## iTin.Utilities.Pdf.Writer.Config namespace

| public type | description |
| --- | --- |
| interface [IPdfObjectConfig](./iTin.Utilities.Pdf.Writer.Config/IPdfObjectConfig.md) | Represents configuration information for an object [`PdfObject`](./iTin.Utilities.Pdf.Writer/PdfObject.md). |
| class [PdfOutputResultConfig](./iTin.Utilities.Pdf.Writer.Config/PdfOutputResultConfig.md) | Represents configuration information for an object [`PdfObject`](./iTin.Utilities.Pdf.Writer/PdfObject.md). |

## iTin.Utilities.Pdf.Writer.Input namespace

| public type | description |
| --- | --- |
| interface [IPdfInput](./iTin.Utilities.Pdf.Writer.Input/IPdfInput.md) | Defines a generic input |
| interface [IPdfInputAction](./iTin.Utilities.Pdf.Writer.Input/IPdfInputAction.md) |  |
| class [PdfInputAction](./iTin.Utilities.Pdf.Writer.Input/PdfInputAction.md) |  |
| class [PdfInputCache](./iTin.Utilities.Pdf.Writer.Input/PdfInputCache.md) |  |

## iTin.Utilities.Pdf.Writer.Operations.Insert namespace

| public type | description |
| --- | --- |
| interface [IInsert](./iTin.Utilities.Pdf.Writer.Operations.Insert/IInsert.md) | Defines allowed actions for insert objects |
| class [InsertImage](./iTin.Utilities.Pdf.Writer.Operations.Insert/InsertImage.md) | A Specialization of [`InsertImageBase`](./iTin.Utilities.Pdf.Writer.Operations.Insert/InsertImageBase.md) class. Allows insert an image object. |
| abstract class [InsertImageBase](./iTin.Utilities.Pdf.Writer.Operations.Insert/InsertImageBase.md) | Specialization of [`IInsert`](./iTin.Utilities.Pdf.Writer.Operations.Insert/IInsert.md) interface. Acts as base class for insert image actions. |
| abstract class [InsertTextBase](./iTin.Utilities.Pdf.Writer.Operations.Insert/InsertTextBase.md) | Specialization of [`IInsert`](./iTin.Utilities.Pdf.Writer.Operations.Insert/IInsert.md) interface. Acts as base class for insert text actions. |

## iTin.Utilities.Pdf.Writer.Operations.Replace namespace

| public type | description |
| --- | --- |
| enum [EndLocationStrategy](./iTin.Utilities.Pdf.Writer.Operations.Replace/EndLocationStrategy.md) | Defines the strategy of the end point of the element to be inserted. |
| interface [IReplace](./iTin.Utilities.Pdf.Writer.Operations.Replace/IReplace.md) | Defines allowed actions for replacement objects |
| class [ReplaceSystemTag](./iTin.Utilities.Pdf.Writer.Operations.Replace/ReplaceSystemTag.md) | Specialization of [`IReplace`](./iTin.Utilities.Pdf.Writer.Operations.Replace/IReplace.md) interface. Contains the information for replace a text object. |
| class [ReplaceText](./iTin.Utilities.Pdf.Writer.Operations.Replace/ReplaceText.md) | Specialization of [`IReplace`](./iTin.Utilities.Pdf.Writer.Operations.Replace/IReplace.md) interface that contains the information for replace a text object. |
| class [ReplaceTextOptions](./iTin.Utilities.Pdf.Writer.Operations.Replace/ReplaceTextOptions.md) | Represents replace text options |
| enum [StartLocationStrategy](./iTin.Utilities.Pdf.Writer.Operations.Replace/StartLocationStrategy.md) | Defines the strategy of the start point of the element to be inserted. |
| enum [VerticalFineStrategy](./iTin.Utilities.Pdf.Writer.Operations.Replace/VerticalFineStrategy.md) | Defines the strategy of the fine vertical point of the element to be inserted. |

## iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement namespace

| public type | description |
| --- | --- |
| interface [IReplacement](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement/IReplacement.md) | Defines allowed actions for bookmark replacement object |

## iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text namespace

| public type | description |
| --- | --- |
| class [GlobalReplacementsCollection](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/GlobalReplacementsCollection.md) | Represents a collection of system tags |
| interface [ISystemTagTextReplacement](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/ISystemTagTextReplacement.md) | Defines allowed actions for text replacement object |
| interface [ITextReplacement](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/ITextReplacement.md) | Defines allowed actions for text replacement object |
| abstract class [SystemTagTextReplacementBase](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/SystemTagTextReplacementBase.md) | Specialization of [`IReplacement`](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement/IReplacement.md) interface. Acts as base class for system tags replacement actions. |
| abstract class [TextReplacementBase](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/TextReplacementBase.md) | Specialization of [`IReplacement`](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement/IReplacement.md) interface. Acts as base class for text insert actions. |
| class [WithImageObject](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/WithImageObject.md) | Specialization of [`TextReplacementBase`](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with an image. |
| class [WithTableObject](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/WithTableObject.md) | Specialization of [`TextReplacementBase`](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with a pdf table. |
| class [WithTextObject](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/WithTextObject.md) | Specialization of [`TextReplacementBase`](./iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with another text. |

## iTin.Utilities.Pdf.Writer.Operations.Result.Actions namespace

| public type | description |
| --- | --- |
| class [SaveToFile](./iTin.Utilities.Pdf.Writer.Operations.Result.Actions/SaveToFile.md) | Defines allowed actions for output result data |
| class [SaveToFileAsync](./iTin.Utilities.Pdf.Writer.Operations.Result.Actions/SaveToFileAsync.md) | Defines allowed actions for output result data |

## iTin.Utilities.Pdf.Writer.Operations.Result.Insert namespace

| public type | description |
| --- | --- |
| class [InsertResultData](./iTin.Utilities.Pdf.Writer.Operations.Result.Insert/InsertResultData.md) | Represents insert data for an object InsertResult. |

## iTin.Utilities.Pdf.Writer.Operations.Result.Output namespace

| public type | description |
| --- | --- |
| interface [IPdfOutputResultData](./iTin.Utilities.Pdf.Writer.Operations.Result.Output/IPdfOutputResultData.md) | Represents configuration information for an object [`PdfOutputResultData`](./iTin.Utilities.Pdf.Writer.Operations.Result.Output/PdfOutputResultData.md). |
| class [PdfOutputResultData](./iTin.Utilities.Pdf.Writer.Operations.Result.Output/PdfOutputResultData.md) | Represents configuration information for an object [`PdfOutputResultData`](./iTin.Utilities.Pdf.Writer.Operations.Result.Output/PdfOutputResultData.md). |

## iTin.Utilities.Pdf.Writer.Operations.Result.Replace namespace

| public type | description |
| --- | --- |
| class [ReplaceResultData](./iTin.Utilities.Pdf.Writer.Operations.Result.Replace/ReplaceResultData.md) | Represents insert data for an object ReplaceResult. |

## iTin.Utilities.Pdf.Writer.Operations.Result.Set namespace

| public type | description |
| --- | --- |
| class [SetResultData](./iTin.Utilities.Pdf.Writer.Operations.Result.Set/SetResultData.md) | Represents set data for an object SetResult. |

## iTin.Utilities.Pdf.Writer.Operations.Set namespace

| public type | description |
| --- | --- |
| interface [ISet](./iTin.Utilities.Pdf.Writer.Operations.Set/ISet.md) |  |
| class [SetAuthor](./iTin.Utilities.Pdf.Writer.Operations.Set/SetAuthor.md) | A Specialization of [`SetBase`](./iTin.Utilities.Pdf.Writer.Operations.Set/SetBase.md) class. Allows define the document's author. |
| class [SetBase](./iTin.Utilities.Pdf.Writer.Operations.Set/SetBase.md) | A Specialization of [`ISet`](./iTin.Utilities.Pdf.Writer.Operations.Set/ISet.md) class. |
| class [SetCreator](./iTin.Utilities.Pdf.Writer.Operations.Set/SetCreator.md) | A Specialization of [`SetBase`](./iTin.Utilities.Pdf.Writer.Operations.Set/SetBase.md) class. Allows define the document's creator. |
| class [SetKeywords](./iTin.Utilities.Pdf.Writer.Operations.Set/SetKeywords.md) | A Specialization of [`SetBase`](./iTin.Utilities.Pdf.Writer.Operations.Set/SetBase.md) class. Allows define the document's keywords. |
| class [SetSubject](./iTin.Utilities.Pdf.Writer.Operations.Set/SetSubject.md) | A Specialization of [`SetBase`](./iTin.Utilities.Pdf.Writer.Operations.Set/SetBase.md) class. Allows define the document's subject. |
| class [SetTitle](./iTin.Utilities.Pdf.Writer.Operations.Set/SetTitle.md) | A Specialization of [`SetBase`](./iTin.Utilities.Pdf.Writer.Operations.Set/SetBase.md) class. Allows define the document's title. |

## iTin.Utilities.Pdf.Writer.SystemTag namespace

| public type | description |
| --- | --- |
| interface [ISystemTag](./iTin.Utilities.Pdf.Writer.SystemTag/ISystemTag.md) | Defines a generic System Tag. |
| class [PageNumberSystemTag](./iTin.Utilities.Pdf.Writer.SystemTag/PageNumberSystemTag.md) | Specialization of the [`ISystemTag`](./iTin.Utilities.Pdf.Writer.SystemTag/ISystemTag.md) interface that represents the system tag that allows to replace the page number in a document when merging documents. |
| enum [SystemTags](./iTin.Utilities.Pdf.Writer.SystemTag/SystemTags.md) | Defines the system replaceable tags. |
| class [SystemTagsCollection](./iTin.Utilities.Pdf.Writer.SystemTag/SystemTagsCollection.md) | Represents a collection of system tags |
| class [TotalPagesSystemTag](./iTin.Utilities.Pdf.Writer.SystemTag/TotalPagesSystemTag.md) | Specialization of the [`ISystemTag`](./iTin.Utilities.Pdf.Writer.SystemTag/ISystemTag.md) interface that represents the system tag that allows to replace the total page number in a document when merging documents. |

## iTin.Utilities.Pdf.Writer.TextStrategy namespace

| public type | description |
| --- | --- |
| class [LocationTextExtractionStrategy](./iTin.Utilities.Pdf.Writer.TextStrategy/LocationTextExtractionStrategy.md) | Class LocationTextExtractionStrategy. |

<!-- DO NOT EDIT: generated by xmldocmd for iTin.Utilities.Pdf.Writer.dll -->
