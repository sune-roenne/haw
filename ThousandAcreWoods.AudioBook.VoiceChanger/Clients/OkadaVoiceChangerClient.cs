using ThousandAcreWoods.Language.Extensions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThousandAcreWoods.AudioBook.VoiceChanger.Clients;
public partial class OkadaVoiceChangerClient : IOkadaVoiceChangerClient
{
    static partial void UpdateJsonSerializerSettings(System.Text.Json.JsonSerializerOptions settings)
    {
        settings.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        settings.Converters.Add(new JsonEnumMemberStringEnumConverter(allowIntegerValues: true, namingPolicy: JsonNamingPolicy.SnakeCaseLower));
        settings.IncludeFields = true;
        
    }

}


public class JsonEnumMemberStringEnumConverter : JsonConverterFactory
{
    private readonly JsonNamingPolicy? _namingPolicy;
    private readonly bool _allowIntegerValues;
    private readonly JsonStringEnumConverter _baseConverter;

    public JsonEnumMemberStringEnumConverter() : this(true, null) { }

    public JsonEnumMemberStringEnumConverter(bool allowIntegerValues, JsonNamingPolicy? namingPolicy)
    {
        _allowIntegerValues = allowIntegerValues;
        _namingPolicy = namingPolicy;
        _baseConverter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);

    }

    public override bool CanConvert(Type typeToConvert) => 
        _baseConverter.CanConvert(typeToConvert);


    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var publicStaticFields = typeToConvert.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(_ => (Attr: _.GetCustomAttribute<EnumMemberAttribute>(), Field: _))
            .Where(_ => _.Attr != null && _.Attr.Value != null)
            .ToDictionarySafe(_ => _.Field.Name, _ => _.Attr!.Value!);
        if(publicStaticFields.Any())
        {
            var returnee = new JsonStringEnumConverter(
                namingPolicy: new DictionaryLookupNamingPolicy(publicStaticFields, _namingPolicy), 
                allowIntegerValues: _allowIntegerValues
            ).CreateConverter(typeToConvert, options);
            return returnee;
        }
        return _baseConverter.CreateConverter(typeToConvert, options);

    }
}

public class JsonNamingPolicyDecorator : JsonNamingPolicy
{
    readonly JsonNamingPolicy? underlyingNamingPolicy;

    public JsonNamingPolicyDecorator(JsonNamingPolicy? underlyingNamingPolicy) =>
        this.underlyingNamingPolicy = underlyingNamingPolicy;
    public override string ConvertName(string name) => underlyingNamingPolicy?.ConvertName(name) ?? name;
}

internal class DictionaryLookupNamingPolicy : JsonNamingPolicyDecorator
{
    readonly Dictionary<string, string> dictionary;

    public DictionaryLookupNamingPolicy(Dictionary<string, string> dictionary, JsonNamingPolicy? underlyingNamingPolicy) : base(underlyingNamingPolicy) => 
        this.dictionary = dictionary ?? 
        throw new ArgumentNullException();
    public override string ConvertName(string name) => dictionary.TryGetValue(name, out var value) ? value : base.ConvertName(name);
}