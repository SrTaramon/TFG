using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlA : MonoBehaviour
{

    public List<GameObject> cards;

    private GameObject activeCard;

    public static bool active;

    public static int cardCount;

    private int previousCount;

    void Start(){

        cardCount = 0;
        previousCount = 0;

        for (int i = 0; i < cards.Count; ++i){
            GameObject temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        activeCard = Instantiate(cards[0],gameObject.transform.position, Quaternion.identity);
        active = true;
    }

    void Update(){
        if (!active && (cardCount != previousCount) && (cardCount < cards.Count)){
            Instantiate(cards[cardCount],gameObject.transform.position, Quaternion.identity);
            ++previousCount;
        }

    }
   
}
