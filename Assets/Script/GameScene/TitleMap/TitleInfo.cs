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

    private Material tileMat;
    // Use this for initialization
    void Start () {
        tileMat = GetComponent<MeshRenderer>().materials[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTile(TileType type)
    {
        tileType = type;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bursh"))
        {
            BasePlayer player = other.gameObject.GetComponentInParent<BasePlayer>();
            tileMat.color = PlayerColorManager.GetColor(player.brushColor);

            TileType newType = PlayerColorManager.GetTileType(tileMat.color);

            TileChecker.ChangeOneTile(tileType, newType);
            tileType = newType;
            TileChecker.RefreshPercent();
        }
    }
}
