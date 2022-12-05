using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highscoreText;
    private int lives;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private bool paused;
    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 120;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }

        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            timerText.SetText("Time: " + Mathf.Round(timeLeft));
            if (timeLeft < 0)
            {
                GameOver();
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

        if (score < 0)
        {
            GameOver();
        }
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
            highscoreText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("highscore", score);
            highscoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        isGameActive = false;
    }

public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
