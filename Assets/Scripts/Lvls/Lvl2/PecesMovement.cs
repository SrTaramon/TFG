using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PecesMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 touchPosition;

    private Vector3 offset;

    private Vector3 direction;
    public GameObject end;
    private Vector3 initialPos, finalPos, dropPosition;

    public static bool correct, error, paused;

    Animator aniContl;

    private bool badMove, goodMove, inside, bingo;

    private float t;
    // Start is called before the first frame update
    void Start()
    {
        correct = false;
        error = false;
        inside = false;
        badMove = false;
        goodMove = false;
        bingo = false;
        paused = false;
        t = 0;
        initialPos = transform.position;
        finalPos = end.transform.position;
        //aniContl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {  
        switch (gameObject.name){
            case "P11(Clone)":
            case "P12(Clone)":       
                createScreenBorders(7.4f, 4f);
                break;
            case "P13(Clone)":           
                createScreenBorders(6.5f, 3.2f);
                break;
            case "P21(Clone)":
            case "P22(Clone)":           
                createScreenBorders(6.5f, 4f);
                break;
            case "P23(Clone)":           
                createScreenBorders(7.4f, 4.3f);
                break;
            case "P24(Clone)":           
                createScreenBorders(8f, 4.6f);
                break;
            default:
                break;
        }

        if (badMove){
            t += Time.deltaTime;

            if (t >= 2){
                transform.position = initialPos;
                badMove = false;
                t = 0;
            } else {    
                transform.position = Vector3.Lerp(dropPosition, initialPos, t / 1);
            }
        } 
        else if (goodMove){
            t += Time.deltaTime;

            if (t >= 1){
                transform.position = finalPos;
                goodMove = false;
                t = 0;
            } else {    
                transform.position = Vector3.Lerp(dropPosition, finalPos, t / 0.5f);
            }
        }

        /* if (correct){
            aniContl.SetBool("point", true);
            aniContl.SetBool("error", false);
        } else if (error){
            aniContl.SetBool("error", true);
            aniContl.SetBool("point", false);
        } */
    }


    private void createScreenBorders(float xLimit, float yLimit){
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -xLimit, xLimit),
            Mathf.Clamp(transform.position.y, -yLimit, yLimit),
            transform.position.z  
        );
    }

    void OnMouseDown()
    {
        if (!bingo && !paused){
            touchPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z));
        }
    }

     void OnMouseDrag(){
        if (!bingo && !paused){ 
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag(gameObject.name)){
            inside = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag(gameObject.name)){
            inside = false;
        }
    }

    void OnMouseUp(){
        if (inside){
            ++Lvl2.numCorPieces;
            bingo = true;
            goodMove = true;
            badMove = false;
        } else {
            badMove = true;
            goodMove = false;
        }
        dropPosition = transform.position;
    }

}
