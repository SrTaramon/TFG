using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SaveData.current = SerializationManager.Load();
        if (!SaveData.current.introDone) SceneManager.LoadScene("Intro");
        else SceneManager.LoadScene("Map");
    }
}
