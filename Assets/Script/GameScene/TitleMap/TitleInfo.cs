using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInfo : MonoBehaviour {

    public enum TitleType
    {
        eTitleBase,
        eTitleRed,
        eTitleGreen,
        eTitleBule,
        eTitleYellow
    }

    public TitleType titleType;
    public int playerIndex;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public TitleInfo() { }
    public TitleInfo(TitleType type, int playerIndex)
    {
        titleType = TitleType.eTitleBase;
        this.playerIndex = playerIndex;
    }

    public void SetTitle(int PlayerIndex, TitleType type)
    {
        playerIndex = PlayerIndex;
        titleType = type;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "b")
        {
            GameObject.Find("TitleMapManager").GetComponent<TitleMapGenerator>().DrawColorPlayer(collision.gameObject.GetComponentInParent<BasePlayer>());
        }
    }
}
