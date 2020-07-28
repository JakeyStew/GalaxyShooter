using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;
    private Animator _anim;

    [SerializeField]
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player is NULL");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Animator is NULL");
        }
        if (_audioSource == null)
        {
            Debug.Log("Audio Source is NULL");
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Move down at 4m/s
        transform.Translate(new Vector3(0, -1, 0) * _enemySpeed * Time.deltaTime);

        //Check if bottom of screen
        float enemyPosition = transform.position.y;
        if(enemyPosition <= -6.0f)
        {
            //Respawn at top with a new random position on the x
            transform.position = new Vector3(Random.Range(-11f, 11f), 7.7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //Damage the player
        //Destroy us
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.4f);
        }

        //if other is laser
        //destroy laser
        //destroy us
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            //Add 10 to score
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.4f);
        }
    }
}
