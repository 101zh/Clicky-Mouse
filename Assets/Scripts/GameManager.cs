using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    public bool isGameActive;

    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    string difficulty;


    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetString("Difficulty");
        if (difficulty.Equals("Easy"))
        {
            spawnRate = 1.5f;
        }
        else if (difficulty.Equals("Medium"))
        {
            spawnRate = 1.0f;
        }
        else if (difficulty.Equals("Hard"))
        {
            spawnRate = 0.5f;
        }

        isGameActive = true;
        StartCoroutine(SpawnTarget());
        scoreText.text = "Score: " + score;
        gameOverScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        int highScore = PlayerPrefs.GetInt("High Score");
        if (highScore < score)
        {
            PlayerPrefs.SetInt("High Score", score);
            highScore = score;
        }
        highScoreText.text = "High Score: " + highScore;

        isGameActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
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
    }

    public void StartGame(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
