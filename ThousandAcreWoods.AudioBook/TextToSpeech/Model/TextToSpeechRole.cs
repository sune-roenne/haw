using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.Model;
public enum TextToSpeechRole
{
    //The voice imitates a girl.
    Girl = 1,
    //The voice imitates a boy.
    Boy = 2,
    //The voice imitates a young adult female.
    YoungAdultFemale = 3,
    //The voice imitates a young adult male.
    YoungAdultMale = 4,
    //The voice imitates an older adult female.
    OlderAdultFemale = 5,
    //The voice imitates an older adult male.
    OlderAdultMale = 6,
    //The voice imitates a senior female.
    SeniorFemale = 7,
    //The voice imitates a senior male.
    SeniorMale = 8
}
