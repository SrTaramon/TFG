using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    Animator animator;

    void Start(){
        
        animator = gameObject.GetComponent<Animator>();

    }

    void OnMouseDown(){
        animator.SetBool("clicked", true);
        StartCoroutine(waitForAnimationEnd());
    }

    IEnumerator waitForAnimationEnd(){
        yield return new WaitForSeconds(1);
        animator.SetBool("clicked", false);
        switch (gameObject.name){
            case "Cierto":
                Lvl3.cert = true;
                break;
            case "Falso":
                Lvl3.fals = true;
                break;
            default:
                break;
        }
    }
}
