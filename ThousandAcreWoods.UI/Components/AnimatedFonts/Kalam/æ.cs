
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class æ : AnimatedLetter
{
    public æ() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 79.27,110.18
                    C 86.55,97.27 73.82,72.00 55.45,77.82
                      37.82,84.73 18.18,114.00 14.55,140.36
                      10.91,166.73 46.55,167.27 68.00,132.73
                      86.55,104.91 90.36,82.91 106.91,78.55
                      133.27,72.91 141.27,104.55 134.91,112.73
                      128.55,120.91 97.09,123.45 77.09,114.91
                      61.45,150.55 81.45,161.27 103.27,159.82
                      115.82,158.73 127.09,154.00 131.45,150.55",
                PathLength: 600)
             ],
        letter: "æ")
    {
    }
    protected override int ViewBoxWidth => 150;
}
