
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class k : AnimatedLetter
{
    public k() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 40.36,14.55
                    C 44.73,20.55 43.09,36.18 43.09,41.82
                      43.09,47.45 23.82,142.00 16.91,156.00
                      27.82,149.27 78.36,84.18 90.18,71.45
                      76.18,90.91 61.82,104.18 56.36,112.18
                      49.09,120.73 54.36,130.91 56.73,137.27
                      59.09,143.64 72.55,158.73 85.27,161.45",
                PathLength: 395)
             ],
        letter: "k")
    {
    }
    protected override int ViewBoxWidth => 120;
}
