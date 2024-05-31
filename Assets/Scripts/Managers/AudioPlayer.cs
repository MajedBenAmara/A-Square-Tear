using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioClips = { };
    [SerializeField] private AudioSource _GameMusicTheme, _MainMenuMusicTheme;
    [SerializeField] private Slider _MusicSlider, _SoundSlider;
    private float _MusicVolume = 1, _SoundVolume = 1;

    private void Start()
    {
        LoadVolume();
    }

    public void ChangeSoundVolume()
    {
        _SoundVolume = _SoundSlider.value;
        PlayerPrefs.SetFloat("Sound Volume", _SoundSlider.value);
        foreach (var c in audioClips)
        {
            c.volume = _SoundVolume;
        }

    }
    public void ChangeMusicVolume()
    {
        _MusicVolume = _MusicSlider.value;
        PlayerPrefs.SetFloat("Music Volume", _MusicSlider.value);
        _GameMusicTheme.volume = _MusicVolume;
        _MainMenuMusicTheme.volume = _MusicVolume;

    }

    private void LoadVolume()
    {
        _MusicSlider.value = PlayerPrefs.GetFloat("Music Volume");
        _SoundSlider.value = PlayerPrefs.GetFloat("Sound Volume");
    }

    public void PlayOnButtonHover()
    {
        audioClips[0].Play();
    }

    public void PlayOnButtonClick()
    {
        audioClips[1].Play();
    }

    public void PlayOnSlashShoot()
    {
        audioClips[2].Play();
    }

    public void PlayOnDiscTurn()
    {
        audioClips[3].Play();
        audioClips[3].loop = true;
    }

    public void PlayOnBulletFire()
    {
        audioClips[4].Play();
    }
    public void PlayOnWeaponSelection()
    {
        audioClips[5].Play();
    }

}

