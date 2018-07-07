using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTrigger : MonoBehaviour
{
    public int playerCount;
    bool isFinish;
    [SerializeField]
    private Text finishText;
    private void Start()
    {
        isFinish = false;
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        Debug.Log(string.Format("Start with {0} Players", playerCount));
        // test
        playerCount = 2;
        finishText.enabled = false;
    }

    private void Update()
    {
        if(playerCount<=1 && !isFinish)
        {
            isFinish = true;
            StartCoroutine(SetFinishText());
        }
    }

    private void OnTriggerExit(Collider exitObject)
    {
        if(exitObject.tag == "Player")
        {
            exitObject.GetComponent<DebugController>().SetGameOver();
            exitObject.GetComponent<PlayerAnimation>().SetGameOver();
//            exitObject.GetComponent<BoxCollider>().enabled = false;
            playerCount--;
            Debug.Log(exitObject + " is dead. playerCount=" + playerCount);
        }
    }

    IEnumerator SetFinishText()
    {
        yield return new WaitForSeconds(1);
        GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
        int activeNum = 0;
        foreach (GameObject go in currentPlayers)
        {
            if (go.activeSelf) activeNum++;
        }
        finishText.enabled = true;
        if(activeNum<=0)
        {
            finishText.text = "DRAW!";
            Debug.Log("DRAW!");
        }
        else if (activeNum==1)
        {
            foreach (GameObject go in currentPlayers)
            {
                if(go.activeSelf)
                {
                    finishText.text = currentPlayers[0].name + " is Win!";
                    Debug.Log(currentPlayers[0].name + " is Win!");
                }
            }
        }
    }

    public void Reset()
    {
        Start();
    }
}
