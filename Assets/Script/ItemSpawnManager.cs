using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public float spawnTime = 5f;
    public BoxCollider spawnArea;
    public GameObject item;

    private Vector3 upperLeftCorner;
    private Vector3 bottomRightCorner;
    private System.Random random = new System.Random();

	void Start ()
    {
        upperLeftCorner = spawnArea.bounds.min;
        bottomRightCorner = spawnArea.bounds.max;

        upperLeftCorner.y = bottomRightCorner.y;

        Instantiate(item, upperLeftCorner, new Quaternion());
        Instantiate(item, bottomRightCorner, new Quaternion());

        StartCoroutine(SpawnItemRandomly());
    }

	void Update ()
    {
	}

    public IEnumerator SpawnItemRandomly()
    {
        int cnt = 0;
        while (cnt < 50)
        {
            int x = random.Next((int)upperLeftCorner.x, (int)bottomRightCorner.x);
            int z = random.Next((int)upperLeftCorner.z, (int)bottomRightCorner.z);
            GameObject o = Instantiate(item, new Vector3(x, upperLeftCorner.y, z), new Quaternion());
            PlayerColor c = (PlayerColor)random.Next(0, 4);
            Debug.Log(c);
            o.GetComponent<PaintBucketBehaviour>().SetColor(c);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
