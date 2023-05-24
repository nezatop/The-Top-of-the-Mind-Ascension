using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Range(0, 10)][SerializeField] private float AliveTime = 5.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(die(AliveTime));
        }
    }

    public IEnumerator die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
