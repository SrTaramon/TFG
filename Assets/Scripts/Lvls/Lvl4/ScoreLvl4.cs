using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLvl4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        switch (col.gameObject.tag) {
            case "Sad":
                if (gameObject.tag == "Sad"){
                    ++Lvl4.points;
                } else {
                    ++Lvl4.errors;
                }
                Ball.destroyed = true;
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            case "Happy":
                if (gameObject.tag == "Happy"){
                    ++Lvl4.points;
                } else {
                    ++Lvl4.errors;
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
