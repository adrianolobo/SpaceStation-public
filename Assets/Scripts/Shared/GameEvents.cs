using System;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    public static GameEvents current;

    public event Action onOpenConfigMenu;
    public void openConfigMenu()
    {
        onOpenConfigMenu?.Invoke();
    }

    public event Action onCloseConfigMenu;
    public void closeConfigMenu()
    {
        onCloseConfigMenu?.Invoke();
    }
}
