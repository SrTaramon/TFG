using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvlA : MonoBehaviour
{

    public List<GameObject> cards;

    private GameObject activeCard;

    public static bool active;

    public static int cardCount;

    private int previousCount;

    private float time;

    public TextMeshProUGUI  timer;

    void Start(){

        time = 0;
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
            //ImgMovement.animation = false;
            Instantiate(cards[cardCount],gameObject.transform.position, Quaternion.identity);
            ++previousCount;
        }

        time += Time.deltaTime;

        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        timer.GetComponent<TextMeshProUGUI>().SetText(min.ToString("00") + ":" + sec.ToString("00"));
        
    }
   
}
