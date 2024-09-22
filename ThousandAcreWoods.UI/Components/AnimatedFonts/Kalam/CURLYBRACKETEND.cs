
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class CURLYBRACKETEND : AnimatedLetter
{
    public CURLYBRACKETEND() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 30.55,6.73
                   C 44.09,7.91 61.73,5.91 63.36,32.82
                     64.09,45.18 46.91,58.45 40.18,68.09
                     45.36,72.27 59.09,73.18 66.64,78.36
                     62.82,86.91 48.00,94.00 43.18,101.36
                     43.00,111.82 58.73,118.45 61.64,133.27
                     64.55,148.09 37.82,166.27 30.64,174.73",
                PathLength: 500)

             ],
        letter: "CURLYBRACKETEND")
    {
    }
    protected override int ViewBoxWidth => 80;
}
