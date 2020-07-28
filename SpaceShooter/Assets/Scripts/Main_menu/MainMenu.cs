using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private LevelChanger _levelChanger;

    public void LoadGame()
    {
        _levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        if (_levelChanger == null)
        {
            Debug.Log("Level Changer is NULL - MainMenu.cs");
        }
        _levelChanger.FadeToLevel(1);
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
