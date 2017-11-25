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
            bp.brushColor = other.GetComponent<PaintBucketThrowedBehaviour>().color;
            brushMat.color  = PlayerColorManager.GetColor(bp.brushColor);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("PaintBucket"))
        {
            if(bucket.activeSelf == false)
            {
                bucket.SetActive(true);
            }            
            bp.bucketColor = other.GetComponent<PaintBucketBehaviour>().color;
            bucketMat.color = PlayerColorManager.GetColor(bp.bucketColor);

            if(bp.bucketColor == bp.originalColor)
            {
                brushMat.color = bucketMat.color;
                bp.brushColor = bp.bucketColor;
            }
            Destroy(other.gameObject);
        }
    }
}
