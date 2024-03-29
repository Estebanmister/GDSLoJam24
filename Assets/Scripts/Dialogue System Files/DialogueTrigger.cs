using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManagerScript ManagerSan;

    public void triggerDialogue()
    {
        ManagerSan.setUp();
        ManagerSan.runDialogue();
    }
}
