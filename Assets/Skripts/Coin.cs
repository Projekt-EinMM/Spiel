using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        //Check that the object we collided is the Player
        if (other.gameObject.name != "MainCharacter")
        {
            return;
        }
        //Add Player Score
        GameManager.inst.IncrementScore();
        
        //Remove Coins that are not collected
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
