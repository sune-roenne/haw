
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class ONE : AnimatedLetter
{
    public ONE() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 14.00,71.50
                    C 24.00,65.00 53.00,44.50 57.50,31.00
                      63.00,88.00 61.00,136.00 72.00,161.50",
                PathLength: 220)

             ],
        letter: "1")
    {
    }
    protected override int ViewBoxWidth => 120;
}
