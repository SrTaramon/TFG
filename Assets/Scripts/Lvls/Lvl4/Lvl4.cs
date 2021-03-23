using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Lvl4 : MonoBehaviour
{
    public List<GameObject> balls;
    public GameObject ballPos;
    public List<GameObject> imatges;
    public List<GameObject> posicions;

    public static int points, errors;

    private int count;

    private float time;

    public Text  timer, finalTime;

    private int min, sec;
    public static int state; //0 = intro, 1 = tutorial, 2 = game, 3 = pause, 4 = outro

    public GameObject action, game, outro, pause, introexp, tutorial, star1, star2, star3;
    
    public static bool fals, cert; 

    private GameObject ball, img1, img2;

    private bool created;

    public static bool needHappy, needSad;

    void Start(){

        fals = false;
        cert = false;
        introexp.SetActive(true);
        
    }

    public void startTutorial(){

        state = 1;
        introexp.SetActive(false);
        
    }

    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        created = false;
        needHappy = false;
        needSad = false;
        time = 0;
        points = 0;
        errors = 0;
        count = 0;
        tutorial.SetActive(false);

        img1 = Instantiate(imatges[4], posicions[4].gameObject.transform.position, Quaternion.identity);
        img2 = Instantiate(imatges[5], posicions[5].gameObject.transform.position, Quaternion.identity);
        ball = Instantiate(balls[0], ballPos.gameObject.transform.position, Quaternion.identity);

        state = 2;
    }

    //Consultem l'estat en que ens trobem
    void Update(){

        switch (state) {
            case 0:
                Start();
                break;
            case 1:
                tutorial.SetActive(true);
                break;
            case 2:
                action.SetActive(true);
                game.SetActive(true);
                gameAction();
                break;
            case 3:
                action.SetActive(false);
                pause.SetActive(true);
                break;
            case 4:
                updateScore(min, sec, points, errors);
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                PlayerPrefs.SetInt("Lvl4", 1);
                break;
            default:
                break;
        }

        //Timer
        if (state == 2){
            time += Time.deltaTime;

            min = Mathf.FloorToInt(time / 60);
            sec = Mathf.FloorToInt(time % 60);
            timer.text = min.ToString("00") + ":" + sec.ToString("00");
        }
        
    }

    private void gameAction() {

        if (ball == null){
            if (needHappy){
                ball = Instantiate(balls[0], ballPos.gameObject.transform.position, Quaternion.identity);
            }
            else if (needSad){
                ball = Instantiate(balls[1], ballPos.gameObject.transform.position, Quaternion.identity);
            }
        }

        if (img1 == null && img2 == null){
            created = false;
        }

        if (!created){
            switch (errors + points){
                case 2:
                    img1 = Instantiate(imatges[0], posicions[0].gameObject.transform.position, Quaternion.identity);
                    img2 = Instantiate(imatges[1], posicions[1].gameObject.transform.position, Quaternion.identity);
                    needHappy = true;
                    created = true;
                    break;
                case 4:
                    img1 = Instantiate(imatges[2], posicions[2].gameObject.transform.position, Quaternion.identity);
                    img2 = Instantiate(imatges[3], posicions[3].gameObject.transform.position, Quaternion.identity);
                    needSad = true;
                    created = true;
                    break;
                case 6:
                    img1 = Instantiate(imatges[8], posicions[8].gameObject.transform.position, Quaternion.identity);
                    img2 = Instantiate(imatges[9], posicions[9].gameObject.transform.position, Quaternion.identity);
                    needHappy = true;
                    created = true;
                    break;
                case 8:
                    img1 = Instantiate(imatges[6], posicions[6].gameObject.transform.position, Quaternion.identity);
                    img2 = Instantiate(imatges[7], posicions[7].gameObject.transform.position, Quaternion.identity);
                    needSad = true;
                    created = true;
                    break;
                default:
                    break;
            }
        }
    }


    public void endLvl(){
        pause.SetActive(false);
        SceneManager.LoadScene("Map");
    }

    public void backToLvl(){
        pause.SetActive(false);
        state = 2;
    }

    public void restartLvl(){
        pause.SetActive(false);
        startGame();
    }

    public void updateScore(int min, int sec, int points, int errors){

        if (points >= 8) { //3 estrella
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (points >= 5) { //2 estrellas
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else { //1 estrellas
            star1.SetActive(true);
        }
        
        finalTime.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
