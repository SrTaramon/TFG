using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{

    void Update(){
        Debug.Log(SaveData.current.estrelles1);
    }
    // Start is called before the first frame update
    void OnMouseDown(){
        SceneManager.LoadScene(gameObject.name);
    }
}
