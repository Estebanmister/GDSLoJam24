using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ProfileManager : MonoBehaviour
{
    public Image profile;

    //happy, normal, stern
    public Sprite[] day;
    public Sprite[] night;
    public Sprite[] dawn;
    public Sprite[] bloodyDawn;

    //happy, normal, angry, monster angry, blood happy
    public Sprite[] angel;
    //
    public Sprite[] villager;

    public void setAxel(char scene, char mood,char blood)
    {
        //mood = 'h' for happy, 'n' for normal, 's' for stern
        int index;
        if (mood == 'h')
        {
            index = 0;
        } else if (mood == 's') {
            index = 2;
        }
        else
        {
            index = 3;
        }

        //scene = 'd' for day, 'n' for night, 'e' for dawn
        if (scene=='d')
        {
            profile.sprite = day[index];
        }else if(scene == 'n'){
            profile.sprite = night[index];
        }
        else if(scene == 'e')
        {
            //bloody version of dawn 
            if (blood == 'b')
            {
                profile.sprite = bloodyDawn[index];
            }
            else
            {
                profile.sprite = dawn[index];
            }
        }
        else
        {
            return;
        }
    }

    public void setAngel(char mood,char modifier)
    {
        //mood = 'h' for happy, 'n' for normal, 'a' for angry
        //modifer = 'm' for monster, 'b' for blood 
        if (mood == 'h')
        {
            if (modifier == 'b')
            {
                //bloody happy 
                profile.sprite = angel[4];
            }
            else
            {
                //happy
                profile.sprite = angel[0];
            }
        }else if (mood == 'a')
        {
            if(modifier == 'm')
            {
                //angry monster
                profile.sprite = angel[3];
            }
            else
            {
                //angry
                profile.sprite = angel[2];
            }
        }
        else if(mood=='n')
        {
            //normal
            profile.sprite = angel[1];
        }
        else
        {
            return;
        }
    }

    public void setVillager()
    {
        profile.sprite = villager[1];
    }
}
