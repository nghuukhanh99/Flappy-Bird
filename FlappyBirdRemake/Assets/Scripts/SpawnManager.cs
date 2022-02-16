using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SpawnManager : MonoBehaviour
{
    PlayerCtrl playCtrl;

    public GameObject[] obstaclesPrefabs;

    private float spawnPosX = -1.7f;

    private float spawnPosMinY = -1.20f;

    private float spawnPosMaxY = 2.2f;

    private float spawnPosZ = 0.2f;

    private float startDelay = 2;

    private float repeatRate = 1.5f;

    private float spawnRate = 1.5f;

    public TextMeshProUGUI gameOverText;

    public bool isGameActive = false;

    public Button restartButton;

    public Button startButton;

    public Button scoreButton;

    public GameObject titleScreen;

    public bool canTouch = false;

    public GameObject tapTitle;

    public GameObject highScoreBoard;

    AudioSource UiAudio;

    public AudioClip restartAudio;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;

        canTouch = false;

        highScoreBoard.gameObject.SetActive(false);

        InvokeRepeating("SpawnObstacles", startDelay, repeatRate);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObstacles() // sinh ra pipe
    {
        isGameActive = true;

        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);

            int pipeIndex = Random.Range(0, obstaclesPrefabs.Length);

            Vector3 spawnPos = new Vector3(spawnPosX, Random.Range(spawnPosMinY, spawnPosMaxY), spawnPosZ);

            Instantiate(obstaclesPrefabs[pipeIndex], spawnPos, obstaclesPrefabs[pipeIndex].transform.rotation);
        }
        
    }
    
    public void StartGame() // ham bat dau game
    {
        isGameActive = true;

        canTouch = true;

        StartCoroutine(SpawnObstacles());

        titleScreen.gameObject.SetActive(false);

        startButton.gameObject.SetActive(false);

        scoreButton.gameObject.SetActive(false);

        tapTitle.gameObject.SetActive(false);

        highScoreBoard.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void GameOver() // screen game over
    {
        restartButton.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(true);

        scoreButton.gameObject.SetActive(true);

        isGameActive = false;

        canTouch = false;

        Time.timeScale = 0;

        highScoreBoard.gameObject.SetActive(true);

        // pause everything when object dead
    }

    public void RestartGame() // nut restart game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        highScoreBoard.gameObject.SetActive(false);
    }

    public void seeBestScore()
    {
        highScoreBoard.gameObject.SetActive(true);
    }

    
}
