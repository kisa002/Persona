using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerColor : MonoBehaviour
{
    public GameObject brush;
    public GameObject bucket;
    public BasePlayer bp;

    private Material brushMat;
    private Material bucketMat;

	void Start ()
    {
        brushMat = brush.GetComponent<Renderer>().materials[1];
        bucketMat = bucket.GetComponent<Renderer>().materials[1];

        brushMat.color = PlayerColorManager.GetColor(bp.originalColor);
        bucketMat.color = PlayerColorManager.GetColor(bp.originalColor);

        bucket.SetActive(false);
    }

	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if(other.CompareTag("PaintBucketThrowed"))
        {
            brushMat.color = PlayerColorManager.GetColor(other.GetComponent<PaintBucketThrowedBehaviour>().color);
            bp.brushColor = brushMat.color;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("PaintBucket"))
        {
            if(bucket.activeSelf == false)
            {
                bucket.SetActive(true);
            }
            bucketMat.color = PlayerColorManager.GetColor(other.GetComponent<PaintBucketBehaviour>().color);
            bp.bucketColor = bucketMat.color;
            if(bucketMat.color == PlayerColorManager.GetColor(bp.originalColor))
            {
                brushMat.color = bucketMat.color;
                bp.brushColor = brushMat.color;
            }
            Destroy(other.gameObject);
        }
    }
}
