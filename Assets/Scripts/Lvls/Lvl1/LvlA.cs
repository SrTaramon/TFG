using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlA : MonoBehaviour
{

    public List<GameObject> imgs;

    public Transform start;

    void Start(){

        for (int i = 0; i < imgs.Count; ++i){
            GameObject temp = imgs[i];
            int randomIndex = Random.Range(i, imgs.Count);
            imgs[i] = imgs[randomIndex];
            imgs[randomIndex] = temp;
        }

        imgs[0].transform.position = start.position;
        imgs[0].SetActive(true);
    }

    void Update(){

        for (int i = 5; i > 0; --i){
            if (!imgs[0].activeSelf){
                imgs.RemoveAt(0);
                imgs[0].transform.position = start.position;
                imgs[0].SetActive(true);
            }
        }
    }
   
}
