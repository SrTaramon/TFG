using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl2 : MonoBehaviour
{
    public List<GameObject> peces, initialPos, instantiates;

    private GameObject activeCard;

    public static int cardCount, points, errors;

    private int previousCount;

    private float time;

    public Text  timer, finalTime;

    private int min, sec;
    public static int state, numCorPieces; //0 = intro, 1 = game, 2 = pause, 3 = outro

    public GameObject action, game, outro, pause, introexp, star1, star2, star3;

    private bool done;

    void Start(){

        time = 0;
        numCorPieces = 0;

        introexp.SetActive(true);
        
    }

    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        time = 0;
        points = 0;
        errors = 0;
        introexp.SetActive(false);

        for (int i = 0; i < peces.Count; ++i){
            instantiates[i] = Instantiate(peces[i], initialPos[i].transform.position, Quaternion.identity);
        }

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
                action.SetActive(true);
                game.SetActive(true);
                PecesMovement.paused = false;
                gameAction();
                break;
            case 2:
                action.SetActive(false);
                pause.SetActive(true);
                PecesMovement.paused = true;
                break;
            case 3:
                updateScore(min, sec, points, errors);
                destroyAllPieces();
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                PlayerPrefs.SetInt("Lvl2", 1);
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

    private void gameAction() {
        if (numCorPieces == 7){
            state = 3;
        }
    }
    public void pauseActive(){
        state = 2;
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
        destroyAllPieces();
        startGame();
    }

    private void destroyAllPieces(){
        for (int i = 0; i < peces.Count; ++i){
            Destroy(instantiates[i]);
        }
    }

    public void updateScore(int min, int sec, int points, int errors){

        if (min == 0 && sec <= 30) { //3 estrella
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (min <= 1) { //2 estrellas
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else { //1 estrellas
            star1.SetActive(true);
        }
        
        finalTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
