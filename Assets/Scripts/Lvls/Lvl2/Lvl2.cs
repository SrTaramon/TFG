﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl2 : MonoBehaviour
{
    public List<GameObject> peces, initialPos, instantiates;

    public static int points, errors;

    private float time;

    public Text  timer, finalTime, counterText;

    private int min, sec, estrelles;
    public static int state, numCorPieces, counter; //state {0 = intro, 1 = game, 2 = pause, 3 = outro}

    public GameObject action, game, outro, pause, introexp, star1, star2, star3, record, insignea;

    private bool done, gameOver, once;

    void Start(){

        time = 0;
        numCorPieces = 0;
        cleanLvl();
        introexp.SetActive(true);
        if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Background");
    }

    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        time = 0;
        points = 0;
        errors = 0;
        counter = 5;
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
                //PlayerPrefs.SetInt("Lvl2", 1);
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

        if (counter >= 0) counterText.text = counter.ToString();

        if (counter == 0) StartCoroutine(waitForGameOver());
        
    }

    IEnumerator waitForGameOver(){

        //play lose music
        yield return new WaitForSeconds(2);

        gameOver = true;
        min = 0;
        sec = 0;
        points = 0;
        state = 3;
    }

    private void cleanLvl(){
        once = true;
        counter = 5;
        counterText.text = "5";
        state = 0;
        gameOver = false;
        pause.SetActive(false);
        action.SetActive(false);
        game.SetActive(false);
        outro.SetActive(false);
    }

    private void gameAction() {
        if (numCorPieces == 7){
            StartCoroutine(waitForGameEnds());
        }
    }

    IEnumerator waitForGameEnds(){

        yield return new WaitForSeconds(2);

        state = 3;
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

        //mitja test1 42s
        if (!gameOver){
            if ((min == 0 && sec <= 30) && (counter == 4 || counter == 5)) { //3 estrella
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                insignea.SetActive(true);
                estrelles = 3;
            }
            else if (min == 0 && sec <= 50 && counter == 3) { //2 estrellas
                star1.SetActive(true);
                star2.SetActive(true);
                insignea.SetActive(true);
                estrelles = 2;
            }
            else { //1 estrellas
                star1.SetActive(true);
                insignea.SetActive(true);
                estrelles = 1;
            }

            if (once){
                if (MapManager.soundON) {
                    FindObjectOfType<AudioManager>().Stop("Background");
                    FindObjectOfType<AudioManager>().Play("Celebration");
                }
                once = false;
            }
        } else {
            if (once){
                if (MapManager.soundON) {
                    FindObjectOfType<AudioManager>().Stop("Background");
                    FindObjectOfType<AudioManager>().Play("Ohh");
                }
                once = false;
            }
        }

        saveScore(estrelles, (min * 60) + sec);
        
        finalTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    private void saveScore(int estrelles, int temps){
        if (estrelles != 0){
            SaveData.current = SerializationManager.Load();
            if (SaveData.current.temps2 == 0){
                SaveData.current.temps2 = temps;
                record.SetActive(true);
            }
            else if (temps < SaveData.current.temps2){
                SaveData.current.temps2 = temps;
                record.SetActive(true);
            }
            if (estrelles > SaveData.current.estrelles2){
                SaveData.current.estrelles2 = estrelles;
                SaveData.current.new2 = true;
            } 
            SerializationManager.Save(SaveData.current);
        }
    }
}
