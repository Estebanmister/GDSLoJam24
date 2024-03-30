using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManagerScript : MonoBehaviour
{
    public GameObject visualNovel;
    public ProfileManager profile;
    public DialogueBoxManager Scene;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public char theme;//'d','n','e'
    public int lines;
    public string[] names;
    public string[] sentences;
    public string[] animations;
    public DialogueLine[] dialogue;


    public void setUp()
    {
        dialogue = new DialogueLine[lines];

        for (int i = 0; i < lines; i++)
        {
            dialogue[i] = new DialogueLine(names[i],sentences[i],animations[i]);
        }
    }

    public void runDialogue()
    {
        visualNovel.SetActive(true);
        Scene.setBox(theme);
        for(int i = 0; i < lines; i++)
        {
            dialogue[i].runLine(profile,nameText,dialogueText);
        }
        //visualNovel.SetActive(false);
    }
}
