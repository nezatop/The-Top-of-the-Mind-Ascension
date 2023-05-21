using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            else if (Input.GetKey(KeyCode.S))
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            else other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
    }
}