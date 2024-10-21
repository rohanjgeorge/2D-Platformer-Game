using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController.PickUpKey();
        Destroy(gameObject);
    }
}
