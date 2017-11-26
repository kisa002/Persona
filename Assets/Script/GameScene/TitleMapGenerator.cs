using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMapGenerator : MonoBehaviour {
    public GameObject[] players = new GameObject[4];
    public Vector2 mapSize;
    public float delta;

    private GameObject tileResource;
    // Use this for initialization
    void Start () {
        Debug.Log("Map Gen");
        TileChecker.Reset();
        tileResource = Resources.Load<GameObject>(path: "GameScene/BaseTitle");

        GeneratorMap();
        TileChecker.totalCount = (int)(mapSize.x * mapSize.y);
        TileChecker.tileCount[0] = TileChecker.totalCount;
        TileChecker.RefreshPercent();

        //GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>().BgmIngame();
        Debug.Log("pc2 : " + UIManager.playerCount);
        for(int i=3; i>=UIManager.playerCount; i--)
        {
            players[i].SetActive(false);
        }
        GameObject.FindGameObjectWithTag("StageArea").GetComponent<ItemSpawnManager>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void SetControlable(bool value)
    {
        for(int i=0; i<UIManager.playerCount; i++)
        {
            players[i].GetComponent<BasePlayer>().canMove = value;
        }
        GameObject.FindGameObjectWithTag("StageArea").GetComponent<ItemSpawnManager>().enabled = value;
    }

    private void GeneratorMap()
    {        
        Vector3 basePos = new Vector3(0.0f,0.0f,0.0f);

        GameObject borad = new GameObject("borad");

        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                GameObject obj = Instantiate(original: tileResource, parent: borad.transform);
                obj.transform.localScale = new Vector3(delta, 1.0f, delta);
                obj.transform.position = basePos + new Vector3(j * delta, 0.0f, i * delta);
                obj.GetComponent<TitleInfo>().SetTile(TitleInfo.TileType.eTileBase);
                
            }
        }
    }
}
