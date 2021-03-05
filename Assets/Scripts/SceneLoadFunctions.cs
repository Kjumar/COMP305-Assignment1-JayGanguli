using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadFunctions : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
