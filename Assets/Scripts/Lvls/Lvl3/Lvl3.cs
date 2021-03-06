﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl3 : MonoBehaviour
{
    public List<GameObject> mites;
    private List<GameObject> instantiates;

    public static int points, errors, counter;

    private int count, estrelles;

    private float time;

    public Text  timer, finalTime, counterText;

    private int min, sec;
    public static int state; //0 = intro, 1 = game, 2 = pause, 3 = outro

    public GameObject action, game, outro, pause, introexp, star1, star2, star3, record, insignea;
    
    public static bool fals, cert;

    private bool gameOver, once;

    public Animator animator;

    void Start(){

        fals = false;
        cert = false;
        cleanLvl();
        if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Background");
        introexp.SetActive(true);
        
    }

    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        counter = 5;
        time = 0;
        points = 0;
        errors = 0;
        count = 0;
        introexp.SetActive(false);

        for (int i = 0; i < mites.Count; ++i){
            GameObject temp = mites[i];
            int randomIndex = Random.Range(i, mites.Count);
            mites[i] = mites[randomIndex];
            mites[randomIndex] = temp;
        }
        
        instantiates = mites;
        instantiates[count] = Instantiate(mites[count], game.gameObject.transform.position, Quaternion.identity);

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
                gameAction();
                break;
            case 2:
                action.SetActive(false);
                game.SetActive(false);
                pause.SetActive(true);
                instantiates[count].SetActive(false);
                break;
            case 3:
                 updateScore(min, sec, points, errors);
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                PlayerPrefs.SetInt("Lvl3", 1);
                break;
            default:
                break;
        }

       if (counter >= 0) counterText.text = counter.ToString();

        if (counter == 0) StartCoroutine(waitForGameOver());

        //Timer
        if (state == 1){
            time += Time.deltaTime;

            min = Mathf.FloorToInt(time / 60);
            sec = Mathf.FloorToInt(time % 60);
            timer.text = min.ToString("00") + ":" + sec.ToString("00");
        }
        
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
        gameOver = false;
        counter = 5;
        counterText.text = "5";
        state = 0;
        pause.SetActive(false);
        action.SetActive(false);
        game.SetActive(false);
        outro.SetActive(false);
    }

    private void gameAction() {

        if (count == 10){
            state = 3;
            return;
        }

        //Comprobem quina decisió han prés i si han acertat o no
        if (fals){
            if (instantiates[count].name[0] == 'B'){
                newMite();
                animator.SetBool("point", true);
                StartCoroutine(waitForAnimationEnd("point"));
                ++points;
                if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Correct");
            } else {
                newMite();
                animator.SetBool("error", true);
                StartCoroutine(waitForAnimationEnd("error"));
                --counter;
                ++errors;
                if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Wrong");
                Handheld.Vibrate();
            }
            fals = false;
        } 
        else if (cert){
            if (instantiates[count].name[0] == 'G'){
                newMite();
                animator.SetBool("point", true);
                StartCoroutine(waitForAnimationEnd("point"));
                ++points;
                if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Correct");
            } else {
                newMite();
                animator.SetBool("error", true);
                StartCoroutine(waitForAnimationEnd("error"));
                if (MapManager.soundON) FindObjectOfType<AudioManager>().Play("Wrong");
                Handheld.Vibrate();
                --counter;
                ++errors;
            }
            cert = false;
        }
    }

    IEnumerator waitForAnimationEnd(string x){
        yield return new WaitForSeconds(1);
        animator.SetBool(x, false);
    }

    private void newMite(){
        Destroy(instantiates[count]);
        ++count;
        if (count <= 9){
            instantiates[count] = Instantiate(mites[count], game.gameObject.transform.position, Quaternion.identity);
        }
    }


    public void endLvl(){
        pause.SetActive(false);
        outro.SetActive(false);
        SceneManager.LoadScene("Map");
    }

    public void backToLvl(){
        pause.SetActive(false);
        instantiates[count].SetActive(true);
        state = 1;
    }

    public void restartLvl(){
        pause.SetActive(false);
        for (int i = 0; i < mites.Count; ++i){
            instantiates[i] = mites[i];
        }
        startGame();
    }

    public void pauseActive(){
        state = 2;
    }

    public void updateScore(int min, int sec, int points, int errors){

        //mitja test1 1:20min
        if (!gameOver){
            if ((min < 1 || (min <= 1 && sec <= 10)) && (counter == 4 || counter == 5)) { //3 estrella
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                insignea.SetActive(true);
                estrelles = 3;
            }
            else if (min <= 1 && sec >= 30 && counter == 3) { //2 estrellas
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
                FindObjectOfType<AudioManager>().Stop("Background");
                FindObjectOfType<AudioManager>().Play("Ohh");
                once = false;
            }
        }

        saveScore(estrelles, (min * 60) + sec);
        
        finalTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    private void saveScore(int estrelles, int temps){
        if (estrelles != 0){
            SaveData.current = SerializationManager.Load();
            if (SaveData.current.temps3 == 0){
                SaveData.current.temps3 = temps;
                record.SetActive(true);
            }
            else if (temps < SaveData.current.temps3){
                SaveData.current.temps3 = temps;
                record.SetActive(true);
            }
            if (estrelles > SaveData.current.estrelles3){
                SaveData.current.estrelles3 = estrelles;
                SaveData.current.new3 = true;
            } 
            SerializationManager.Save(SaveData.current);
        }
    }
}
