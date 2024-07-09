using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private CheckPointMng mng;

    public GameObject thisPlayer;
    // Start is called before the first frame update
    void Start()
    {
        mng = GameObject.FindGameObjectWithTag("CheckpointMng").GetComponent<CheckPointMng>();

        if(mng.gameData.CheckPointNo == 0)
        {
            thisPlayer.transform.position = Vector3.zero;
        }
        else
        {
            thisPlayer.transform.position = mng.gameData.SavedPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
