
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class FOUR : AnimatedLetter
{
    public FOUR() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 114.36,102.91
                    C 107.09,104.18 30.91,115.09 5.27,123.45
                      60.91,55.82 78.36,36.55 89.09,24.91
                      87.27,100.73 85.27,132.73 95.09,163.09",
                PathLength: 450)

             ],
        letter: "4")
    {
    }

    protected override int ViewBoxWidth => 120;
}
