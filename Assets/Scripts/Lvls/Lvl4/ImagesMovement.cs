using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesMovement : MonoBehaviour
{
    private float nextForceTime = 0;
    public float period = 2;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
