using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMapGenerator : MonoBehaviour {

    public Dictionary<Vector2, TitleInfo> Map { get; private set; }
    public Dictionary<Vector2, GameObject> sponObject;

    public Vector2 mapSize;
    public float delta;

    // Use this for initialization
    void Start () {
        GeneratorMap();

    }
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonUp("Paint"))
        {
            DrawColorPlayer(GameObject.Find("GreenPlayer").GetComponent<BasePlayer>());
        }
	}

    private void GeneratorMap()
    {        
        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        //Vector3 basePos = new Vector3(-width/2.0f, 0.0f, height/2.0f);
        Vector3 basePos = new Vector3(0.0f,0.0f,0.0f);

        GameObject borad = new GameObject();
        Map = new Dictionary<Vector2, TitleInfo>();
        sponObject = new Dictionary<Vector2, GameObject>();

        //TitleMapInstance = new TitleMap();

        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                GameObject obj = Instantiate(original: Resources.Load<GameObject>(path: "GameScene/BaseTitle"), parent: borad.transform);
                obj.transform.localScale = new Vector3(delta, 1.0f, delta);
                obj.transform.position = basePos + new Vector3(j * delta, 0.0f, i * delta);
                obj.GetComponent<TitleInfo>().SetTitle(-1, TitleInfo.TitleType.eTitleBase);
                
                TitleInfo info = new TitleInfo(TitleInfo.TitleType.eTitleBase, -1);
                Map.Add(new Vector2(j, i), info);
                sponObject.Add(new Vector2(j, i), obj);
            }
        }
    }

    public void DrawColorPlayer(BasePlayer player)
    {
        Vector3 playerPos = player.GetComponent<Transform>().localPosition / delta;

        int x = Mathf.FloorToInt(playerPos.x);
        int z = Mathf.FloorToInt(playerPos.z);


        //Vector3 drawStart = new Vector3(x, 0.0f,z) /*+ player.playerLook*/;

        //if(Map.ContainsKey(drawStart))
        //{
        //    GameObject obj;
        //    sponObject.TryGetValue(drawStart, out obj);

        //    obj.GetComponent<MeshRenderer>().materials[0].color = Color.red;

        //    TitleInfo info;
        //    Map.TryGetValue(drawStart, out info);
        //    info.SetTitle(1, TitleInfo.TitleType.eTitleRed);
        //}
    }
}
