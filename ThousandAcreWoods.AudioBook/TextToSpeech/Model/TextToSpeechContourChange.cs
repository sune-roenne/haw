using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model;
public record TextToSpeechContourChange(
    decimal ProgressInPercent,
    decimal ChangeInHertz
    );
