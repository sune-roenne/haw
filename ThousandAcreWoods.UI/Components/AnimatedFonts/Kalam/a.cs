
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class a : AnimatedLetter
{
    public a() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                     M 90.91,96.91
                     C 92.00,90.18 94.36,82.36 87.09,75.09
                       79.82,67.82 52.00,72.55 36.55,97.82
                       21.09,123.09 26.18,139.82 28.18,144.91
                       30.18,150.00 44.36,159.82 60.55,149.27
                       76.73,138.73 90.45,103.45 90.82,97.36
                       91.09,129.55 86.36,159.00 86.36,159.00",
                PathLength: 520)
             ],
        letter: "a")
    {
    }
    protected override int ViewBoxWidth => 120;
}
