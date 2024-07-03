using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication1;

public static  class JsonUtils
{
    public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
    {
        Converters = { new StringEnumConverter() },
        NullValueHandling = NullValueHandling.Ignore 
    };
}