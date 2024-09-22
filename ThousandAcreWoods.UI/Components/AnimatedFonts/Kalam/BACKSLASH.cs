
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class BACKSLASH : AnimatedLetter
{
    public BACKSLASH() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 12.67,10.33
                    C 15.67,31.33 57.33,146.67 58.67,178.00",
                PathLength: 190)

             ],
        letter: "BACKSLASH")
    {
    }

    protected override int ViewBoxWidth => 80;
}
