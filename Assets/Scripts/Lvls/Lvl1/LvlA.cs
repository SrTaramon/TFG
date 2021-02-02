using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LvlA : MonoBehaviour
{

    public List<GameObject> cards;

    private GameObject activeCard;

    public static bool active;

    public static int cardCount, points, errors;

    private int previousCount;

    private float time;

    public TextMeshProUGUI  timer, finalTime;

    private int min, sec;
    private int state; //0 = intro, 1 = game, 2 = outro

    public GameObject intro, action, outro, star1, star2, star3;

    void Start(){

        time = 0;
        cardCount = 0;
        previousCount = 0;
        points = 0;
        errors = 0;
        state = 0;

        intro.SetActive(true);
        
    }

    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        for (int i = 0; i < cards.Count; ++i){
            GameObject temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        intro.SetActive(false);
        activeCard = Instantiate(cards[0],gameObject.transform.position, Quaternion.identity);
        active = true;
        state = 1;
    }

    //Consultem l'estat en que ens trobem
    void Update(){

        switch (state) {
            case 0:
                Start();
                break;
            case 1:
                action.SetActive(true);
                gameAction();
                break;
            case 2:
                updateScore(min, sec, points, errors);
                outro.SetActive(true);
                PlayerPrefs.SetInt("LvlA", 1);
                break;
            default:
                break;
        }

        //Timer
        if (state != 2){
             time += Time.deltaTime;

            min = Mathf.FloorToInt(time / 60);
            sec = Mathf.FloorToInt(time % 60);
            timer.GetComponent<TextMeshProUGUI>().SetText(min.ToString("00") + ":" + sec.ToString("00"));
        }
        
    }

    private void gameAction() {

        if (!active && (cardCount != previousCount) && (cardCount < cards.Count)){
            Instantiate(cards[cardCount],gameObject.transform.position, Quaternion.identity);
            ++previousCount;
        }

        if (cardCount == cards.Count){
            StartCoroutine(waitForLastAnimation());
            
        }
    }

    IEnumerator waitForLastAnimation(){

        yield return new WaitForSeconds(3);

        action.SetActive(false);
        state = 2;
    }

    public void endLvl(){
        SceneManager.LoadScene("Map");
    }

    public void updateScore(int min, int sec, int points, int errors){

        if (points >= 4) { //1 estrella
            star1.SetActive(true);
        }
        if (points >= 10) { //2 estrellas
            star2.SetActive(true);
        }
        if (points == 17) { //3 estrellas
            star3.SetActive(true);
        }
        
        finalTime.GetComponent<TextMeshProUGUI>().SetText(min.ToString("00") + ":" + sec.ToString("00"));
    }
   
}
