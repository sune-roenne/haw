
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class BRACKETEND : AnimatedLetter
{
    public BRACKETEND() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 21.00,16.33
                   C 47.33,14.33 50.00,11.67 58.00,11.67
                     61.00,48.67 54.33,88.33 62.33,150.67
                     58.67,155.67 46.33,152.00 29.33,166.67",
                PathLength: 250)

             ],
        letter: "BRACKETEND")
    {
    }

    protected override int ViewBoxWidth => 80;
}
