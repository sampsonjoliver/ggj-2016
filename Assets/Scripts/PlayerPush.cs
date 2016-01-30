using UnityEngine;
using System.Collections;

public class PlayerPush : MonoBehaviour {

    public Vector3 spawnPosition;
    
    public GameObject spawnPrefab;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetKeyDown(KeyCode.Space)) {
           // spawn a push volume
           GameObject pushGo = (GameObject)Instantiate(spawnPrefab, transform.position + (transform.rotation * spawnPosition), transform.rotation);
           //pushGo.transform.parent = gameObject.transform;
           Push push = pushGo.GetComponent<Push>();
           if(push == null) // useless check, will always be there as long as correct prefab used
            Debug.LogError("Spawned PushVolume does not have Push component!!!! :'(");
           push.player = gameObject.transform;
       }
	}
}
