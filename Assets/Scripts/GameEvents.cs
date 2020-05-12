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

    public event Action onNewModuleCreated;
    public void newModuleCreated()
    {
        onNewModuleCreated?.Invoke();
    }

    public event Action onCreateNewModule;
    public void createNewModule()
    {
        onCreateNewModule?.Invoke();
    }

    public event Action onStartSpawnSequence;
    public void startSpawnSequence()
    {
        onStartSpawnSequence?.Invoke();
    }

    public event Action onEndSpawnSequence;
    public void endSpawnSequence()
    {
        onEndSpawnSequence?.Invoke();
    }

    public event Action<int> onCargosDelivered;
    public void cargosDelivered(int amountCargosDelivered)
    {
        onCargosDelivered?.Invoke(amountCargosDelivered);
    }
    public event Action onPlayBtnClicked;
    public void playBtnClicked()
    {
        onPlayBtnClicked?.Invoke();
    }
}
