using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using TMPro;

public class SoundSettings : MonoBehaviour
{
    [SerializeField]
    private Button _buttonclose;
    [SerializeField]
    private Button _buttonopen;
    
    [SerializeField]
    private Slider _allaudioslider;
    [SerializeField]
    private Slider _musicLvl;
    [SerializeField]
    private Slider _sfxLvl;
    
    [SerializeField]
    private TextMeshProUGUI _textallaudios;
    [SerializeField]
    private TextMeshProUGUI _textmusicLvl;
    [SerializeField]
    private TextMeshProUGUI _textsfxLvl;
   
    [SerializeField]
    private AudioMixer _audiomixer;
    [SerializeField]
    private GameObject _root;
    [SerializeField]
    private string allsounds;
    [SerializeField]
    private string musiclvl;
    [SerializeField]
    private string sfxLvl;

    private void Start()
    {
        _root.SetActive(false);
    }
    private void OnEnable()
    {
        _audiomixer.GetFloat(allsounds, out var valueA);
        _textallaudios.text = normValue(valueA).ToString("F0");
        _audiomixer.GetFloat(musiclvl, out var valueM);
        _textmusicLvl.text = normValue(valueM).ToString("F0");
        _audiomixer.GetFloat(sfxLvl, out var valueS);
        _textsfxLvl.text = normValue(valueS).ToString("F0");
        _buttonopen.onClick.AddListener(OpenOnClick);
        _buttonclose.onClick.AddListener(CloseonClick);
        _allaudioslider.onValueChanged.AddListener(AllSoundSliderValueChange);
        _musicLvl.onValueChanged.AddListener(MusicSliderValueChange);
        _sfxLvl.onValueChanged.AddListener(SfxSliderValueChange);

    }

    private void AllSoundSliderValueChange(float value)
    {
        _audiomixer.SetFloat(allsounds, value);
        _textallaudios.text = normValue(value).ToString("F0");
    }

    private void MusicSliderValueChange(float value)
    {
        _audiomixer.SetFloat(musiclvl, value);
        _textmusicLvl.text = normValue(value).ToString("F0");
    }

    private void SfxSliderValueChange(float value)
    {
        _audiomixer.SetFloat(sfxLvl, value);
        _textsfxLvl.text = normValue(value).ToString("F0");
    }

    private float normValue(float value)
    {
        return value + 80;
    }

    private void OpenOnClick()
    {
        _root.SetActive(true);
    }


    private void CloseonClick()
    {
        _root.SetActive(false);
    }
    private void OnDisable()
    {
        _buttonopen.onClick.RemoveListener(OpenOnClick);
        _buttonclose.onClick.RemoveListener(CloseonClick);
        _allaudioslider.onValueChanged.RemoveListener(AllSoundSliderValueChange);
        _musicLvl.onValueChanged.RemoveListener(MusicSliderValueChange);
        _sfxLvl.onValueChanged.RemoveListener(SfxSliderValueChange);
    }
}
