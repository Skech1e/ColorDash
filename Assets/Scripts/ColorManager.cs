using System;
using UnityEngine;

public struct ColorManager
{
    public static Color GetColor(E_Colour colour)
    {
        Color c = colour switch
        {
            E_Colour.Red => Color.red,
            E_Colour.Green => Color.green,
            E_Colour.Blue => Color.blue,
            _ => Color.white
        };
        return c;
    }
}
