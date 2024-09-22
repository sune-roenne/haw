
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _M : AnimatedLetter
{
    public _M() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 9.45,162.36
                    C 9.45,162.36 39.20,52.07 35.56,34.44
                      48.03,37.25 62.01,111.90 69.47,116.81
                      76.92,121.72 139.91,36.14 141.00,39.78
                      142.09,43.42 115.09,164.91 115.09,164.91",
                PathLength: 470)


             ],
        letter: "M")
    {
    }

    protected override int ViewBoxWidth => 150;
}

