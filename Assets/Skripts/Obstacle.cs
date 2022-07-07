using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Obstacle : MonoBehaviour

{
    PlayerMovment playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovment>();
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.name == "MainCharacter")
        {
            playerMovement.Die(); //Kill the Player
        }
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
}
