using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8f;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 1, 0) * _laserSpeed * Time.deltaTime);
        if(transform.position.y > 8f)
        {
            //Triple shot is three lasers inside a laser container, when you fire and they go off screen these lasers aren't destroyed
            //so need to get the parent and then delete what it contains

            //Check if this object has a parent.
            //If it does, destroy the parent
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
