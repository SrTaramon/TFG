﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Insigneas : MonoBehaviour
{

    public GameObject i1, i2, i3, i4, i5, i7, e1, e2, e3, panelIns;

    private bool celebration;

    public Text crono, titol;

    private string t1 = "Cartas reproductoras";
    private string t2 = "Construcción de símbolos";
    private string t4 = "Pelotas emocionales";
    private string t3 = "Destruyendo mitos";
    private string t5 = "Fuga de letras";
    private string t7 = "Memory";


    // Start is called before the first frame update
    void Start()
    {
        celebration = false;

        if (SerializationManager.Load() != null){
            SaveData.current = SerializationManager.Load();
        }

        if (SaveData.current.estrelles1 != 0){
            i1.SetActive(true);
            celebration = true;
        } else {
            i1.SetActive(false);
        }

        if (SaveData.current.estrelles2 != 0){
            i2.SetActive(true);
            celebration = true;
        } else {
            i2.SetActive(false);
        }

        if (SaveData.current.estrelles3 != 0){
            i3.SetActive(true);
            celebration = true;
        } else {
            i3.SetActive(false);
        }

        if (SaveData.current.estrelles4 != 0){
            i4.SetActive(true);
            celebration = true;
        } else {
            i4.SetActive(false);
        }

        if (SaveData.current.estrelles5 != 0){
            i5.SetActive(true);
            celebration = true;
        } else {
            i5.SetActive(false);
        }

        if (SaveData.current.estrelles7 != 0){
            i7.SetActive(true);
            celebration = true;
        } else {
            i7.SetActive(false);
        }

        if (celebration){
            if (LvlManager.soundON) FindObjectOfType<AudioManager>().Play("Tada");
        } 
    }

    public void backToMap(){
        SceneManager.LoadScene("Map");
    }

    // Update is called once per frame
    public void infoInsignea(Button button){
        int id = Int32.Parse(button.gameObject.name);
        switch (id){
            case 1:
                titol.text = t1;
                crono.text = transformTime(SaveData.current.temps1);
                activateStarts(SaveData.current.estrelles1);
                break;
            case 2:
                titol.text = t2;
                crono.text = transformTime(SaveData.current.temps2);
                activateStarts(SaveData.current.estrelles2);
                break;
            case 3:
                titol.text = t3;
                crono.text = transformTime(SaveData.current.temps3);
                activateStarts(SaveData.current.estrelles3);
                break;
            case 4:
                titol.text = t4;
                crono.text = transformTime(SaveData.current.temps4);
                activateStarts(SaveData.current.estrelles4);
                break;
            case 5:
                titol.text = t5;
                crono.text = transformTime(SaveData.current.temps5);
                activateStarts(SaveData.current.estrelles5);
                break;
            case 7:
                titol.text = t7;
                crono.text = transformTime(SaveData.current.temps7);
                activateStarts(SaveData.current.estrelles7);
                break;
            default:
                break;
        }
        panelIns.SetActive(true);
    }

    private string transformTime(int timeAc){
        int min = (timeAc / 60);
        int sec = (timeAc % 60);
        if (timeAc >= 60){
            if (sec <= 9) return min.ToString() + ":0" + sec.ToString() + "min";
            else return min.ToString() + ":" + sec.ToString() + "min";
        } else {
            return "0:" + timeAc.ToString() + "s";
        }
    }

    private void activateStarts(int stars){
        switch (stars){
            case 1:
                e1.SetActive(true);
                break;
            case 2:
                e1.SetActive(true);
                e2.SetActive(true);
                break;
            case 3:
                e1.SetActive(true);
                e2.SetActive(true);
                e3.SetActive(true);
                break;
        }
    }

    public void stopInfo(){
        titol.text = "";
        crono.text = "";
        e1.SetActive(false);
        e2.SetActive(false);
        e3.SetActive(false);
        panelIns.SetActive(false);
    }
}
