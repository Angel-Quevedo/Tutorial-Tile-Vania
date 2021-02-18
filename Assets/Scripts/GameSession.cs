using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        var numOfGameSessions =  FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
            RestarLevel();
        else
            ResetGameSession();
    }

    public void AddToScore (int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    private void RestarLevel()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
