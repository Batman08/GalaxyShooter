using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("All Hazards")]
    public GameObject[] hazards;
    [Space]
    [Header("Buttons")]
    public GameObject restartBtn;
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public GameObject backBtn;
    [Space]
    [Header("Hazards Spawn Location")]
    public Vector3 spawnValues;
    [Space]
    public int hazardCount;
    [Space]
    [HideInInspector]
    public int waveCount = 1;
    [Header("Increase Difficulty Of Game")]
    public int increaseDifficultyTime;
    public int increaseDifficultyHazards;
    [Space]
    [Header("Wait Times")]
    public float spawnWait = 0.5f;
    public float startWait;
    public float WaveTxtWaitTime;
    public float waveWait;
    [Space]
    [Header("Game Texts")]
    public Text gameOverText;
    public Text Wave;
    public Text waveNumber;
    public Text scoreText;
    public GameObject EndWaveText;
    public GameObject EndScoreText;


    private AudioSource backGroundAudioSource;
    private bool gameOver;
    [HideInInspector]
    public int score;
    private DateTime startTime;
    [HideInInspector]
    public int _generatePowerUp;
    private int _allHazards;
    //private LivesManager _livesManager;
    private int randInt1, randInt2;

    void Awake()
    {
        EndWaveText.SetActive(value: false);
        EndScoreText.SetActive(value: false);
        instance = this;
        //_livesManager = GetComponent<LivesManager>();
        backGroundAudioSource = GetComponent<AudioSource>();
        restartBtn.SetActive(false);
        backBtn.SetActive(false);
        PauseMenu.SetActive(value: false);
        PauseButton.SetActive(value: true);
        gameOver = false;
        gameOverText.text = "";
        score = 0;
        //Advertisement.Initialize("1567637");
        randInt1 = UnityEngine.Random.Range(0, 5);
        randInt2 = UnityEngine.Random.Range(0, 5);
        //Debug.Log(randInt1 + "-----" + randInt2);
    }

    void Start()
    {
        StartCoroutine("SetUpGame");
    }

    IEnumerator SetUpGame()
    {
        Time.timeScale = 1;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        startTime = DateTime.Now;
        yield return new WaitForSeconds(startWait);
        HideWaveCount();
    }

    void Update()
    {
        _allHazards = hazards.Length;
        _generatePowerUp = UnityEngine.Random.Range(0, _allHazards);

        //   Debug.Log(_generatePowerUp);

        //if (gameOver == true)
        //{
        ////    TouchPad.SetActive(value: false);
        //}
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver == true)
            {
                break;
            }

            Wave.text = "Wave: ";
            waveCount++;
            waveNumber.text = waveCount.ToString();

            TryMakeGameHarder();
            yield return new WaitForSeconds(waveWait);
            HideWaveCount();
            randInt1 = UnityEngine.Random.Range(0, 4);
            randInt2 = UnityEngine.Random.Range(0, 4);
            Debug.Log(randInt1 + "-----" + randInt2);
        }
    }

    private void TryMakeGameHarder()
    {
        int timePassed = (DateTime.Now - startTime).Seconds;

        bool makeGameHarder = timePassed > increaseDifficultyTime;

        if (makeGameHarder)
        {
            spawnWait = spawnWait - (spawnWait * 0.8f);

            hazardCount += increaseDifficultyHazards;

            startTime = System.DateTime.Now;
        }
    }

    void HideWaveCount()
    {
        Wave.text = "";
        waveNumber.text = "";
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "" + score;
    }

    public void GameOver()
    {
        gameOver = true;
        bool ScoreIsGreaterThanHighScore = (PlayerPrefs.GetInt("score") < score);
        if (ScoreIsGreaterThanHighScore)
        {
            PlayerPrefs.SetInt("score", score);
        }

        bool WaveIsGreaterThanHighestWave = (PlayerPrefs.GetInt("wave") < waveCount);
        if (WaveIsGreaterThanHighestWave)
        {
            PlayerPrefs.SetInt("wave", waveCount);
        }

        gameOverText.text = "Game Over";
        EndWaveText.SetActive(value: true);
        EndScoreText.SetActive(value: true);
        scoreText.enabled = false;
        restartBtn.SetActive(true);
        backBtn.SetActive(true);
        PauseButton.SetActive(value: false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        bool ScoreIsGreaterThanHighScore = (PlayerPrefs.GetInt("score") < score);
        if (ScoreIsGreaterThanHighScore)
        {
            PlayerPrefs.SetInt("score", score);
        }

        bool WaveIsGreaterThanHighestWave = (PlayerPrefs.GetInt("wave") < waveCount);
        if (WaveIsGreaterThanHighestWave)
        {
            PlayerPrefs.SetInt("wave", waveCount);
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(value: true);
        PauseButton.SetActive(value: false);
        // TouchPad.SetActive(value: false);
        Time.timeScale = 0;

        #region Other Way Of Doing This
        //PauseMenu.SetActive(!PauseMenu.activeSelf);
        //  Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        #endregion
    }

    public void ContinueGame()
    {
        PauseMenu.SetActive(value: false);
        PauseButton.SetActive(value: true);
        //   TouchPad.SetActive(value: true);
        Time.timeScale = 1;
    }

    public void DisableMusic()
    {
        backGroundAudioSource.mute = !backGroundAudioSource.mute;
    }

    #region Unity Ads Code
    /*void Revive(ShowResult sr)
    {
        if (sr == ShowResult.Finished)
        {
            gameOver = false;
            _livesManager.numLives = 1;
            _livesManager.SpawnPlayer();
            StartCoroutine(SpawnWaves());
        }
    }

    public void RequestRevive()
    {
        ShowOptions so = new ShowOptions
        {
            resultCallback = Revive
        };

        Advertisement.Show("rewardedVideo", so);
    }

    public void PlayAd()
    {
        if (randInt1 == randInt2)
        {
            _ad.ShowNormalAd();
        }
    }*/
    #endregion
}
