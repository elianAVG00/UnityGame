using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int addedTime = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cuando el player toca un Checkpoint se agrega tiempo y se destruye el objeto checkpoint
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.time += addedTime;
            Destroy(gameObject, 0.1f);
        }
    }
}
