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

    public void setHighScore(int currentScore, string stationName)
    {
        if (currentScore < getHighScore(stationName)) return;
        PlayerPrefs.SetInt($"highest-score:{stationName}", currentScore);
    }

    public int getHighScore(string stationName)
    {
        return PlayerPrefs.GetInt($"highest-score:{stationName}");
    }

    public void setAudioStatus(bool audioStatus)
    {
        PlayerPrefs.SetString("audio-status", audioStatus ? "on" : "off");
    }

    public bool getAudioStatus()
    {
        if (PlayerPrefs.GetString("audio-status") == "on") return true;
        if (PlayerPrefs.GetString("audio-status") == "off") return false;
        setAudioStatus(true);
        return true;
    }
}
