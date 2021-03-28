using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Va a tener info del eje del player
    float h;
    float v;
    Vector3 moveDirection;
    //Nos permite modificarlo desde Unity
    [SerializeField] public float speed = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera cam;
    Vector2 facingDirection;
    [SerializeField] Transform bulletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    [SerializeField] int health = 10;
    bool poweerShotEnabled;
    bool invulnerable;
    [SerializeField] int invulnerableTime = 3;

    public int Health
    {
        get => health;
        set
        {
            health = value;

            UIManager.Instance.UpdateUIHealth(health);
        }
    }

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
        transform.position += moveDirection * Time.deltaTime * speed;

        //Movimiento de la mira:
        //Asigno a fDirection -> a la posicion del mouse ( con cam lo traduzco de posicion de pantalla a posicion de mundo/juego) y le resto lapos del player para q la mira se vaya moviendo segun el player, no solo segun el mouse
        facingDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        aim.position = transform.position + (Vector3)facingDirection.normalized;

        //Si player presiona boton izq instancio una bala
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            //obtengo la rotacion que necesito que salga la bala(que consigo de la mira)
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);

            if (poweerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }

            //https://docs.unity3d.com/Manual/Coroutines.html
            StartCoroutine(ReloadGun());
        }
    }
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }
    IEnumerator MakeVulnerableAgain()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    public void TakeDamage()
    {
        if(invulnerable == false)
        {
            Health--;
            invulnerable = true;
            StartCoroutine(MakeVulnerableAgain());
            if (Health <= 0)
            {
                GameManager.instance.gameOver = true;
                UIManager.Instance.ShowGameOverScreen();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    //incremntar cadencia de disparo
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowersShot:
                    //activar l  powerShot
                    poweerShotEnabled = true;
                    break;
            }
            Destroy(collision.gameObject, 0.1f);
        }
    }
}