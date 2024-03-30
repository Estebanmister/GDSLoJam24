using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueLine : MonoBehaviour
{
    public string speaker;
    public string spokenLine;
    public string profile;

    public DialogueLine(string speaker,string spokenLine,string profile)
    {
        this.speaker = speaker;
        this.spokenLine = spokenLine;
        this.profile = profile;
    }

    public void runLine(ProfileManager profile, TMP_Text nameText, TMP_Text dialogueText)
    {
        //change name 
        nameText.text = speaker;

        //change profile
        char[] letters = (this.profile+"   ").ToCharArray();
        if (speaker == "Axel")
        {
            profile.setAxel(letters[0],letters[1],letters[2]);

        }else if (speaker == "Angel")
        {
            profile.setAngel(letters[0],letters[1]);
        }
        else
        {
            profile.setVillager();
        }
        dialogueText.text = spokenLine;
    }
}
