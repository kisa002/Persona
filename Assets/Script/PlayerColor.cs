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
}
