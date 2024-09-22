
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class l : AnimatedLetter
{
    public l() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 45.82,17.45
                    C 58.36,61.45 27.82,136.18 26.18,159.45",
                PathLength: 170)
             ],
        letter: "l")
    {
    }
    protected override int ViewBoxWidth => 70;
}
