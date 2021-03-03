using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startPos, endPos, fingerPos, fingerDir;
    
    private LineRenderer lineRenderer;

    private Rigidbody2D rb;

    private Camera cam;

    private float max;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;
        max = 2;
    }

    // Update is called once per frame
    void Update()
    {
        fingerPos = cam.ScreenToWorldPoint(Input.mousePosition);
        fingerDir = fingerPos - gameObject.transform.position;
        fingerDir.z = 0;
        fingerDir = fingerDir.normalized;
    }

    void OnMouseDown(){
        lineRenderer.enabled = true;
    }

    void OnMouseDrag(){
        startPos = gameObject.transform.position;
        startPos.z = 0;
        lineRenderer.SetPosition(0, startPos);
        endPos = fingerPos;
        endPos.z = 0;
        float length = Mathf.Clamp(Vector2.Distance(startPos, endPos), 0, max);
        endPos = startPos + (fingerDir * length);
        lineRenderer.SetPosition(1, endPos);
    }

    void OnMouseUp(){
        lineRenderer.enabled = false;
        Vector2 ballForce = new Vector2(Mathf.Clamp(startPos.x-endPos.x, -5, 5), Mathf.Clamp(startPos.y - endPos.y, -5, 5));
        rb.AddForce(ballForce * 3, ForceMode2D.Impulse);
    }
}
