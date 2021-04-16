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

    public static int cardCount, points, errors, counter;

    private int previousCount;

    private float time;

    public Text  timer, finalTime, counterText;

    private int min, sec, estrelles;
    private int state; //0 = intro, 1 = intro, 2 = game, 3 = outro

    public GameObject action, game, outro, pause, introexp, star1, star2, star3, record;

    private bool done, restart, gameOver;

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
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                //PlayerPrefs.SetInt("Lvl1", 1);
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

        counterText.text = counter.ToString();

        /* switch(counter){
            case 5:
                counterText.text = "5";
                break;
            case 4:
                counterText.text = "4";
                break;
            case 3:
                counterText.text = "3";
                break;
            case 2:
                counterText.text = "2";
                break;
            case 1:
                counterText.text = "1";
                break;
            case 0:
                counterText.text = "0";
                StartCoroutine(waitForGameOver());
                break;
            default:
                break;
        } */
        
    }

    private void cleanLvl(){
        gameOver = false;
        counter = 5;
        counterText.text = "5";
        state = 0;
        estrelles = 0;
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

    IEnumerator waitForGameOver(){

        //play lose music
        yield return new WaitForSeconds(2);

        if (activeCard != null){
            activeCard.SetActive(false);
        }
        gameOver = true;
        min = 0;
        sec = 0;
        points = 0;
        state = 3;
    }

    IEnumerator waitForCardDisappear(){

        yield return new WaitForSeconds(3);

        activeCard = Instantiate(cards[cardCount],gameObject.transform.position, Quaternion.identity);
        
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

        //mitja test1 1:26min
        if (!gameOver){
            if (min < 1 || (min == 1 && sec <= 20)) { //3 estrella
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                estrelles = 3;
            }
            else if (min < 2) { //2 estrellas
                star1.SetActive(true);
                star2.SetActive(true);
                estrelles = 2;
            }
            else { //1 estrellas
                star1.SetActive(true);
                estrelles = 1;
            }
        }

        saveScore(estrelles, (min * 60) + sec);
        
        finalTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    private void saveScore(int estrelles, int temps){
        SaveData.current = SerializationManager.Load();
        if (temps < SaveData.current.temps1){
            SaveData.current.temps1 = temps;
            record.SetActive(true);
        } else {
            record.SetActive(false);
        }
        if (estrelles > SaveData.current.estrelles1) SaveData.current.estrelles1 = estrelles;
        SerializationManager.Save(SaveData.current);
    }
   
}
