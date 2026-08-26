using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = musicSource.volume;
    }

    public void ChangeVolume()
    {
        musicSource.volume = volumeSlider.value;
    }
}
