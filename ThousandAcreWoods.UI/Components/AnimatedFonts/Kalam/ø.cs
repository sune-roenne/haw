
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class ø : AnimatedLetter
{
    public ø() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 86.13,86.74
                    C 74.17,66.30 44.55,83.91 35.82,101.00
                      26.55,118.64 14.36,146.55 36.36,160.36
                      64.91,172.18 90.19,121.69 92.36,107.27
                      93.44,98.06 86.22,86.87 86.22,86.87",
                PathLength: 295),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 99.36,74.36
                    C 99.36,74.36 26.91,150.00 16.27,166.09",
                PathLength: 150)

             ],
        letter: "ø")
    {
    }

    protected override int ViewBoxWidth => 120;
}
