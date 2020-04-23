using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<AbstractStationModule> onModulePlaced;
    public void modulePlaced(AbstractStationModule module)
    {
        onModulePlaced?.Invoke(module);
    }
}
