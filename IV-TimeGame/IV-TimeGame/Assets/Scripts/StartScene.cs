using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button startButton;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(SwitchScene);
    }

    // Update is called once per frame
     private void SwitchScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        
    }
}
