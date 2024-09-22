
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class FIVE : AnimatedLetter
{
    public FIVE() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 23.27,46.18
                    C 24.73,60.00 18.00,88.18 16.00,98.36
                      49.82,89.64 84.55,72.00 94.73,97.45
                      102.36,123.27 59.27,157.09 37.45,160.73",
                PathLength: 355),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 20.18,46.73
                    C 41.82,35.27 63.45,27.09 96.73,24.18",
                PathLength: 100)

             ],
        letter: "5")
    {
    }

    protected override int ViewBoxWidth => 120;
}
