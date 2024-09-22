
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class y : AnimatedLetter
{
    public y() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 52.91,69.45
                    C 34.55,102.36 24.55,122.36 32.91,149.09
                      42.00,168.91 69.27,136.91 84.91,116.55
                      95.64,100.73 104.18,71.27 104.18,71.27
                      104.18,71.27 71.27,173.45 64.55,186.00
                      57.82,198.55 34.36,191.45 30.91,189.27
                      27.45,187.09 22.55,181.64 19.09,167.09",
                PathLength: 395)
             ],
        letter: "y")
    {
    }
    protected override int ViewBoxWidth => 120;
}
