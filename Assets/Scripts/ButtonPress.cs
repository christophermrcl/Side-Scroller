using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    public GameObject objectEnable;
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableObject()
    {
        objectEnable.SetActive(true);
    }
    public void DisableObject()
    {
        objectEnable.SetActive(false);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        // If running in the Unity Editor, stop playing
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running in a build, quit the application
        Application.Quit();
        #endif
    }
}
