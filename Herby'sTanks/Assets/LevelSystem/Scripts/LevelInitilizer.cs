using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    public LevelDetails levelDetails;
    public TextMeshProUGUI MissionName;
    public TextMeshProUGUI MissionLevel;
    public TextMeshProUGUI EnemyCount;
    private Discord_Controller disc_Controller;

    private void Start()
    {
        spawnManager = GameObject.Find("PlayerManager").GetComponent<SpawnManager>();
        disc_Controller = GameObject.Find("DiscordController").GetComponent<Discord_Controller>();
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
}