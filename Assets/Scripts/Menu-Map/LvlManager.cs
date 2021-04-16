using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{

    public void goToInsigneas(){
        SceneManager.LoadScene("Insigneas");
    }
    // Start is called before the first frame update
    void Start(){
        Debug.Log(SaveData.current.estrelles1);
        Debug.Log(SaveData.current.temps1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
