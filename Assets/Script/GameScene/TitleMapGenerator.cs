using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMapGenerator : MonoBehaviour {

    public Dictionary<Vector2, TitleInfo> Map { get; private set; }
    //public Dictionary<Vector2, GameObject> sponObject;

    public Vector2 mapSize;
    public float delta;

    // Use this for initialization
    void Start () {
        GeneratorMap();

    }
	
	// Update is called once per frame
	void Update () {

	}

    private void GeneratorMap()
    {        
        Vector3 basePos = new Vector3(0.0f,0.0f,0.0f);

        GameObject borad = new GameObject("borad");
        
        Map = new Dictionary<Vector2, TitleInfo>();

        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                GameObject obj = Instantiate(original: Resources.Load<GameObject>(path: "GameScene/BaseTitle"), parent: borad.transform);
                obj.transform.localScale = new Vector3(delta, 1.0f, delta);
                obj.transform.position = basePos + new Vector3(j * delta, 0.0f, i * delta);
                obj.GetComponent<TitleInfo>().SetTitle(TitleInfo.TileType.eTileBase);
                
                TitleInfo info = new TitleInfo(TitleInfo.TileType.eTileBase);
                Map.Add(new Vector2(j, i), info);
            }
        }
    }

    public void ChangeTileData(TitleInfo info, Vector3 tilePos)
    {
        if (Map.ContainsKey(tilePos))
        {
            Map[tilePos] = info;
        }
    }
}
