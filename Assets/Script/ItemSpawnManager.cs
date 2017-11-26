using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public float spawnTime = 5f;
    public int maxSpawn = 10;
    public int currentSpawn = 0;
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
    }

	void Update ()
    {
	}

    public IEnumerator SpawnItemRandomly()
    {
        int cnt = 0;
        while (cnt < 100)
        {
            int x = random.Next((int)upperLeftCorner.x, (int)bottomRightCorner.x);
            int z = random.Next((int)upperLeftCorner.z, (int)bottomRightCorner.z);
            GameObject o = Instantiate(item, new Vector3(x, upperLeftCorner.y, z), new Quaternion());
            o.transform.Rotate(-90f, 0f, 0f);
            PlayerColor c = (PlayerColor)random.Next(0, 4);
            Debug.Log(c);
            PaintBucketBehaviour b = o.GetComponent<PaintBucketBehaviour>();
            b.SetColor(c);
            b.manager = this;
            currentSpawn += 1;
            yield return new WaitUntil(() => currentSpawn < maxSpawn);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
