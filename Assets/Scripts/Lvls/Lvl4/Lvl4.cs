using System.Collections.Generic;
using System.Collections;
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

    public Text  timer, finalTime, counterText;

    private int min, sec, estrelles;
    public static int state, counter; //state {0 = intro, 1 = game, 2 = pause, 3 = outro}

    public GameObject action, game, outro, pause, introexp, star1, star2, star3, record;
    
    public static bool fals, cert; 

    private GameObject ball, img1, img2;

    private bool created, gameOver;

    public static bool needHappy, needSad;

    void Start(){

        fals = false;
        cert = false;
        introexp.SetActive(true);
        cleanLvl();
        
    }


    //En auqesta funció primer ordener de manera random la llista de cartes, cambiem d'estat i instanciem la primera carta
    public void startGame(){

        created = false;
        needHappy = true;
        needSad = false;
        time = 0;
        points = 0;
        errors = 0;
        count = 0;
        introexp.SetActive(false);

        if (img1 == null)  img1 = Instantiate(imatges[4], posicions[4].gameObject.transform.position, Quaternion.identity);
        if (img2 == null) img2 = Instantiate(imatges[5], posicions[5].gameObject.transform.position, Quaternion.identity);
        if (ball == null) ball = Instantiate(balls[0], ballPos.gameObject.transform.position, Quaternion.identity);

        img1.SetActive(true);
        img2.SetActive(true);

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
                pause.SetActive(true);
                if (img1 != null) img1.SetActive(false);
                if (img2 != null) img2.SetActive(false);
                break;
            case 3:
                updateScore(min, sec, points, errors);
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                Destroy(ball);
                PlayerPrefs.SetInt("Lvl4", 1);
                break;
            default:
                break;
        }

        counterText.text = counter.ToString();

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
        gameOver = false;
        counterText.text = "5";
        counter = 5;
        state = 0;
        pause.SetActive(false);
        action.SetActive(false);
        game.SetActive(false);
        outro.SetActive(false);
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
                case 10:
                    state = 3;
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
        state = 1;
        if (img1 != null) img1.SetActive(true);
        if (img2 != null) img2.SetActive(true);
    }

    public void restartLvl(){
        pause.SetActive(false);
        startGame();
    }

    public void pauseActive(){
        state = 2;
    }

    public void updateScore(int min, int sec, int points, int errors){

        //mitja test1 1:35min
        if (!gameOver){
            if (min < 1 || (min <= 1 && sec <= 25)) { //3 estrella
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                estrelles = 3;
            }
            else if (min <= 1 && sec <= 50) { //2 estrellas
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
        if (estrelles != 0){
            SaveData.current = SerializationManager.Load();
            if (SaveData.current.temps4 == 0){
                SaveData.current.temps4 = temps;
                record.SetActive(true);
            }
            else if (temps < SaveData.current.temps4){
                SaveData.current.temps4 = temps;
                record.SetActive(true);
            }
            if (estrelles > SaveData.current.estrelles4) SaveData.current.estrelles4 = estrelles;
            SerializationManager.Save(SaveData.current);
        }
    }
}
