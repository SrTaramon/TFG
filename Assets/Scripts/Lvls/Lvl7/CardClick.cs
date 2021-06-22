using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardClick : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Lvl7.state == 2){
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnMouseDown() {
        if (Lvl7.clickCount <= 2){
            gameObject.SetActive(false);
            int carta = Int32.Parse(gameObject.name);
            Lvl7.parella.Add(--carta);
            ++Lvl7.clickCount;
        }
    }
}
