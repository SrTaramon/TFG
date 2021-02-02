using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgMovement : MonoBehaviour
{
    private Vector3 touchPosition;

    private Vector3 offset;
    private Rigidbody2D rb;

    private Vector3 direction;

    //private float speed = 10f;

    public static bool correct, error, alive;

    Animator aniContl;
    // Start is called before the first frame update
    void Start()
    {
        correct = false;
        error = false;
        alive = true;
        rb = GetComponent<Rigidbody2D>();
        aniContl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
          Mathf.Clamp(transform.position.x, -8.8f, 8.8f),
          Mathf.Clamp(transform.position.y, -1.6f, 1.6f)  
        );

        if (correct){
            aniContl.SetBool("point", true);
            aniContl.SetBool("error", false);
        } else if (error){
            aniContl.SetBool("error", true);
            aniContl.SetBool("point", false);
        }
    }


    void OnMouseDown()
    {
     touchPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
     offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z));
 
    }

     void OnMouseDrag()
    {
        if (alive){
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
}
