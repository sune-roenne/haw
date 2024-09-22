
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _P : AnimatedLetter
{
    public _P() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 42.67,36.67
                       C 57.35,41.40 23.33,162.33 23.33,162.33",
                PathLength: 150),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 41.12,38.50
                       C 41.12,38.50 77.50,18.25 104.38,44.62
                         118.85,72.44 92.87,86.95 91.12,89.56
                         81.00,97.00 55.38,106.50 37.12,110.38",
                PathLength: 200)


             ],
        letter: "P")
    {
    }
    protected override int ViewBoxWidth => 120;
}

