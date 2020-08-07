
namespace iTin.Core.Models.Design.Styling
{
    using System;
    
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a generic style
    /// </summary>
    [JsonArray(AllowNullItems = true)]
    public interface IBorders : ICombinable<IBorders>, ICloneable
    {
    }
}
