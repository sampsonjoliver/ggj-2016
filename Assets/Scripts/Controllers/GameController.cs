using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class GameController : MonoBehaviour {
    public Color[] playerColors = {Color.red, Color.blue, Color.green, Color.yellow };
    private const string HorizontalAxisPrefix = "Horizontal";
    private const string VerticalAxisPrefix = "Vertical";
    private const string FireKeyPrefix = "Fire";

    public List<Transform> playerSpawnPoints;
    public List<Transform> plebSpawnPoints;
    private int playerCount = 0;
    private int plebCount;
    public GameObject playerPrefab;
    public GameObject plebPrefab;

    public List<GameObject> playerTargets;
    public List<PlayerIndicator> playerIndicators;

    private CameraController cameraController;
    private Rotate cameraRotateController;
    private ScoreController scoreController;
    public Fader fader;
    public Light pitLight;
    public MeshRenderer pitFluid;

    private int winningPlayer;
    private bool playerHasWon;
    private GameState currentGameState;
    private enum GameState {
        STARTING,
        PLAYING,
        ENDING
    }

    private bool[] enabledPlayers = new bool[4];

	// Use this for initialization
	void Awake () {
        cameraController = GetComponentInChildren<CameraController>();
        cameraRotateController = GetComponentInChildren<Rotate>();
        RoundStarting();

        InitPlayers();
	}

	// Update is called once per frame
	void Update () {
        if (currentGameState == GameState.STARTING) {
            CheckEnabledPlayers();
            if (Input.GetButtonDown("Submit")) {
                RoundPlaying();
            }
        } else if (currentGameState == GameState.PLAYING) {

        } else if (currentGameState == GameState.ENDING) {

        }

	   if (Input.GetButtonDown("Quit")) {
           Application.Quit();
       }

       if(Input.GetButtonDown("Reload")) {
            Application.LoadLevel(Application.loadedLevel);
       }
	}

    private void RoundStarting()
    {
        // Players need to opt-in and press return to begin play
        currentGameState = GameState.STARTING;
        FadeIn();
        SetCameraOrbit(true);
        // show all indicators as disabled
        for(int i = 0; i < playerTargets.Count; ++i) {
            enabledPlayers[i] = false;
            playerIndicators[i].gameObject.SetActive(true);
            playerIndicators[i].SetColor(playerColors[i]);
            playerIndicators[i].Set(false);
        }
    }

    private void RoundPlaying()
    {
        // Initialise the players, plebs, and camera targets (so... players, again)
        currentGameState = GameState.PLAYING;
        FindPlebs();
        InitPlayers();
        SetCameraTargets();
        
        SetCameraOrbit(false);

        // disable all indicators
        for(int i = 0; i < playerTargets.Count; ++i) {
            playerIndicators[i].gameObject.SetActive(false);
            playerIndicators[i].Set(false);
        }

        SetupScoreController();
        playerHasWon = false;
    }


    private void RoundEnding()
    {
        // Go to the end-game state
        currentGameState = GameState.ENDING;
        SetCameraOrbit(true);
        pitLight.color = playerColors[winningPlayer];
    }

    private void SetupScoreController() {

        scoreController = GetComponent<ScoreController>();
        scoreController.Init(plebCount, enabledPlayers, playerColors);
    }

    public void HandleScoreIncrement(GameObject player) {
        scoreController.IncrementScore(GetPlayerNumber(player));
    }

    internal void OnPlayerWin(int playerNumber)
    {
        winningPlayer = playerNumber;
        playerHasWon = true;
        Debug.Log("Player Win: " + playerNumber);
        pitLight.color = playerColors[playerNumber];
        Color pitColor = playerColors[playerNumber];
        pitColor *= 0.1f;
        pitColor.a = 0.8f;
        pitFluid.material.color = pitColor;
        RoundEnding();
    }

    private void SetCameraOrbit(bool enabled) {
        cameraController.GetComponentInParent<Rotate>().enabled = enabled;
        CameraLerp lerp = cameraController.GetComponentInParent<CameraLerp>();
        Action onFinish = null;
        if(enabled) {
            cameraController.enabled = !enabled;
            lerp.TargetAngle = 40;
            lerp.TargetDepth = -18;
            lerp.TargetCenter = new Vector3(-0.95f, 1, -0.95f);
        }
        else {
            lerp.TargetAngle = 60;
            lerp.TargetDepth = -14;
            lerp.TargetCenter = Vector3.zero;
            lerp.TargetRotation = 0;
            onFinish = () => cameraController.enabled = true;
        }
        lerp.StartLerp(0.75f, onFinish);
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
        SetCameraOrbit(false);
    }

    private void FindPlebs() {
        plebCount = 0;
        foreach (GameObject pleb in GameObject.FindGameObjectsWithTag(Tags.PLEB))
        {
            ++plebCount;
        }
    }

    private void SpawnPlebs() {
        for (int i = 0; i < plebSpawnPoints.Count; ++i) {
            Instantiate(plebPrefab, plebSpawnPoints[i].position, plebSpawnPoints[i].rotation);
        }
    }

    private void InitPlayers() {
        for (int i = 0; i < playerTargets.Count; ++i) {
            // Enable/disable players
            playerTargets[i].SetActive(enabledPlayers[i]);
            Debug.Log(enabledPlayers[i]);

            // Set up player components
            playerTargets[i].GetComponent<PlayerMovement>().setInputAxes(HorizontalAxisPrefix + (i+1), VerticalAxisPrefix + (i+1));
            playerTargets[i].GetComponent<PlayerPush>().inputKey = FireKeyPrefix + (i+1);
            playerTargets[i].GetComponentInChildren<Converter>().color = playerColors[i];
        }
    }

    private void CheckEnabledPlayers() {
        if (Input.GetButtonDown("Fire1")) {
            enabledPlayers[0] = !enabledPlayers[0];
            playerIndicators[0].Set(enabledPlayers[0]);
        }
        if (Input.GetButtonDown("Fire2")) {
            enabledPlayers[1] = !enabledPlayers[1];
            playerIndicators[1].Set(enabledPlayers[1]);
        }
        if (Input.GetButtonDown("Fire3")) {
            enabledPlayers[2] = !enabledPlayers[2];
            playerIndicators[2].Set(enabledPlayers[2]);
        }
        if (Input.GetButtonDown("Fire4")) {
            enabledPlayers[3] = !enabledPlayers[3];
            playerIndicators[3].Set(enabledPlayers[3]);
        }
    }

    public void FadeIn() {
        fader.FadeIn();
    }

    public void FadeOut() {
        fader.FadeOut();
    }
}
