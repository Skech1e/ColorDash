using UnityEngine;

public struct GateData
{
    public E_Colour colour { get; private set; }
    public bool isPassed { get; private set; }

    public GateData(E_Colour colour)
    {
        this.colour = colour;
        isPassed = false;
    }

    public void IsPassed() => isPassed = true;
    public void Reset() => isPassed = false;
}
public enum E_Colour
{
    Red = 0, Green = 1, Blue = 2, White = 3
}