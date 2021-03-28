using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] int health = 1;
    [SerializeField] float speed = 1;
    [SerializeField] int scorePoints = 100;
    void Start()
    {
        //busco al player y guardo su transform
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        //inicio con una posicion determinada para los enemy, q sale de manera random de alguno de los spawnPoints
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    private void Update()
    {
        //genero una direction que sea la del posicion del player - la del enemy se decir se va a ir acercando cada vez que entre en Update()
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3)direction * Time.deltaTime * speed;

    }

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            GameManager.instance.Score += scorePoints;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            collision.GetComponent<Player>().TakeDamage();
        }
    }
}
