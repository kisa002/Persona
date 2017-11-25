using UnityEngine;


public enum PlayerColor { red, green, blue, yellow };
public static class PlayerColorManager
{
    public static Color GetColor(PlayerColor colorEnum)
    {
        Color result = new Color();
        switch(colorEnum)
        {
        case PlayerColor.red:
            result = Color.red;
            break;
        case PlayerColor.green:
            result = Color.green;
            break;
        case PlayerColor.blue:
            result = Color.blue;
            break;
        case PlayerColor.yellow:
            result = Color.yellow;
            break;
        }
        return result;
    }

    public static TitleInfo.TileType GetTileType(PlayerColor color)
    {
        return GetTileType(GetColor(color));
    }
    public static TitleInfo.TileType GetTileType(Color color)
    {
        if(color.Equals(Color.red))
        {
            return TitleInfo.TileType.eTileRed;
        }
        else if (color.Equals(Color.green))
        {
            return TitleInfo.TileType.eTileGreen;
        }
        else if (color.Equals(Color.blue))
        {
            return TitleInfo.TileType.eTileBlue;
        }
        else if (color.Equals(Color.yellow))
        {
            return TitleInfo.TileType.eTileYellow;
        }
        else
        {
            return TitleInfo.TileType.eTileBase;
        }
    }
}
