
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class c : AnimatedLetter
{
    public c() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 92.91,88.09
                    C 83.64,71.00 67.00,72.27 48.00,87.73
                      29.00,103.18 23.18,134.18 25.73,140.36
                      28.27,146.55 33.45,161.45 55.73,159.27
                      78.00,157.00 82.27,149.18 87.64,143.64",
                PathLength: 195)
             ],
        letter: "c")
    {
    }

    protected override int ViewBoxWidth => 120;
}
