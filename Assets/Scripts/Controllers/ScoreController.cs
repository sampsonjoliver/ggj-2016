using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    public float winningScore;
    public float[] playerScores;
    public Slider[] playerScoreUIElements;
    public Image[] playerScoreUIFillImages;
    private Color[] playerColors;
    
    public PitFluidControl pitFluid;
    
	// Use this for initialization
	void Start () {
	   
	}
    
    public void Init(int numPlebs, bool[] players, Color[] playerColors) {       
        winningScore = Mathf.Floor(numPlebs / players.Length);
        playerScores = new float[players.Length];
        this.playerColors = playerColors;
        
        for (int i = 0; i < playerScores.Length; ++i) {
            if(players[i]) {
                Debug.Log(playerScoreUIElements[i]);
                playerScoreUIElements[i].gameObject.SetActive(true);
                playerScoreUIElements[i].maxValue = winningScore;
                playerScores[i] = 0f;
            }
       }
    }
	
	// Update is called once per frame
	void Update () {
	   for (int i = 0; i < playerScores.Length; ++i) {
           playerScoreUIElements[i].value = playerScores[i];
           playerScoreUIFillImages[i].color = Color.Lerp(Color.white, playerColors[i], winningScore / playerScores[i]);
       }
	}
    
    public void IncrementScore(int playerNumber) {
        playerScores[playerNumber]++;
        UpdatePitFluid();
        
        // Signal a winner to the game controller
        if(playerScores[playerNumber] >= winningScore) {
            GetComponent<GameController>().OnPlayerWin(playerNumber);
        }
    }
    
    private void UpdatePitFluid() {
        float maxFraction = float.MinValue;
        for (int i = 0; i < playerScores.Length; ++i) {
            maxFraction = Mathf.Max(maxFraction, playerScores[i] / winningScore);
        }
        pitFluid.Set(maxFraction);
    }
}
