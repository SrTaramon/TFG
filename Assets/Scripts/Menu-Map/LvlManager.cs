using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{

    void Update(){
        if (PlayerPrefs.GetInt("LvlA") == 1){
            Debug.Log("lvl completed");
        }
    }
    // Start is called before the first frame update
    void OnMouseDown(){
        SceneManager.LoadScene(gameObject.name);
    }
}
