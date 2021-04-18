using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class AvatarPick : MonoBehaviour
{

    public GameObject panelIntro, panelAvatar;

    void Start(){
        panelIntro.SetActive(true);
        panelAvatar.SetActive(false);
    }

    public void activateAvatarSelection(){
        panelIntro.SetActive(false);
        panelAvatar.SetActive(true);
    }

    public void pickAvatar(Button choice){
        SaveData.current.selectedAvatar = Int32.Parse(choice.gameObject.name);
        SaveData.current.introDone = true;
        SerializationManager.Save(SaveData.current);
        SceneManager.LoadScene("Map");
    }
}
