# iPdfWriter assembly

## iPdfWriter namespace

| public type | description |
| --- | --- |
| static class [PdfFonts](./iPdfWriter/PdfFonts.md) | Represents |
| class [PdfInput](./iPdfWriter/PdfInput.md) | Represents a pdf file. |
| static class [PdfInputExtensions](./iPdfWriter/PdfInputExtensions.md) | Static class than contains common extension methods for objects of type [`PdfInput`](./iPdfWriter/PdfInput.md). |
| class [PdfObject](./iPdfWriter/PdfObject.md) | Represents a generic pdf object, this allows add pdf files to [`Items`](./iPdfWriter/PdfObject/Items.md) property and specify a user custom configuration. |

## iPdfWriter.ComponentModel namespace

| public type | description |
| --- | --- |
| enum [KnownInputType](./iPdfWriter.ComponentModel/KnownInputType.md) | Specifies the known input type. |
| enum [KnownOutputType](./iPdfWriter.ComponentModel/KnownOutputType.md) | Specifies the known output type. |
| class [PdfObjectConfig](./iPdfWriter.ComponentModel/PdfObjectConfig.md) | Represents configuration information for an object [`PdfObject`](./iPdfWriter/PdfObject.md). |

## iPdfWriter.Config namespace

| public type | description |
| --- | --- |
| interface [IPdfObjectConfig](./iPdfWriter.Config/IPdfObjectConfig.md) | Represents configuration information for an object [`PdfObject`](./iPdfWriter/PdfObject.md). |
| class [PdfOutputResultConfig](./iPdfWriter.Config/PdfOutputResultConfig.md) | Represents configuration information for an object [`PdfObject`](./iPdfWriter/PdfObject.md). |

## iPdfWriter.Input namespace

| public type | description |
| --- | --- |
| interface [IPdfInput](./iPdfWriter.Input/IPdfInput.md) | Defines a generic input |
| interface [IPdfInputAction](./iPdfWriter.Input/IPdfInputAction.md) |  |
| class [PdfInputAction](./iPdfWriter.Input/PdfInputAction.md) |  |
| class [PdfInputCache](./iPdfWriter.Input/PdfInputCache.md) |  |

## iPdfWriter.Operations.Actions namespace

| public type | description |
| --- | --- |
| class [SaveToFile](./iPdfWriter.Operations.Actions/SaveToFile.md) | Defines allowed actions for output result data |
| class [SaveToFileAsync](./iPdfWriter.Operations.Actions/SaveToFileAsync.md) | Defines allowed actions for output result data |

## iPdfWriter.Operations.Insert namespace

| public type | description |
| --- | --- |
| interface [IInsert](./iPdfWriter.Operations.Insert/IInsert.md) | Defines allowed actions for insert objects |
| class [InsertImage](./iPdfWriter.Operations.Insert/InsertImage.md) | A Specialization of [`InsertImageBase`](./iPdfWriter.Operations.Insert/InsertImageBase.md) class. Allows insert an image object. |
| abstract class [InsertImageBase](./iPdfWriter.Operations.Insert/InsertImageBase.md) | Specialization of [`IInsert`](./iPdfWriter.Operations.Insert/IInsert.md) interface. Acts as base class for insert image actions. |
| abstract class [InsertTextBase](./iPdfWriter.Operations.Insert/InsertTextBase.md) | Specialization of [`IInsert`](./iPdfWriter.Operations.Insert/IInsert.md) interface. Acts as base class for insert text actions. |

## iPdfWriter.Operations.Replace namespace

| public type | description |
| --- | --- |
| enum [EndLocationStrategy](./iPdfWriter.Operations.Replace/EndLocationStrategy.md) | Defines the strategy of the end point of the element to be inserted. |
| interface [IReplace](./iPdfWriter.Operations.Replace/IReplace.md) | Defines allowed actions for replacement objects |
| class [ReplaceSystemTag](./iPdfWriter.Operations.Replace/ReplaceSystemTag.md) | Specialization of [`IReplace`](./iPdfWriter.Operations.Replace/IReplace.md) interface. Contains the information for replace a text object. |
| class [ReplaceText](./iPdfWriter.Operations.Replace/ReplaceText.md) | Specialization of [`IReplace`](./iPdfWriter.Operations.Replace/IReplace.md) interface that contains the information for replace a text object. |
| class [ReplaceTextOptions](./iPdfWriter.Operations.Replace/ReplaceTextOptions.md) | Represents replace text options |
| enum [StartLocationStrategy](./iPdfWriter.Operations.Replace/StartLocationStrategy.md) | Defines the strategy of the start point of the element to be inserted. |
| enum [VerticalFineStrategy](./iPdfWriter.Operations.Replace/VerticalFineStrategy.md) | Defines the strategy of the fine vertical point of the element to be inserted. |

## iPdfWriter.Operations.Replace.Replacement namespace

| public type | description |
| --- | --- |
| interface [IReplacement](./iPdfWriter.Operations.Replace.Replacement/IReplacement.md) | Defines allowed actions for bookmark replacement object |

## iPdfWriter.Operations.Replace.Replacement.Text namespace

