
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class TWO : AnimatedLetter
{
    public TWO() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 9.45,51.27
                    C 23.82,41.64 62.36,14.36 79.82,45.45
                      91.45,75.64 41.27,124.91 17.09,150.55
                      8.91,158.18 19.82,160.73 19.82,160.73
                      19.82,160.73 116.91,152.18 114.73,145.82",
                PathLength: 350)

             ],
        letter: "2")
    {
    }
    protected override int ViewBoxWidth => 120;
}
