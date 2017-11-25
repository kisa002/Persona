using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTeleporter : MonoBehaviour
{
    public BoxCollider area;
    public float margin = 60f;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

	void Start ()
    {
        minX = area.bounds.min.x - margin;
        maxX = area.bounds.max.x + margin;
        minZ = area.bounds.min.z - margin;
        maxZ = area.bounds.max.z + margin;
        Debug.Log(minZ);
    }

	void LateUpdate ()
    {
        float x = transform.position.x, z = transform.position.z;

		if(x < minX)
        {
            x = maxX - (margin / 2);
            Debug.Log("UnderX");
        }
        else if(maxX < x)
        {
            x = minX + (margin / 2);
            Debug.Log("OverX");
        }

        if (z < minZ)
        {
            z = maxZ - (margin / 2);
            Debug.Log("UnderZ");
        }
        else if (maxZ < z)
        {
            z = minZ + (margin / 2);
            Debug.Log("OverZ");
        }

        transform.position = new Vector3(x, transform.position.y, z);
    }
}
