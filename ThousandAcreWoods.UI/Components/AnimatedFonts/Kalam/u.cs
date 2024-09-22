
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class u : AnimatedLetter
{
    public u() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 45.00,71.75
                    C 24.75,109.50 20.00,128.25 23.00,148.00
                      31.25,175.75 59.41,143.16 72.91,128.91
                      87.59,113.20 89.55,87.09 93.82,72.00
                      90.32,92.27 81.09,123.18 73.18,159.73",
                PathLength: 355)
             ],
        letter: "u")
    {
    }
    protected override int ViewBoxWidth => 120;
}
