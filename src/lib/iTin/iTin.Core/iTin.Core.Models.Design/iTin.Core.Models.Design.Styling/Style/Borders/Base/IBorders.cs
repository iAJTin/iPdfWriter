
using System;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design.Styling
{
    /// <summary>
    /// Defines a generic style
    /// </summary>
    [JsonArray(AllowNullItems = true)]
    public interface IBorders : ICombinable<IBorders>, ICloneable
    {
    }
}
