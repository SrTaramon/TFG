using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PecesMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 touchPosition;

    private Vector3 offset;

    private Vector3 direction;

    public GameObject finalPos, initialPos;

    public static bool correct, error, inside, outside;

    Animator aniContl;
    // Start is called before the first frame update
    void Start()
    {
        correct = false;
        error = false;
        inside = false;
        outside = false;
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
        touchPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z));
        
    }

     void OnMouseDrag(){
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag(gameObject.name)){
            Debug.Log("Hola");
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag(gameObject.name)){
            Debug.Log("Adeu");
        }
    }

}
