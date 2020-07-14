using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public bool isOn = true;
    public Sprite onSprite;
    public Sprite offSprite;
    private Image buttonImage;
    private TextMeshProUGUI labelText;
    public Color32 offColor;
    public Color32 onColor;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        labelText = GetComponentInChildren<TextMeshProUGUI>();
        isOn = SoundManager.Instance.isAudioOn;
        setButtonState();
    }

    public void toggle()
    {
        isOn = !isOn;
        setButtonState();
    }

    private void setButtonState()
    {
        if (isOn)
        {
            SoundManager.Instance.unMuteAllSounds();
            buttonImage.sprite = offSprite;
            labelText.text = "OFF";
            labelText.color = offColor;
            return;
        }
        SoundManager.Instance.muteAllSounds();
        buttonImage.sprite = onSprite;
        labelText.text = "ON";
        labelText.color = onColor;
    }
}
