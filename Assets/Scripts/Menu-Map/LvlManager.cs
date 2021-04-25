using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{

    public GameObject a1, a2, a3, a4, al1, al2, al3, al4, m1, m2, m3, m4;

    public void goToInsigneas(){
        SceneManager.LoadScene("Insigneas");
    }
    // Start is called before the first frame update
    void Start(){
        SaveData.current = SerializationManager.Load();
        switch (SaveData.current.selectedAvatar){
            case 1:
                a1.SetActive(true);
                al1.SetActive(true);
                break;
            case 2:
                a2.SetActive(true);
                al2.SetActive(true);
                break;
            case 3:
                a3.SetActive(true);
                al3.SetActive(true);
                break;
            case 4:
                a4.SetActive(true);
                al4.SetActive(true);
                break;
            default:
                break;
        }

        if (SaveData.current.estrelles1 != 0) m1.SetActive(true);
        else m1.SetActive(false);
        if (SaveData.current.estrelles2 != 0) m2.SetActive(true);
        else m2.SetActive(false);
        if (SaveData.current.estrelles3 != 0) m3.SetActive(true);
        else m3.SetActive(false);
        if (SaveData.current.estrelles4 != 0) m4.SetActive(true);
        else m4.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
