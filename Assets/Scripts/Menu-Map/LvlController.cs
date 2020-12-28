using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown(){
        SceneManager.LoadScene(gameObject.name);
    }
}
