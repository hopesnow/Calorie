using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerExit(Collider exitObject)
    {
        if(exitObject.tag == "Player")
        {
            exitObject.GetComponent<DebugController>().SetGameOver();
            exitObject.GetComponent<PlayerAnimation>().SetGameOver();
//            exitObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
