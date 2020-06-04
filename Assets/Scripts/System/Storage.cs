using UnityEngine;
using System.Collections;

public class Storage : Singleton<Storage>
{
    public int getTotalCargos()
    {
        return PlayerPrefs.GetInt("total-cargos");
    }

    public void setTotalCargos(int totalCargos)
    {
        PlayerPrefs.SetInt("total-cargos", totalCargos);
    }
}
