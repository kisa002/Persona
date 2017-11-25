using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketBehaviour : MonoBehaviour
{
    public PlayerColor color;
    public ItemSpawnManager manager;

	void Start ()
    {
        SetColor(color);
	}

	void Update ()
    {
		
	}

    void OnDestroy()
    {
        manager.currentSpawn -= 1;
    }

    public void SetColor(PlayerColor c)
    {
        color = c;
        gameObject.GetComponent<Renderer>().materials[1].color = PlayerColorManager.GetColor(color);
    }
}
