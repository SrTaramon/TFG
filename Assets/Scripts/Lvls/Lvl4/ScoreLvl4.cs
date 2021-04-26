using UnityEngine;

public class ScoreLvl4 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
        switch (col.gameObject.tag) {
            case "Sad":
                if (gameObject.tag == "Sad"){
                    Lvl4.needSad = false;
                    Lvl4.needHappy = true;
                    ++Lvl4.points;
                    if (LvlManager.soundON) FindObjectOfType<AudioManager>().Play("Correct");
                } else {
                    Lvl4.needHappy = false;
                    Lvl4.needSad = true;
                    --Lvl4.counter;
                    ++Lvl4.errors;
                    if (LvlManager.soundON) FindObjectOfType<AudioManager>().Play("Wrong");
                    Handheld.Vibrate();
                }
                Ball.destroyed = true;
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            case "Happy":
                if (gameObject.tag == "Happy"){
                    Lvl4.needHappy = false;
                    Lvl4.needSad = true;
                    ++Lvl4.points;
                    if (LvlManager.soundON) FindObjectOfType<AudioManager>().Play("Correct");
                } else {
                    Lvl4.needSad = false;
                    Lvl4.needHappy = true;
                    --Lvl4.counter;
                    ++Lvl4.errors;
                    if (LvlManager.soundON) FindObjectOfType<AudioManager>().Play("Wrong");
                    Handheld.Vibrate();
                }
                Ball.destroyed = true;
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
