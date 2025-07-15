using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Start is called before the first frame update
    // reference to player
    public PlayerController player;

    void Start()
    {
        //grab ref to player
        player = GameObject.Find("Moon").GetComponent<PlayerController>();
    }
    void OnTriggerEnter(Collider other)
    {
        // if the player collides with coin, increase points and destroy
        if (other.name == "Moon")
        {
            player.coinCount++;
            Destroy(this.gameObject);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
