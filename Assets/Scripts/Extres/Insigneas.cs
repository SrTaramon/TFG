using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Insigneas : MonoBehaviour
{

    public GameObject i1, i2, i3, i4, i5, i7, e1, e2, e3, panelIns, t1, t2, t3, t4, t5, t7;

    private bool celebration;

    public Text crono;


    // Start is called before the first frame update
    void Start()
    {
        celebration = false;

        if (SerializationManager.Load() != null){
            SaveData.current = SerializationManager.Load();
        }

        SaveData.current.new1 = false;
        SaveData.current.new2 = false;
        SaveData.current.new3 = false;
        SaveData.current.new4 = false;
        SaveData.current.new5 = false;
        SaveData.current.new7 = false;

        SerializationManager.Save(SaveData.current);
        

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
            if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Tada");
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
                t1.SetActive(true);
                crono.text = transformTime(SaveData.current.temps1);
                activateStarts(SaveData.current.estrelles1);
                break;
            case 2:
                t2.SetActive(true);
                crono.text = transformTime(SaveData.current.temps2);
                activateStarts(SaveData.current.estrelles2);
                break;
            case 3:
                t3.SetActive(true);
                crono.text = transformTime(SaveData.current.temps3);
                activateStarts(SaveData.current.estrelles3);
                break;
            case 4:
                t4.SetActive(true);
                crono.text = transformTime(SaveData.current.temps4);
                activateStarts(SaveData.current.estrelles4);
                break;
            case 5:
                t5.SetActive(true);
                crono.text = transformTime(SaveData.current.temps5);
                activateStarts(SaveData.current.estrelles5);
                break;
            case 7:
                t7.SetActive(true);
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
        crono.text = "";
        e1.SetActive(false);
        e2.SetActive(false);
        e3.SetActive(false);
        panelIns.SetActive(false);
        t1.SetActive(false);
        t2.SetActive(false);
        t3.SetActive(false);
        t4.SetActive(false);
        t5.SetActive(false);
        t7.SetActive(false);
    }
}
