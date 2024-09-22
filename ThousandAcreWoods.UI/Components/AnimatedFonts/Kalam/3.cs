
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class THREE : AnimatedLetter
{
    public THREE() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 8.55,42.36
                    C 21.64,25.45 48.00,9.82 79.45,26.36
                      98.55,41.27 67.82,68.36 61.64,71.64
                      55.45,74.91 10.73,102.36 21.09,92.36
                      49.27,82.36 80.36,73.27 97.82,85.82
                      115.82,104.00 106.91,131.64 80.73,147.82
                      70.18,154.36 45.27,161.09 32.91,161.45",
                PathLength: 550)

             ],
        letter: "3")
    {
    }
    protected override int ViewBoxWidth => 120;
}
