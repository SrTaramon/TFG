using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{

    public GameObject a1, a2, a3, a4, al1, al2, al3, al4, m1, m2, m3, m4, m5, m7, lvl2, lvl7, b1lvl7, b2lvl7, b3lvl7, b4lvl7, b5lvl7, lvl5, b1lvl5, b2lvl5, b3lvl5, b4lvl5, b5lvl5, blvl5, blvl7, lvlCompleted;

    private bool once;

    public static bool soundON;

    public Sprite On, Off;

    public void goToInsigneas(){
        SceneManager.LoadScene("Insigneas");
    }

    public void changeSound(Button b){
        if (soundON){
            FindObjectOfType<AudioManager>().Stop("Theme");
            b.GetComponent<Image>().sprite = Off;
        } else {
            FindObjectOfType<AudioManager>().Play("Theme");
            b.GetComponent<Image>().sprite = On;
        }
        soundON = !soundON;
    }
    // Start is called before the first frame update
    void Start(){
        soundON = true;
        once = true;
        SaveData.current = SerializationManager.Load();
        switch (SaveData.current.selectedAvatar){
            case 1:
                a1.SetActive(true);
                al1.SetActive(true);
                break;
            case 2:
                a2.SetActive(true);
                al2.SetActive(true);
                break;
            case 3:
                a3.SetActive(true);
                al3.SetActive(true);
                break;
            case 4:
                a4.SetActive(true);
                al4.SetActive(true);
                break;
            default:
                break;
        }

        if (SaveData.current.estrelles1 != 0){
            m1.SetActive(true);
            if (!SaveData.current.desblo5){
                lvl5.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                lvl5.gameObject.tag = "NoBloc";
                blvl5.GetComponent<SpriteRenderer>().color = new Color(0.745283f, 0.3388749f, 0.2348344f, 1f);
                b1lvl5.GetComponent<SpriteRenderer>().color = new Color(0.9058f, 0.4156f, 0.2901f, 1f);
                b2lvl5.GetComponent<SpriteRenderer>().color = new Color(0.9058f, 0.4156f, 0.2901f, 1f);
                b3lvl5.GetComponent<SpriteRenderer>().color = new Color(0.9058f, 0.4156f, 0.2901f, 1f);
                b4lvl5.GetComponent<SpriteRenderer>().color = new Color(0.9058f, 0.4156f, 0.2901f, 1f);
                b5lvl5.GetComponent<SpriteRenderer>().color = new Color(0.9058f, 0.4156f, 0.2901f, 1f);
                SaveData.current.desblo5 = true;
            }
        } 
        else m1.SetActive(false);
        if (SaveData.current.estrelles2 != 0){
            m2.SetActive(true);
            // if (!SaveData.current.desblo6){
            //     lvl6.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            //     lvl6.gameObject.tag = "NoBloc";
            //     blvl6.GetComponent<SpriteRenderer>().color = new Color(0.754717f, 0.2876469f, 0.5737274f);
            //     b1lvl6.GetComponent<SpriteRenderer>().color = new Color(0.9254f, 0.4862f, 0.6901f, 1f);
            //     b2lvl6.GetComponent<SpriteRenderer>().color = new Color(0.9254f, 0.4862f, 0.6901f, 1f);
            //     b3lvl6.GetComponent<SpriteRenderer>().color = new Color(0.9254f, 0.4862f, 0.6901f, 1f);
            //     b4lvl6.GetComponent<SpriteRenderer>().color = new Color(0.9254f, 0.4862f, 0.6901f, 1f);
            //     b5lvl6.GetComponent<SpriteRenderer>().color = new Color(0.9254f, 0.4862f, 0.6901f, 1f);
            //     SaveData.current.desblo6 = true;
            // }
        } 
        else m2.SetActive(false);
        if (SaveData.current.estrelles3 != 0){
            m3.SetActive(true);
            if (!SaveData.current.desblo7){
                lvl7.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                lvl7.gameObject.tag = "NoBloc";
                blvl7.GetComponent<SpriteRenderer>().color = new Color(0.9339623f, 0.7135811f, 0.01321642f, 1f);
                b1lvl7.GetComponent<SpriteRenderer>().color = new Color(0.882353f, 0.6666667f, 0.01176471f, 1f);
                b2lvl7.GetComponent<SpriteRenderer>().color = new Color(0.882353f, 0.6666667f, 0.01176471f, 1f);
                b3lvl7.GetComponent<SpriteRenderer>().color = new Color(0.882353f, 0.6666667f, 0.01176471f, 1f);
                b4lvl7.GetComponent<SpriteRenderer>().color = new Color(0.882353f, 0.6666667f, 0.01176471f, 1f);
                b5lvl7.GetComponent<SpriteRenderer>().color = new Color(0.882353f, 0.6666667f, 0.01176471f, 1f);
                SaveData.current.desblo7 = true;
            }
        } 
        else m3.SetActive(false);
        if (SaveData.current.estrelles4 != 0) m4.SetActive(true);
        else m4.SetActive(false);
        if (SaveData.current.estrelles5 != 0) m5.SetActive(true);
        else m5.SetActive(false);
        if (SaveData.current.estrelles7 != 0) m7.SetActive(true);
        else m7.SetActive(false);

        if (SaveData.current.new1 == true || SaveData.current.new2 == true || SaveData.current.new3 == true || SaveData.current.new4 == true || SaveData.current.new5 == true || SaveData.current.new7 == true){
            lvlCompleted.SetActive(true);
        } else {
            lvlCompleted.SetActive(false);
        }
        
    }

}
