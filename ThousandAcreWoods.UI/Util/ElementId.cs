namespace ThousandAcreWoods.UI.Util;

public class ElementId(string ElementName)
{
    private readonly long _id = UniqueId.NextId();
    private string? _stringId;
    public string Id
    {
        get {
            _stringId ??= $"taw-{ElementName}-{_id}";
            return _stringId;
        }
    }

    public string IdFor(string suffix) => $"{Id}-{suffix}";

    public static ElementId For(string ElementName) => new ElementId(ElementName);


}
