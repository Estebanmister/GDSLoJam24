using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static int nights=0;

    public void increaseNight()
    {
        nights++;
    }

    public bool getEnding()
    {
        if (nights == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
