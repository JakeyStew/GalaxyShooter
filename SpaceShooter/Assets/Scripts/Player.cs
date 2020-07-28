using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5f;
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManger;

    private bool _isTripleShotActive = false;
    private bool _isSpeedActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;

    //variable reference to shield visualiser
    [SerializeField]
    private GameObject _playerShield;

    [SerializeField]
    private GameObject[] _damageEngines;

    [SerializeField]
    private AudioClip _laserAudio;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManger = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManger == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL!");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The laser audio is NULL!");
        } 
        else
        {
            _audioSource.clip = _laserAudio;
        }
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(new Vector3(1, 0, 0) * horizontalInput * playerSpeed * Time.deltaTime);
        //transform.Translate(new Vector3(0, 1, 0) * verticalInput * playerSpeed * Time.deltaTime);

        //if speedboost acitve is false
        if (_isSpeedActive == false)
        {
            //Optimal solution
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * playerSpeed * Time.deltaTime);
        }
        else
        {
            //else speed boost multiplier
            //Optimal solution
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * (playerSpeed * _speedMultiplier) * Time.deltaTime);
        }

        float xDirection = transform.position.x;
        float yDirection = transform.position.y;

        if (yDirection >= 0)
        {
            transform.position = new Vector3(xDirection, 0, 0);
        }
        else if (yDirection <= -3.8f)
        {
            transform.position = new Vector3(xDirection, -3.8f, 0);
        }

        if (xDirection >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, yDirection, 0);
        }
        else if (xDirection <= -11.3f)
        {
            transform.position = new Vector3(11.3f, yDirection, 0);
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        //Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);

        //If spacekey pressed
        //if tripleshot active is true
        //fire 3 lasers (triple shot prefab)
        //else fire 1 laser
        if (_isTripleShotActive)
        {
            Instantiate(tripleShotPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);
        }

        //Play the laser audio clip
        _audioSource.Play();
    }

    public void Damage()
    {
        //if shields is active
        //do nothing
        //deactivate shields
        //return;
        if (_isShieldActive)
        {
            _isShieldActive = false;
            //deactivate shield visualiser
            _playerShield.SetActive(false);
            return;
        }

        //Equal to _lives = _lives - 1;
        _lives -= 1;
        //Pass through lives

        //if lives is 2, enable right engine
        //else if lives is 1, enable left engine
        int randomEngine = Random.Range(0, 2);
        if (_lives == 2)
        {
            _damageEngines[randomEngine].SetActive(true);
        }
        else if (_lives == 1)
        {
            _damageEngines[0].SetActive(true);
            _damageEngines[1].SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        //Check if dead
        //Destroy us
        if (_lives < 1)
        {
            //Communicate with Spawn Manager
            //Let them know to stop spawning
            _spawnManger.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    //Method to control powerup
    public void TripleShotActive()
    {
        //TripleShotActive active becomes true;
        //Start the power down coroutine for the triple shot
        _isTripleShotActive = true;
        StartCoroutine(PowerDownTripleShot());
    }

    //IEnumerator TripleShotPowerDownRoutine
    //Wait 5 seconds
    //Set the triple shot to false
    IEnumerator PowerDownTripleShot()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedPowerUpActive()
    {
        _isSpeedActive = true;
        StartCoroutine(PowerDownSpeed());
    }

    IEnumerator PowerDownSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false;
    }

    public void ShieldPowerUpActive()
    {
        _isShieldActive = true;
        //enable shield visualiser
        _playerShield.SetActive(true);
    }

    //Method to add 10 to score
    //Communicate wiht the UI to update the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
