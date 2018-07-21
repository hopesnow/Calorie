using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectTestSettings : MonoBehaviour {

    [SerializeField]
    Text playerNumText;

	// Use this for initialization
	void Start () {
        int playerNum = FieldManager.Instance.PlayerNum;
        if (playerNum < 2)
            FieldManager.Instance.PlayerNum = 2;
        playerNumText.text = string.Format("{0} Players",FieldManager.Instance.PlayerNum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementPlayerNum()
    {
        int num = FieldManager.Instance.PlayerNum;
        num++;
        FieldManager.Instance.PlayerNum = num;
        playerNumText.text = string.Format("{0} Players", FieldManager.Instance.PlayerNum);
    }

    public void DecrementPlayerNum()
    {
        int num = FieldManager.Instance.PlayerNum;
        num--;
        FieldManager.Instance.PlayerNum = num;
        playerNumText.text = string.Format("{0} Players", FieldManager.Instance.PlayerNum);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("HTSTest");
    }
}
