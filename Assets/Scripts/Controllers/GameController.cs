using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    public List<Transform> playerSpawnPoints;
    public List<Transform> plebSpawnPoints;
    public int playerCount = 2;
    public int plebCount = 100;
    public GameObject playerPrefab;
    public GameObject plebPrefab;
    public List<GameObject> playerTargets;
    public CameraController cameraController;
    
    
	// Use this for initialization
	void Start () {
	//    SpawnPlebs();
    //    SpawnPlayers();
       SetCameraTargets();
	}

    private void SetCameraTargets() {
        List<Transform> camTargets = new List<Transform>(playerTargets.Count);
        
        foreach (GameObject target in playerTargets) {
            camTargets.Add(target.transform);
        }
        cameraController.cameraTargets = camTargets;
    }

    private void SpawnPlayers() {
        for (int i = 0; i < plebSpawnPoints.Count; ++i) {
            playerTargets.Add(Instantiate(playerPrefab, plebSpawnPoints[i].position, plebSpawnPoints[i].rotation) as GameObject);
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
}
