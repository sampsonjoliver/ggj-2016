using UnityEngine;
using System.Collections;

public class PlayerPush : MonoBehaviour {

    public Vector3 spawnPosition;
    
    public GameObject spawnPrefab;
    public string inputKey;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(inputKey)) {
            // spawn a push volume
            GameObject pushGo = (GameObject)Instantiate(spawnPrefab, transform.position + (transform.rotation * spawnPosition), transform.rotation);
            pushGo.transform.parent = gameObject.transform;
            Push push = pushGo.GetComponent<Push>();
            
            // useless check, will always be there as long as correct prefab used
            if (push == null)  
                Debug.LogError("Spawned PushVolume does not have Push component!!!! :'(");
                
            push.player = gameObject.transform;
        }
    }
}
