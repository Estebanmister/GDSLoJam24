using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxManager : MonoBehaviour
{
    public Image box;

    public Sprite day;
    public Sprite night;
    public Sprite dawn;

    public void setDay()
    {
        box.sprite = day;
    }

    public void setNight()
    {
        box.sprite = night;
    }

    public void setDawn()
    {
        box.sprite = dawn;
    }

    public void setBox(char color)
    {
        if (color == 'd')
        {
            setDay();
        }else if(color == 'n')
        {
            setNight();
        }
        else
        {
            setDawn();
        }
    }
}