| public type | description |
| --- | --- |
| class [GlobalReplacementsCollection](./iPdfWriter.Operations.Replace.Replacement.Text/GlobalReplacementsCollection.md) | Represents a collection of system tags |
| interface [ISystemTagTextReplacement](./iPdfWriter.Operations.Replace.Replacement.Text/ISystemTagTextReplacement.md) | Defines allowed actions for text replacement object |
| interface [ITextReplacement](./iPdfWriter.Operations.Replace.Replacement.Text/ITextReplacement.md) | Defines allowed actions for text replacement object |
| abstract class [SystemTagTextReplacementBase](./iPdfWriter.Operations.Replace.Replacement.Text/SystemTagTextReplacementBase.md) | Specialization of [`IReplacement`](./iPdfWriter.Operations.Replace.Replacement/IReplacement.md) interface. Acts as base class for system tags replacement actions. |
| abstract class [TextReplacementBase](./iPdfWriter.Operations.Replace.Replacement.Text/TextReplacementBase.md) | Specialization of [`IReplacement`](./iPdfWriter.Operations.Replace.Replacement/IReplacement.md) interface. Acts as base class for text insert actions. |
| class [WithImageObject](./iPdfWriter.Operations.Replace.Replacement.Text/WithImageObject.md) | Specialization of [`TextReplacementBase`](./iPdfWriter.Operations.Replace.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with an image. |
| class [WithTableObject](./iPdfWriter.Operations.Replace.Replacement.Text/WithTableObject.md) | Specialization of [`TextReplacementBase`](./iPdfWriter.Operations.Replace.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with a pdf table. |
| class [WithTextObject](./iPdfWriter.Operations.Replace.Replacement.Text/WithTextObject.md) | Specialization of [`TextReplacementBase`](./iPdfWriter.Operations.Replace.Replacement.Text/TextReplacementBase.md) interface. Contains the logic to replace a text with another text. |

## iPdfWriter.Operations.Result.Insert namespace

| public type | description |
| --- | --- |
| class [InsertResultData](./iPdfWriter.Operations.Result.Insert/InsertResultData.md) | Represents insert data for an object InsertResult. |

## iPdfWriter.Operations.Result.Output namespace

| public type | description |
| --- | --- |
| interface [IPdfOutputResultData](./iPdfWriter.Operations.Result.Output/IPdfOutputResultData.md) | Represents configuration information for an object [`PdfOutputResultData`](./iPdfWriter.Operations.Result.Output/PdfOutputResultData.md). |
| class [PdfOutputResultData](./iPdfWriter.Operations.Result.Output/PdfOutputResultData.md) | Represents configuration information for an object [`PdfOutputResultData`](./iPdfWriter.Operations.Result.Output/PdfOutputResultData.md). |

## iPdfWriter.Operations.Result.Replace namespace

| public type | description |
| --- | --- |
| class [ReplaceResultData](./iPdfWriter.Operations.Result.Replace/ReplaceResultData.md) | Represents insert data for an object ReplaceResult. |

## iPdfWriter.Operations.Result.Set namespace

| public type | description |
| --- | --- |
| class [SetResultData](./iPdfWriter.Operations.Result.Set/SetResultData.md) | Represents set data for an object SetResult. |

## iPdfWriter.Operations.Set namespace

| public type | description |
| --- | --- |
| interface [ISet](./iPdfWriter.Operations.Set/ISet.md) |  |
| class [SetAuthor](./iPdfWriter.Operations.Set/SetAuthor.md) | A Specialization of [`SetBase`](./iPdfWriter.Operations.Set/SetBase.md) class. Allows define the document's author. |
| class [SetBase](./iPdfWriter.Operations.Set/SetBase.md) | A Specialization of [`ISet`](./iPdfWriter.Operations.Set/ISet.md) class. |
| class [SetCreator](./iPdfWriter.Operations.Set/SetCreator.md) | A Specialization of [`SetBase`](./iPdfWriter.Operations.Set/SetBase.md) class. Allows define the document's creator. |
| class [SetKeywords](./iPdfWriter.Operations.Set/SetKeywords.md) | A Specialization of [`SetBase`](./iPdfWriter.Operations.Set/SetBase.md) class. Allows define the document's keywords. |
| class [SetSubject](./iPdfWriter.Operations.Set/SetSubject.md) | A Specialization of [`SetBase`](./iPdfWriter.Operations.Set/SetBase.md) class. Allows define the document's subject. |
| class [SetTitle](./iPdfWriter.Operations.Set/SetTitle.md) | A Specialization of [`SetBase`](./iPdfWriter.Operations.Set/SetBase.md) class. Allows define the document's title. |

## iPdfWriter.SystemTag namespace

| public type | description |
| --- | --- |
| interface [ISystemTag](./iPdfWriter.SystemTag/ISystemTag.md) | Defines a generic System Tag. |
| class [PageNumberSystemTag](./iPdfWriter.SystemTag/PageNumberSystemTag.md) | Specialization of the [`ISystemTag`](./iPdfWriter.SystemTag/ISystemTag.md) interface that represents the system tag that allows to replace the page number in a document when merging documents. |
| enum [SystemTags](./iPdfWriter.SystemTag/SystemTags.md) | Defines the system replaceable tags. |
| class [SystemTagsCollection](./iPdfWriter.SystemTag/SystemTagsCollection.md) | Represents a collection of system tags |
| class [TotalPagesSystemTag](./iPdfWriter.SystemTag/TotalPagesSystemTag.md) | Specialization of the [`ISystemTag`](./iPdfWriter.SystemTag/ISystemTag.md) interface that represents the system tag that allows to replace the total page number in a document when merging documents. |

## iPdfWriter.TextStrategy namespace

| public type | description |
| --- | --- |
| class [LocationTextExtractionStrategy](./iPdfWriter.TextStrategy/LocationTextExtractionStrategy.md) | Class LocationTextExtractionStrategy. |

<!-- DO NOT EDIT: generated by xmldocmd for iPdfWriter.dll -->
