using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMapGenerator : MonoBehaviour {

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
        //Vector3 basePos = new Vector3(-958.0f, 0.0f, 538.0f);
        
        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        Vector3 basePos = new Vector3(-width/2.0f, 0.0f, height/2.0f);

        GameObject borad = new GameObject();

        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("GameScene/BaseTitle"), borad.transform);
                obj.transform.localScale = new Vector3(delta, 1.0f, delta);
                obj.transform.position = basePos + new Vector3(j * delta, 0.0f, -i * delta);
            }
        }
    }

}
