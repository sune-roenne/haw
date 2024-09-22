
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class w : AnimatedLetter
{
    public w() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 9.55,72.27
                    C 14.00,74.91 10.36,149.27 18.73,161.18
                      33.64,145.18 59.27,84.36 65.82,76.55
                      77.45,99.64 64.27,140.64 73.91,161.45
                      105.73,132.27 110.36,89.36 111.36,76.36",
                PathLength: 400)
             ],
        letter: "w")
    {
    }
    protected override int ViewBoxWidth => 120;
}
