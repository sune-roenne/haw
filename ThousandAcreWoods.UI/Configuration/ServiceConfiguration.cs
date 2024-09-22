namespace ThousandAcreWoods.UI.Configuration;

public class ServiceConfiguration
{
    public const string ConfigurationElementName = "Service";

    public bool UseIIS { get; set; } = false;
    public string? PathBase { get; set; }


}
