using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInfo : MonoBehaviour {

    public enum TileType
    {
        eTileBase,
        eTileRed,
        eTileGreen,
        eTileBlue,
        eTileYellow
    }

    public TileType tileType;

    private TitleMapGenerator generator;
    private Material tileMat;
    // Use this for initialization
    void Start () {
        generator = GameObject.Find("TitleMapManager").GetComponent<TitleMapGenerator>();
        tileMat = GetComponent<MeshRenderer>().materials[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public TitleInfo() { }
    public TitleInfo(TileType type)
    {
        tileType = TileType.eTileBase;
    }

    public void SetTitle(TileType type)
    {
        tileType = type;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bursh")
        {
            BasePlayer player = other.gameObject.GetComponentInParent<BasePlayer>();
            tileMat.color = player.brushColor;

            tileType = PlayerColorManager.GetTileType(tileMat.color);
            
            generator.ChangeTileData(this, transform.position);
        }
    }
}
