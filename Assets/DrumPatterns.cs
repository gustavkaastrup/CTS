using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrumPatterns : MonoBehaviour
{
    public bool[] EasyKickPatternIntro = new bool[8];
    public bool[] EasyKickPatternVerse = new bool[8];
    public bool[] EasyKickPatternChorus = new bool[8];
    public bool[] EasyKickPatternOutro = new bool[8];
    public bool[] EasySnarePatternIntro = new bool[8];
    public bool[] EasySnarePatternVerse = new bool[8];
    public bool[] EasySnarePatternChorus = new bool[8];
    public bool[] EasySnarePatternOutro = new bool[8];
    public bool[] EasyHiHatPatternIntro = new bool[8];
    public bool[] EasyHiHatPatternVerse = new bool[8];
    public bool[] EasyHiHatPatternChorus = new bool[8];
    public bool[] EasyHiHatPatternOutro = new bool[8];
    public bool[] MediumKickPatternIntro = new bool[8];
    public bool[] MediumKickPatternVerse = new bool[8];
    public bool[] MediumKickPatternChorus = new bool[8];
    public bool[] MediumKickPatternOutro = new bool[8];
    public bool[] MediumSnarePatternIntro = new bool[8];
    public bool[] MediumSnarePatternVerse = new bool[8];
    public bool[] MediumSnarePatternChorus = new bool[8];
    public bool[] MediumSnarePatternOutro = new bool[8];
    public bool[] MediumHiHatPatternIntro = new bool[8];
    public bool[] MediumHiHatPatternVerse = new bool[8];
    public bool[] MediumHiHatPatternChorus = new bool[8];
    public bool[] MediumHiHatPatternOutro = new bool[8];
    public bool[] HardKickPatternIntro = new bool[8];
    public bool[] HardKickPatternVerse = new bool[8];
    public bool[] HardKickPatternChorus = new bool[8];
    public bool[] HardKickPatternOutro = new bool[8];
    public bool[] HardSnarePatternIntro = new bool[8];
    public bool[] HardSnarePatternVerse = new bool[8];
    public bool[] HardSnarePatternChorus = new bool[8];
    public bool[] HardSnarePatternOutro = new bool[8];
    public bool[] HardHiHatPatternIntro = new bool[8];
    public bool[] HardHiHatPatternVerse = new bool[8];
    public bool[] HardHiHatPatternChorus = new bool[8];
    public bool[] HardHiHatPatternOutro = new bool[8];

    public bool[][] EasyKickPatterns;
    public bool[][] MediumKickPatterns;
    public bool[][] HardKickPatterns;
    public bool[][] EasySnarePatterns;
    public bool[][] MediumSnarePatterns;
    public bool[][] HardSnarePatterns;
    public bool[][] EasyHiHatPatterns;
    public bool[][] MediumHiHatPatterns;
    public bool[][] HardHiHatPatterns;

    public void Start()
    {
        EasyKickPatterns = new bool[][] { EasyKickPatternIntro, EasyKickPatternVerse, EasyKickPatternChorus, EasyKickPatternOutro };
        MediumKickPatterns = new bool[][] { MediumKickPatternIntro, MediumKickPatternVerse, MediumKickPatternChorus, MediumKickPatternOutro };
        HardKickPatterns = new bool[][] { HardKickPatternIntro, HardKickPatternVerse, HardKickPatternChorus, HardKickPatternOutro };
        EasySnarePatterns = new bool[][] { EasySnarePatternIntro, EasySnarePatternVerse, EasySnarePatternChorus, EasySnarePatternOutro };
        MediumSnarePatterns = new bool[][] { MediumSnarePatternIntro, MediumSnarePatternVerse, MediumSnarePatternChorus, MediumSnarePatternOutro };
        HardSnarePatterns = new bool[][] { HardSnarePatternIntro, HardSnarePatternVerse, HardSnarePatternChorus, HardSnarePatternOutro };
        EasyHiHatPatterns = new bool[][] { EasyHiHatPatternIntro, EasyHiHatPatternVerse, EasyHiHatPatternChorus, EasyHiHatPatternOutro };
        MediumHiHatPatterns = new bool[][] { MediumHiHatPatternIntro, MediumHiHatPatternVerse, MediumHiHatPatternChorus, MediumHiHatPatternOutro };
        HardHiHatPatterns = new bool[][] { HardHiHatPatternIntro, HardHiHatPatternVerse, HardHiHatPatternChorus, HardHiHatPatternOutro };

        DrumGameManagerScript.instance.EasyKickPatterns = EasyKickPatterns;
        DrumGameManagerScript.instance.MediumKickPatterns = MediumKickPatterns;
        DrumGameManagerScript.instance.HardKickPatterns = HardKickPatterns;
        DrumGameManagerScript.instance.EasySnarePatterns = EasySnarePatterns;
        DrumGameManagerScript.instance.MediumSnarePatterns = MediumSnarePatterns;
        DrumGameManagerScript.instance.HardSnarePatterns = HardSnarePatterns;
        DrumGameManagerScript.instance.EasyHiHatPatterns = EasyHiHatPatterns;
        DrumGameManagerScript.instance.MediumHiHatPatterns = MediumHiHatPatterns;
        DrumGameManagerScript.instance.HardHiHatPatterns = HardHiHatPatterns;
    }
}
