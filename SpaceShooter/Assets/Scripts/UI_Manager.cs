using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    //Handle to Text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _quitText;
    [SerializeField]
    private Text _ShootToStartText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Assign text component to handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.Log("Game Manager is NULL");
        }
        StartCoroutine(StartFlickerRoutine());
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        //Display image sprite
        //give it a new one based on currentLives index
        _LivesImg.sprite = _liveSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void StartGame()
    {
        _ShootToStartText.gameObject.SetActive(false);
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _quitText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator StartFlickerRoutine()
    {
        while (true)
        {
            _ShootToStartText.text = "Shoot the asteroid to start";
            yield return new WaitForSeconds(0.75f);
            _ShootToStartText.text = "";
            yield return new WaitForSeconds(0.75f);
        }
    }
}
