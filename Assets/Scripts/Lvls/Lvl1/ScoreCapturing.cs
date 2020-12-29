using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCapturing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        
        switch (gameObject.tag) {
            case "Home":
                if (col.gameObject.CompareTag("Home")){
                    col.gameObject.SetActive(false);
                }
            break;
            case "Dona":
                if (col.gameObject.CompareTag("Dona")){
                    col.gameObject.SetActive(false);
                }
            break;
        }
    }
}
