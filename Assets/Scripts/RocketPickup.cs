using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 5f, 0f, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        playerMovement.rocketFuel += 10;
        Destroy(gameObject);
    }
}
