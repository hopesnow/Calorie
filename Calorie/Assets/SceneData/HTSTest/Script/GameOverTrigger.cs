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
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject[] startPlayers;
    [SerializeField]
    Vector3[] playerSpawnLocation;
    private void Start()
    {
        isFinish = false;
//        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        // test
        playerCount = FieldManager.Instance.PlayerNum;
        Debug.Log(string.Format("Start with {0} Players", playerCount));
        //playerCount = 4;
        startPlayers = new GameObject[playerCount];
        playerSpawnLocation = new Vector3[playerCount];
        for(int playerNo = 0; playerNo < playerCount; playerNo++)
        {
            float spawnX = -6;
            float spawnZ = 3;
            if (playerNo % 2 == 1) spawnX = 6;
            if (playerNo / 2 == 1) spawnZ = -3;
            playerSpawnLocation[playerNo] = new Vector3(spawnX,0,spawnZ);
            startPlayers[playerNo] = Instantiate(playerPrefab, playerSpawnLocation[playerNo], Quaternion.identity) as GameObject;
            startPlayers[playerNo].transform.parent = this.transform.parent;
            startPlayers[playerNo].transform.Rotate(new Vector3(0, 180, 0));
            startPlayers[playerNo].name = string.Format("Player{0}", playerNo+1);
            startPlayers[playerNo].GetComponent<DebugController>().SetPlayerNo(playerNo);
            startPlayers[playerNo].GetComponent<PlayerAnimation>().SetPlayerNo(playerNo);
        }
        finishText.enabled = false;
    }

    private void Init()
    {

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
