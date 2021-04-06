using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LvlA : MonoBehaviour
{

    public List<GameObject> cards;

    private GameObject activeCard;

    public static bool active;

    public static int cardCount, points, errors;

    private int previousCount;

    private float time;

    public Text  timer, finalTime;

    private int min, sec;
    private int state; //0 = intro, 1 = intro, 2 = game, 3 = outro

    public GameObject action, game, outro, pause, introexp, star1, star2, star3;

    private bool done, restart;

    void Start(){

        time = 0;
        cardCount = 0;
        previousCount = 0;
        points = 0;
        errors = 0;

        restart = false;

        introexp.SetActive(true);

        cleanLvl();
        
    }

    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        for (int i = 0; i < cards.Count; ++i){
            GameObject temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        time = 0;
        cardCount = 0;
        previousCount = 0;
        points = 0;
        errors = 0;

        introexp.SetActive(false);

        if (!restart){
            activeCard = Instantiate(cards[0],gameObject.transform.position, Quaternion.identity);
        }

        active = true;
        done = true;
        state = 1;
    }

    //Consultem l'estat en que ens trobem
    void Update(){

        switch (state) {
            case 0:
                Start();
                break;
            case 1:
                game.SetActive(true);
                action.SetActive(true);
                gameAction();
                break;
            case 2:
                game.SetActive(false);
                action.SetActive(false);
                pause.SetActive(true);
                break;
            case 3:
                updateScore(min, sec, points, errors);
                outro.SetActive(true);
                PlayerPrefs.SetInt("Lvl1", 1);
                break;
            default:
                break;
        }

        //Timer
        if (state == 1){
             time += Time.deltaTime;

            min = Mathf.FloorToInt(time / 60);
            sec = Mathf.FloorToInt(time % 60);
            timer.text = min.ToString("00") + ":" + sec.ToString("00");
        }
        
    }

    private void cleanLvl(){
        state = 0;
        pause.SetActive(false);
        action.SetActive(false);
        game.SetActive(false);
        outro.SetActive(false);
    }

    private void gameAction() {

        if (!active && (cardCount != previousCount) && (cardCount < 10) && done){
            done = false;
            StartCoroutine(waitForCardDisappear());
        }

        if (cardCount == 10){
            StartCoroutine(waitForLastAnimation());
            
        }

        restart = false;
    }

    IEnumerator waitForCardDisappear(){

        yield return new WaitForSeconds(3);

        Instantiate(cards[cardCount],gameObject.transform.position, Quaternion.identity);
        
        done = true;
        ++previousCount;
    }

    IEnumerator waitForLastAnimation(){

        yield return new WaitForSeconds(3);

        game.SetActive(false);
        action.SetActive(false);
        state = 3;
    }

    public void endLvl(){
        pause.SetActive(false);
        SceneManager.LoadScene("Map");
    }

    public void backToLvl(){
        pause.SetActive(false);
        state = 1;
    }

    public void restartLvl(){
        pause.SetActive(false);
        restart = true;
        startGame();
    }

    public void pauseActive(){
        state = 2;
    }

    public void updateScore(int min, int sec, int points, int errors){

        if (points >= 3) { //1 estrella
            star1.SetActive(true);
        }
        if (points >= 7) { //2 estrellas
            star2.SetActive(true);
        }
        if (points == 10) { //3 estrellas
            star3.SetActive(true);
        }
        
        finalTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }
   
}
