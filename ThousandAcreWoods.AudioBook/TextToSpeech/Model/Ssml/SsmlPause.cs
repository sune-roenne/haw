﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model.Ssml;
public record SsmlPause(
    int DurationInMilliseconds
    ) : SsmlContent()
{
}
