using UnityEngine;
class TouchCarrier
{
    public int fingerId;
    public Touch touch;

    public TouchCarrier(int fingerId, Touch touch)
    {
        this.fingerId = fingerId;
        this.touch = touch;
    }
}