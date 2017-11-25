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
        gameObject.GetComponent<Renderer>().materials[1].color = PlayerColorManager.GetColor(color);
    }
}
