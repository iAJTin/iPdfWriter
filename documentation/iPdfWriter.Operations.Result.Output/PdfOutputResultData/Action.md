# PdfOutputResultData.Action method (1 of 2)

Executes specified action for this output result instance.

```csharp
public IResult Action(IOutputAction output)
```

| parameter | description |
| --- | --- |
| output | Target output result. |

## Return Value

An instance which implements the IResult interface that contains the result of the operation, to check if the operation is correct, the Success property will be true and the Value property will contain the value; Otherwise, the the Success property will be false and the Errors property will contain the errors associated with the operation, if they have been filled in.

## See Also

* class [PdfOutputResultData](../PdfOutputResultData.md)
* namespace [iPdfWriter.Operations.Result.Output](../../iPdfWriter.md)

---

# PdfOutputResultData.Action method (2 of 2)

Executes specified action for this output result instance asynchronously.

```csharp
public Task<IResult> Action(IOutputActionAsync output, 
    CancellationToken cancellationToken = default)
```

| parameter | description |
| --- | --- |
| output | Target output result. |
| cancellationToken | A cancellation token that can be used by other objects or threads to receive notice of cancellation. |

## Return Value

An instance which implements the IResult interface that contains the result of the operation, to check if the operation is correct, the Success property will be true and the Value property will contain the value; Otherwise, the the Success property will be false and the Errors property will contain the errors associated with the operation, if they have been filled in.

## See Also

* class [PdfOutputResultData](../PdfOutputResultData.md)
* namespace [iPdfWriter.Operations.Result.Output](../../iPdfWriter.md)

<!-- DO NOT EDIT: generated by xmldocmd for iPdfWriter.dll -->