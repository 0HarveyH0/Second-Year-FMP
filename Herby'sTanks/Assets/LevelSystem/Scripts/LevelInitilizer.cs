using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitilizer : MonoBehaviour
{
    public enum levelStates
    {
        Initalise,
        levelActive,
        levelFinished
    }
    public levelStates states;
    public GameObject[] levelList;
    public GameObject Level;
    public bool levelFinished;
    public int levelIndex = 0;
    public GameObject levelScreen;
    public SpawnManager spawnManager;
    [Space]
    [SerializeField] private AudioSource roundStartSFX;
    [Space]
    public LevelDetails levelDetails;
    [SerializeField] private GameObject chalkboardHandler;
    public TextMeshProUGUI MissionName;
    public TextMeshProUGUI MissionLevel;
    public TextMeshProUGUI EnemyCount;
    [Space]
    public bool gameOver = false;
    [SerializeField] private GameObject levelOverHandler;
    [SerializeField] private TextMeshProUGUI levelOverText;
    [SerializeField] private TextMeshProUGUI levelOverCountText;
    private Discord_Controller disc_Controller;

    private void Start()
    {
        gameOver = false;
        levelOverHandler.SetActive(false);
        chalkboardHandler.SetActive(true);
        spawnManager = GameObject.Find("PlayerManager").GetComponent<SpawnManager>();
        disc_Controller = GameObject.Find("DiscordController").GetComponent<Discord_Controller>();
    }
    private void FixedUpdate()
    {
        if (gameOver)
        {
            StartCoroutine(GameOver());
        }
    }
    void InitLevel()
    {           
        Level = Instantiate(levelList[levelIndex]);
        levelDetails = Level.GetComponent<LevelDetails>();
        StartCoroutine(InitialiseLevel());
        Debug.Log("before disc");
        states = levelStates.levelActive;
        disc_Controller.levelCount = levelIndex + 1;
    }
    IEnumerator InitialiseLevel()
    {
        MissionName.text = "Mission:" + levelDetails.missionName;
        MissionLevel.text = "Level: " + levelDetails.missionLevel.ToString();
        if (levelDetails.isPvP)
        {
            EnemyCount.text = "";
        }else if (levelDetails.isCampaign)
        {
            EnemyCount.text = "Enemies: " + levelDetails.enemyCount.ToString();
        }
        levelScreen.SetActive(true);
        roundStartSFX.PlayDelayed(1f);
        yield return new WaitForSeconds(3);
        levelScreen.SetActive(false);
        spawnManager.JoinPlayers();
    }
    void finishLevel()
    {
        DestroyImmediate(Level, true);
        spawnManager.RemovePlayers();
        levelIndex++;
        levelFinished = false;
        //Level = Instantiate(levelList[levelIndex]);
        states = levelStates.Initalise;
    }
    void levelActive()
    {
        if (levelFinished)
        {
            states = levelStates.levelFinished;
        }
    }
    void Update()
    {
        if(levelIndex < levelList.Length)
        {
            switch (states)
            {
                case (levelStates.Initalise):
                    InitLevel();
                    break;
                case (levelStates.levelActive):
                    levelActive();
                    break;

                case (levelStates.levelFinished):
                    finishLevel();
                    break;
            }
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator GameOver()
    {
        chalkboardHandler.SetActive(false);
        levelOverCountText.text = $"You made it to level {levelIndex + 1} , Great Job!";
        levelOverHandler.SetActive(true);
        levelScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        levelScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}