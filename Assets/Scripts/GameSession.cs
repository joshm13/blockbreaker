using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0.1f, 10f )] [SerializeField] float gameSpeed = 1f;
    int pointsPerBlock;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int lives = 3;
    [SerializeField] AudioClip gameOverSound;
    int lastSceneIndex;
    [SerializeField] AudioClip nextLevelSound;


    //state variables
    [SerializeField] int currentScore = 0;
    
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        pointsPerBlock = Random.Range(50, 100);
        Time.timeScale = gameSpeed;
        livesText.text = "Lives: " + lives.ToString();
        levelText.text = "Level: " + SceneManager.GetActiveScene().buildIndex;
        if(SceneManager.GetActiveScene().buildIndex > lastSceneIndex && 
            SceneManager.GetActiveScene().buildIndex != 10)
        {
            lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            AudioSource.PlayClipAtPoint(nextLevelSound, Camera.main.transform.position);


        }
        if (SceneManager.GetActiveScene().buildIndex >= 10)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }

    }

    public void addToScore()
    {
        currentScore += pointsPerBlock;
        scoreText.text = currentScore.ToString();
    }
    
    public void resetGame()
    {
        Destroy(gameObject);
    }

    public void loseLife()
    {
        lives--;
    }

    public int getLives()
    {
        return lives;
    }

}
