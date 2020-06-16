using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager volume;

    public Slider musicSlider;
    public Slider sfxSlider;

    public float music = 1;
    public float sfx = 1;

    private void Awake()
    {
        if (!volume)
        {
            volume = this;
        }
        else
        {
            music = volume.music;
            sfx = volume.sfx;
            volume = this;
        }
    }

    public void Change()
    {
       
            music = musicSlider.value;
            sfx = sfxSlider.value;

    }

}
