using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _asteroidRotateSpeed = 15.0f;

    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private UI_Manager _uiManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if(_spawnManager == null)
        {
            Debug.Log("Spawn Manager is NULL - Asteroid.cs");
        }
        if(_uiManager == null)
        {
            Debug.Log("UI Manager is NULL - Asteroid.cs");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Rotate object on the z axis
        transform.Rotate(Vector3.forward * _asteroidRotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _uiManager.StartGame();
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.15f);
        }
    }
}
