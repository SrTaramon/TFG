using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCapturing : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col){
        
        switch (gameObject.tag) {
            case "Home":
                if (col.gameObject.CompareTag("Home")){
                    destroyCard(col);
                    ImgMovement.correct = true;
                    ++LvlA.points;
                } else {
                    destroyCard(col);
                    ImgMovement.error = true;
                    ++LvlA.errors;
                }
            break;
            case "Dona":
                if (col.gameObject.CompareTag("Dona")){
                    destroyCard(col);
                    ImgMovement.correct = true;
                    ++LvlA.points;
                } else {
                    destroyCard(col);
                    ImgMovement.error = true;
                    ++LvlA.errors;
                }
            break;
        }
    }

    public void destroyCard(Collider2D col){
        col.GetComponent<BoxCollider2D>().enabled = !col.GetComponent<BoxCollider2D>().enabled;
        StartCoroutine(stopMovement());
        Destroy(col.gameObject, 3f);
        LvlA.active = false;
        ++LvlA.cardCount;
    }

    IEnumerator stopMovement(){

        yield return new WaitForSeconds(1);

        ImgMovement.alive = false;
    }
}
