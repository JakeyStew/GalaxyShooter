using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    //ID for powerups
    //0 = Tripleshot
    //1 = Speed
    //3 = Shields
    [SerializeField]
    private int powerID;

    [SerializeField]
    private AudioClip _audioClip;

    private void Update()
    {
        //Move down at a speed of 3
        //When we leave the screen, destroy this object
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //Check if bottom of screen
        float powerUpPos = transform.position.y;
        if (powerUpPos <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //OnTriggerCollision
        //Only be collectible by player (HINT: use tags)
        //On collected, destroy
        if (other.tag == "Player")
        {
            //Communicate with the player script
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            if (player != null)
            {
                switch (powerID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerUpActive();
                        break;
                    case 2:
                        player.ShieldPowerUpActive();
                        break;
                    default:
                        Debug.Log("Default Value!");
                        break;
                } 
            }
            Destroy(this.gameObject);
        }
    }
}
