# PdfObject.TryMergeInputsAsync method

Merges all [`PdfInput`](../PdfInput.md) entries asynchronously.

```csharp
public Task<OutputResult> TryMergeInputsAsync(CancellationToken cancellationToken = default)
```

| parameter | description |
| --- | --- |
| cancellationToken | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

## Return Value

A OutputResult reference that contains the result of the operation, to check if the operation is correct, the Success property will be true and the Result property will contain the Result; Otherwise, the the Success property will be false and the Errors property will contain the errors associated with the operation, if they have been filled in.

The type of the return Result is IOutputResultData, which contains the operation result

## See Also

* class [PdfObject](../PdfObject.md)
* namespace [iPdfWriter](../../iPdfWriter.md)

<!-- DO NOT EDIT: generated by xmldocmd for iPdfWriter.dll -->