using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    public Color[] playerColors = {Color.red, Color.blue, Color.green, Color.yellow };
    private const string HorizontalAxisPrefix = "Horizontal";
    private const string VerticalAxisPrefix = "Vertical";
    private const string FireKeyPrefix = "Fire";
    public List<Transform> playerSpawnPoints;
    public List<Transform> plebSpawnPoints;
    public int playerCount = 2;
    public int plebCount = 100;
    public GameObject playerPrefab;
    public GameObject plebPrefab;
    [HideInInspector] public List<GameObject> playerTargets;
    public CameraController cameraController;
    private ScoreController scoreController;
    public Fader fader;
    
	// Use this for initialization
	void Awake () {
	//    SpawnPlebs();
        FindPlebs();
        FindPlayers();
        InitPlayers();
        SetCameraTargets();
        
        SetupScoreController();
        
        FadeIn();
	}

    private void SetupScoreController() {
        scoreController = GetComponent<ScoreController>();
        scoreController.Init(plebCount, playerTargets.Count, playerColors);
    }

    public void HandleScoreIncrement(GameObject player) {
        scoreController.IncrementScore(GetPlayerNumber(player));
    }
    
    public int GetPlayerNumber(GameObject player) {
        return playerTargets.IndexOf(player);
    }

    private void SetCameraTargets() {
        List<Transform> camTargets = new List<Transform>(playerTargets.Count);
        
        foreach (GameObject target in playerTargets) {
            camTargets.Add(target.transform);
        }
        cameraController.cameraTargets = camTargets;
    }

    private void FindPlebs() {
        plebCount = 0;
        foreach (GameObject pleb in GameObject.FindGameObjectsWithTag(Tags.PLEB))
        {
            ++plebCount;
        }
    }

    private void FindPlayers() {
        playerTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.PLAYER));
    }
    
    private void InitPlayers() {
        for (int i = 0; i < playerTargets.Count; ++i) {
            playerTargets[i].GetComponent<PlayerMovement>().setInputAxes(HorizontalAxisPrefix + (i+1), VerticalAxisPrefix + (i+1));
            playerTargets[i].GetComponentInChildren<Converter>().color = playerColors[i];
            playerTargets[i].GetComponent<PlayerPush>().inputKey = FireKeyPrefix + (i+1);
        }
    }

    void SpawnPlebs() {
        for (int i = 0; i < plebSpawnPoints.Count; ++i) {
            Instantiate(plebPrefab, plebSpawnPoints[i].position, plebSpawnPoints[i].rotation);
        }
    }
    
	// Update is called once per frame
	void Update () {
	
	}
    
    public void FadeIn() {
        fader.FadeIn();
    }
    
    public void FadeOut() {
        fader.FadeOut();
    }
}
