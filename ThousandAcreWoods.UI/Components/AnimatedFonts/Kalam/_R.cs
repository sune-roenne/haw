
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _R : AnimatedLetter
{
    public _R() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 38.88,40.38
                       C 48.50,44.00 38.62,86.88 35.75,98.25
                         32.88,109.62 18.50,163.38 18.50,163.38",
                PathLength: 140),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 36.75,41.62
                       C 36.75,41.62 63.25,25.88 89.38,34.25
                         115.50,42.62 104.77,76.95 91.27,82.33
                         75.88,90.62 71.25,92.66 65.39,95.45
                         61.19,97.44 44.25,104.17 42.75,103.50
                         59.16,132.91 107.81,165.77 106.75,163.88",
                PathLength: 300)
             ],
        letter: "R")
    {
    }
    protected override int ViewBoxWidth => 120;
}

