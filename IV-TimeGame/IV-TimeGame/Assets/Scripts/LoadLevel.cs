using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public static void loadLevelByIndex(int i)
    {
        SceneManager.LoadScene(i);
    }
    public static void loadLevelByName(string s)
    {
        SceneManager.LoadScene(s);
    }
}
