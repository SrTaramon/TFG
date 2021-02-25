using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lvl3 : MonoBehaviour
{
    public List<GameObject> mites;
    private List<GameObject> instantiates;

    public static int points, errors;

    private int count;

    private float time;

    public Text  timer, finalTime;

    private int min, sec;
    public static int state; //0 = intro, 1 = tutorial, 2 = game, 3 = pause, 4 = outro

    public GameObject action, game, outro, pause, introexp, tutorial, star1, star2, star3;
    
    public static bool fals, cert; 

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

        time = 0;
        points = 0;
        errors = 0;
        count = 0;
        tutorial.SetActive(false);

        for (int i = 0; i < mites.Count; ++i){
            GameObject temp = mites[i];
            int randomIndex = Random.Range(i, mites.Count);
            mites[i] = mites[randomIndex];
            mites[randomIndex] = temp;
        }
        
        instantiates = mites;
        instantiates[count] = Instantiate(mites[count], game.gameObject.transform.position, Quaternion.identity);

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
                instantiates[count].SetActive(false);
                break;
            case 4:
                updateScore(min, sec, points, errors);
                game.SetActive(false);
                action.SetActive(false);
                outro.SetActive(true);
                PlayerPrefs.SetInt("Lvl2", 1);
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

        if (count == 10){
            Debug.Log("HOLA");
            state = 4;
            return;
        }

        //Comprobem quina decisió han prés i si han acertat o no
        if (fals){
            if (instantiates[count].name[0] == 'B'){
                newMite();
                ++points;
            } else {
                newMite();
                ++errors;
            }
            fals = false;
        } 
        else if (cert){
            if (instantiates[count].name[0] == 'G'){
                newMite();
                ++points;
            } else {
                newMite();
                ++errors;
            }
            cert = false;
        }
    }

    private void newMite(){
        Debug.Log(count);
        Destroy(instantiates[count]);
        ++count;
        if (count <= 9){
            instantiates[count] = Instantiate(mites[count], game.gameObject.transform.position, Quaternion.identity);
        }
    }


    public void endLvl(){
        pause.SetActive(false);
        SceneManager.LoadScene("Map");
    }

    public void backToLvl(){
        pause.SetActive(false);
        instantiates[count].SetActive(true);
        state = 2;
    }

    public void restartLvl(){
        pause.SetActive(false);
        for (int i = 0; i < mites.Count; ++i){
            instantiates[i] = mites[i];
        }
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
