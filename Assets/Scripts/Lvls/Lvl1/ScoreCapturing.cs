using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCapturing : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col){
        
        switch (gameObject.tag) {
            case "Home":
                if (col.gameObject.CompareTag("Home")){
                    ImgMovement.correct = true;
                    col.GetComponent<BoxCollider2D>().enabled = !col.GetComponent<BoxCollider2D>().enabled;
                    Destroy(col.gameObject, 3f);
                    LvlA.active = false;
                    ++LvlA.cardCount;
                    ++LvlA.points;
                } else {
                    col.GetComponent<BoxCollider2D>().enabled = !col.GetComponent<BoxCollider2D>().enabled;
                    ImgMovement.error = true;
                    Destroy(col.gameObject, 3f);
                    LvlA.active = false;
                    ++LvlA.cardCount;
                    ++LvlA.errors;
                }
            break;
            case "Dona":
                if (col.gameObject.CompareTag("Dona")){
                    ImgMovement.correct = true;
                    col.GetComponent<BoxCollider2D>().enabled = !col.GetComponent<BoxCollider2D>().enabled;
                    Destroy(col.gameObject, 3f);
                    LvlA.active = false;
                    ++LvlA.cardCount;
                    ++LvlA.points;
                } else {
                    col.GetComponent<BoxCollider2D>().enabled = !col.GetComponent<BoxCollider2D>().enabled;
                    ImgMovement.error = true;
                    Destroy(col.gameObject, 3f);
                    LvlA.active = false;
                    ++LvlA.cardCount;
                    ++LvlA.errors;
                }
            break;
        }
    }
}
