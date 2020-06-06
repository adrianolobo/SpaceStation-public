using UnityEngine;
using System.Collections;
using TMPro;

public class HighScoreMenuItem : MonoBehaviour
{
    void Start()
    {
        SpaceStation spaceStation = GetComponentInParent<MenuStationLevel>().spaceStation;
        TextMeshProUGUI highScoreText = GetComponent<TextMeshProUGUI>();
        int highScore = Storage.Instance.getHighScore(spaceStation.name);
        highScoreText.SetText(highScore.ToString());
    }

}
