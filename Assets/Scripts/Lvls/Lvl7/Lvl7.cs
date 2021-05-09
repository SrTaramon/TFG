using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl7 : MonoBehaviour
{

    public static List<int> parella;

    public List<GameObject> posicions, cartes, backs;

    private GameObject activeCard;

    public static bool active;

    public static int clickCount, points, errors, counter;

    private int previousCount;

    private float time;

    public Text  timer, finalTime, counterText;

    private int min, sec, estrelles;
    public static int state; //0 = intro, 1 = intro, 2 = game, 3 = outro

    public GameObject action, game, outro, pause, introexp, star1, star2, star3, record, insignea;

    private bool done, restart, gameOver, once, once2;

    public static GameObject card1, card2;

    // Start is called before the first frame update
    void Start(){
        time = 0;
        cleanLvl();
        introexp.SetActive(true);
        if (LvlManager.soundON) FindObjectOfType<AudioManager>().Play("Background");
    }

    private void cleanLvl()
    {
        points = 0;
        parella = new List<int>();
        restart = false;
        once = true;
        gameOver = false;
        counter = 8;
        counterText.text = "5";
        state = 0;
        estrelles = 0;
        once2 = false;
        clickCount = 0;
        pause.SetActive(false);
        action.SetActive(false);
        game.SetActive(false);
        outro.SetActive(false);
    }

    public void startGame(){

        counter = 8;

        if (restart){
            deleteCards();
            taparCards();
            restart = false;
        }

        for (int i = 0; i < cartes.Count; ++i){
            GameObject temp = cartes[i];
            int randomIndex = Random.Range(i, cartes.Count);
            cartes[i] = cartes[randomIndex];
            cartes[randomIndex] = temp;
        }

        for (int j = 0; j < posicions.Count; ++j){
            Instantiate (cartes[j], posicions[j].transform.position, Quaternion.identity);
        }

        time = 0;
        points = 0;
        errors = 0;

        introexp.SetActive(false);

        
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
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
                action.SetActive(false);
                pause.SetActive(true);
                break;
            case 3:
                deleteCards();
                updateScore(min, sec, points, errors);
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                break;
            default:
                break;
        }

        if (clickCount == 2){
            comprobarParella();
        }

        if (points == 5){
            state = 3;
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

    void comprobarParella() {
        
        if (clickCount != 0){
            int carta1 = parella[0];
            int carta2 = parella[1];
            if (cartes[carta1].tag ==  cartes[carta2].tag){
                FindObjectOfType<AudioManager>().Play("Correct");
                ++points;
                parella.RemoveAt(1);
                parella.RemoveAt(0);
            } else {
                FindObjectOfType<AudioManager>().Play("Wrong");
                --counter;
                StartCoroutine(waitForError());
            }

            clickCount = 0;
        }
    }

    private void deleteCards() {
        for (int x = 1; x <= 5; ++x){
            for (int t = 1; t <= 2; ++t){
                string name = x.ToString() + t.ToString() + "(Clone)";
                GameObject g = GameObject.Find(name);
                Destroy(g);
            }
        }
    }

    void taparCards(){
        for (int i = 0; i < backs.Count; ++i){
            backs[i].SetActive(true);
        }
    }
    private void gameAction() {
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

    IEnumerator waitForError(){
        yield return new WaitForSeconds(1);

        backs[parella[0]].SetActive(true);
        backs[parella[1]].SetActive(true);

        parella.RemoveAt(1);
        parella.RemoveAt(0);
    }

    IEnumerator waitForGameOver(){

        //play lose music
        yield return new WaitForSeconds(2);

        gameOver = true;
        estrelles = 0;
        min = 0;
        sec = 0;
        points = 0;
        state = 3;
    }

    public void updateScore(int min, int sec, int points, int errors){

        //mitja test1 1:26min
        if (!gameOver){
            if ((min < 1 || (min == 1 && sec <= 20)) && (counter == 8 || counter == 7 || counter == 6)) { //3 estrella
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                insignea.SetActive(true);
                estrelles = 3;
            }
            else if (min < 2 && (counter == 5 || counter == 4 || counter == 3)) { //2 estrellas
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
                if (LvlManager.soundON) {
                    FindObjectOfType<AudioManager>().Stop("Background");
                    FindObjectOfType<AudioManager>().Play("Celebration");
                }
                once = false;
            }
        } else {
            if (once){
                if (LvlManager.soundON) {
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
            if (SaveData.current.temps7 == 0){
                SaveData.current.temps7 = temps;
                record.SetActive(true);
            }
            else if (temps < SaveData.current.temps7){
                SaveData.current.temps7 = temps;
                record.SetActive(true);
            }
            if (estrelles > SaveData.current.estrelles7){
                SaveData.current.estrelles7 = estrelles;
                SaveData.current.new7 = true;
            }
            SerializationManager.Save(SaveData.current);
        }
    }
}
