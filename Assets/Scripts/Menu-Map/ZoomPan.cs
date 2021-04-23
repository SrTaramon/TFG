using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomPan : MonoBehaviour
{
    Vector3 dragOrigen;
    private float mapMinY = -5;
    private float mapMaxY = 20;
    
    // Start is called before the first frame update
    void Start()
    {
         Camera.main.transform.position = new Vector3(0, 0, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            dragOrigen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)){
            Vector3 difference = dragOrigen - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            difference.x = Camera.main.transform.position.x;
            Camera.main.transform.position = CameraClamp(Camera.main.transform.position + difference);
        }
    }

    private Vector3 CameraClamp(Vector3 targetPosisiton){

        float camHeight = Camera.main.orthographicSize;

        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newY = Mathf.Clamp(targetPosisiton.y, minY, maxY);

        return new Vector3(targetPosisiton.x, newY, targetPosisiton.z);
    }
}
