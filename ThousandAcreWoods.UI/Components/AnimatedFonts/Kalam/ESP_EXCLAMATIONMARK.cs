
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class ESP_EXCLAMATIONMARK : AnimatedLetter
{
    public ESP_EXCLAMATIONMARK() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 39.25,74.25
                    C 45.25,89.25 43.50,152.75 38.00,173.00",
                PathLength: 200),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 32.25,39.50
                    C 34.00,44.00 38.75,44.25 39.50,51.00",
                PathLength: 80)

             ],
        letter: "ESP_EXCLAMATIONMARK")
    {
    }

    protected override int ViewBoxWidth => 80;
}
