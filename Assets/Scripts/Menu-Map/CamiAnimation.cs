using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamiAnimation : MonoBehaviour
{

    public GameObject[] cami;

    private float timer = 60f;
    // Start is called before the first frame update
    void Start()
    {
        cami[0].gameObject.GetComponent<Animation>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        switch (timer){
            case 40:
                cami[1].gameObject.GetComponent<Animation>().Play();
                Debug.Log("1");
            break;
            case 20:
                cami[2].gameObject.GetComponent<Animation>().Play();
                Debug.Log("2");
            break;
            case 0:
                cami[3].gameObject.GetComponent<Animation>().Play();
                Debug.Log("3");
            break;
        }
    }
}
