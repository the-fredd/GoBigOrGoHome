using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LogicController : MonoBehaviour
{
    public float score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject GameOverScreen;

    /*
    void Start()
    {


    }

    void Update()
    {

    }
    */

    public void AddScore()
    { 
        score++;
        scoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    { 
        GameOverScreen.SetActive(true); 
    }
}
