
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class b : AnimatedLetter
{
    public b() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 38.82,13.55
                    C 42.00,92.45 27.00,100.00 15.36,163.45
                      25.27,125.36 70.64,73.00 84.73,76.18
                      98.82,79.36 101.55,106.55 96.36,120.18
                      91.18,133.82 70.64,156.82 65.09,158.91
                      59.55,161.00 43.82,167.36 24.55,145.00",
                PathLength: 520)
             ],
        letter: "b")
    {
    }

    protected override int ViewBoxWidth => 120;
}
