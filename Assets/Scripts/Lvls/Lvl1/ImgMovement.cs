using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgMovement : MonoBehaviour
{
    private Vector3 touchPosition;

    private Vector3 offset;
    private Rigidbody2D rigidbody;

    private Vector3 direction;

    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rigidbody.velocity = new Vector2(direction.x, direction.y) * speed;

            if (touch.phase == TouchPhase.Ended){
                rigidbody.velocity = Vector2.zero;
            }
        }*/
    }


    void OnMouseDown()
    {
     touchPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
     offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z));
 
    }

     void OnMouseDrag()
    {
    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, touchPosition.z);
    
    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    transform.position = curPosition;
    
    }
}
