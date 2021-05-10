using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    void Start() {
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    // Start is called before the first frame update
    public void PlayGame()
    {
        SaveData.current = SerializationManager.Load();
        SaveData.current.soundON = true;
        SerializationManager.Save(SaveData.current);
        if (!SaveData.current.introDone) SceneManager.LoadScene("Intro");
        else SceneManager.LoadScene("Map");
    }
}
