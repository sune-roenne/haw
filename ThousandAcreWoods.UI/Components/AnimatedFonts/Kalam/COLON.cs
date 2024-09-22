
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class COLON : AnimatedLetter
{
    public COLON() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 34.36,157.82
                    C 36.36,166.55 42.36,170.91 45.27,175.09",
                PathLength: 90),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 37.45,88.36
                    C 38.36,97.82 41.09,100.00 43.45,105.64",
                PathLength: 90)

             ],
        letter: "COLON")
    {
    }

    protected override int ViewBoxWidth => 80;
}
