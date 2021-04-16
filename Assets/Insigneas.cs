using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Insigneas : MonoBehaviour
{

    public GameObject i1, i2, i3, i4;

    // Start is called before the first frame update
    void Start()
    {
        if (SerializationManager.Load() != null){
            SaveData.current = SerializationManager.Load();
        }

        if (SaveData.current.estrelles1 != 0){
            i1.SetActive(true);
        } else {
            i1.SetActive(false);
        }

        if (SaveData.current.estrelles2 != 0){
            i2.SetActive(true);
        } else {
            i2.SetActive(false);
        }

        if (SaveData.current.estrelles3 != 0){
            i3.SetActive(true);
        } else {
            i3.SetActive(false);
        }

        if (SaveData.current.estrelles4 != 0){
            i4.SetActive(true);
        } else {
            i4.SetActive(false);
        }
    }

    public void backToMap(){
        SceneManager.LoadScene("Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
