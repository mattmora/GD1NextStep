using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TSceneLoader : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
