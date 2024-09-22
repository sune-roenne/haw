
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class ESP_QUESTIONMARK : AnimatedLetter
{
    public ESP_QUESTIONMARK() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 39.75,75.25
                    C 40.00,87.25 36.75,105.50 29.50,117.00
                      22.25,128.50 7.50,139.00 8.00,151.75
                      8.50,164.50 33.25,159.75 50.00,148.25
                      66.75,136.75 68.75,132.75 72.25,130.25",
                PathLength: 300),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 43.25,44.00
                    C 43.25,44.00 41.00,51.75 39.50,53.25",
                PathLength: 50)


             ],
        letter: "ESP_QUESTIONMARK")
    {
    }

    protected override int ViewBoxWidth => 80;
}
