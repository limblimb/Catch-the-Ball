using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public bool isOnPause;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pauseGameText;
    public Button restartButton;
    private int score;
    public int lives;
    public bool isGameActive;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        EscapeButtonDown();
        PauseGame(isOnPause);
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

    public void UpdateScore(int addToScore)
    {
        score += addToScore;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int minusLive)
    {
        lives -= minusLive;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        UpdateLives(0);
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        StartCoroutine("SpawnTarget");
        UpdateScore(0);
    }

    public void PauseGame(bool pause)
    {
        if (pause && isGameActive)
        {
            Time.timeScale = 0;
            pauseGameText.gameObject.SetActive(true);
        }

        if (!pause && isGameActive)
        {
            pauseGameText.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

    }
        private void EscapeButtonDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOnPause = !isOnPause;
        }
    }
}
