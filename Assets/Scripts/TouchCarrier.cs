using UnityEngine;
class TouchCarrier
{
    public int fingerId;
    public Touch touch;
    public PathLine pathLine;

    public TouchCarrier(int fingerId, Touch touch, PathLine pathLine)
    {
        this.fingerId = fingerId;
        this.touch = touch;
        this.pathLine = pathLine;
    }
}