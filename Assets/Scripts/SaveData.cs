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

    public int temps1, temps2, temps3, temps4, temps5, temps6;

    public int estrelles1, estrelles2, estrelles3, estrelles4, estrelles5, estrelles6;

    public bool introDone, desblo5, desblo7, soundON;
}
