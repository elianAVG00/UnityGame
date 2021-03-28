using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //tener acceso desde otro codigo sin tener q instanciarlo, al ser static la variable existe en memoria hasta q se cierre la app
    public static GameManager instance;
    [SerializeField] public int time = 30;
    [SerializeField] public int difficulty = 1;
    [SerializeField] int score;
    public bool gameOver;

    public int Score
    {
        get => score;
        set
        {
            score = value;

            UIManager.Instance.UpdateUIScore(score);    

            if(score % 1000 == 0)
            {
                difficulty++;
            }
        }
    }

    //aplico singleton para instanciar el objeto una unica vez
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }

        gameOver = true;

        UIManager.Instance.ShowGameOverScreen();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
