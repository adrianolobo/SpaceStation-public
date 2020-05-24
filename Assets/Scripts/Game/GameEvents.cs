using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onStartSpawnSequence;
    public void startSpawnSequence()
    {
        onStartSpawnSequence?.Invoke();
    }

    public event Action<int> onCargosDelivered;
    public void cargosDelivered(int amountCargosDelivered)
    {
        onCargosDelivered?.Invoke(amountCargosDelivered);
    }
}
