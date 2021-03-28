using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Text healthText;
    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text finalScore;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }   
    }

    public void UpdateUIScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateUIHealth(int newHealth)
    {
        healthText.text = newHealth.ToString();
    }

    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }
    //Tengo q llenar final score y activar la pantalla de game over
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        finalScore.text = GameManager.instance.Score.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
