using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace ThousandAcreWoods.LocalStorage.Story;
internal class StoryLoaderDefaultTypeInfoResolver : IJsonTypeInfoResolver
{
    public JsonTypeInfo? GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
