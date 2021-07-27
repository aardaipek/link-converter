using System.Runtime.Serialization;

namespace LinkConverter.Model.Enums
{
    public enum UrlType
    {
        [EnumMember(Value = "")]
        NotSet = 0,

        [EnumMember(Value = "Product")]
        Product = 1,

        [EnumMember(Value = "Search")]
        Search = 2     
    }
}