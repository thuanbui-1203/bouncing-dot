using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject gameOverPanel;
    private static readonly WaitForSeconds _waitFor_1_Second = new(1f);
    private static readonly WaitForSeconds _waitFor_5_Second = new(5f);
    private static readonly WaitForSeconds _waitFor_10_Second = new(10f);
    public GameObject prefab;
    public TextMeshProUGUI scoreText;
    public float spawnRate = 3f;
    public float spawnRange = 4.5f;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        StartCoroutine(nameof(SpawnObstacles));
        StartCoroutine(nameof(IncreaseScore));
        StartCoroutine(nameof(IncreaseDifficulty));
    }

    private IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return _waitFor_1_Second;
            if (Time.timeScale > 0)
            {
                score++;
                UpdateScoreText();
            }
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // spawnRate = Random.Range(0f, 2f);
            float randomY = Random.Range(-spawnRange, spawnRange);
            float randomX = Random.Range(0f, 1f);
            Spawner(randomX, randomY);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void Spawner(float x, float y)
    {
        if (x < 0.5f && Time.timeScale > 0)
        {
            Vector3 spawnPos = new(-18, y, 0);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Vector3 spawnPos = new(18, y, 0);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        ObstacleMover.baseSpeed = 0.1f;
        spawnRate = 3f;
        SceneManager.LoadScene("BouncingDot");
    }

    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return _waitFor_5_Second;
            spawnRate -= 0.1f;
            ObstacleMover.baseSpeed += 0.05f;
            Debug.Log("Difficulty Increased! " + "Current Speed: " + ObstacleMover.baseSpeed + "Current Spawn Rate:" + spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 1f)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
