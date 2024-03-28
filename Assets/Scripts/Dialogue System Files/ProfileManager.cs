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
    //happy, normal, stern
    public Sprite[] night;
    //happy, normal, stern
    public Sprite[] dawn;
    //happy, normal, stern
    public Sprite[] bloodyDawn;
    //happy, normal, angry, monster angry, blood happy
    public Sprite[] angel;
}
