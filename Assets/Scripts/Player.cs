using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Va a tener info del eje del player
    float h;
    float v;
    Vector3 moveDirection;
    float speed = 3;

    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        //les cargo la posicion  
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //cargo en el Vector3 los valores a donde muevo el eje
        moveDirection.x = h;
        moveDirection.y = v;

        //transform almacena los datos del GameObject actual en este caso de Player, deltaTime es el Time de la pc(al multiplicar por el movimiento de player normalizamos su movimiento al de la pc usada)
        transform.position += moveDirection * Time.deltaTime;
    }
}
