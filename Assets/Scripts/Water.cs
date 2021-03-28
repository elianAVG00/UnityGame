using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float originalSpeed;
    Player player;
    [SerializeField] float speedReeductionRatio = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //buscaelobjeto con lscript player adentro
        player = FindObjectOfType<Player>();
        originalSpeed = player.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed *= speedReeductionRatio;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }
    }
}
