using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;

    public static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            if(value != null)
            {
                _current = value;
            }
        }
    }

    public int selectedAvatar; //1, 2, 3, 4
    public int min1, min2, min3, min4, min5, min6;

    public int sec1, sec2, sec3, sec4, sec5, sec6;

    public int estrelles1, estrelles2, estrelles3, estrelles4, estrelles5, estrelles6;
}
