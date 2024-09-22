
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class h : AnimatedLetter
{
    public h() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 49.27,12.18
                    C 58.36,38.91 31.82,138.00 23.64,159.82
                      42.55,136.36 74.00,76.36 94.73,77.27
                      118.73,78.18 90.91,153.09 86.73,160.55",
                PathLength: 395)
             ],
        letter: "h")
    {
    }
    protected override int ViewBoxWidth => 120;
}
