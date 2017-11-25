using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucketBehaviour : MonoBehaviour
{
    public PlayerColor color;

	void Start ()
    {
        SetColor(color);
	}

	void Update ()
    {
		
	}

    public void SetColor(PlayerColor c)
    {
        color = c;
        gameObject.GetComponent<Renderer>().material.color = PlayerColorManager.GetColor(color);
    }
}
