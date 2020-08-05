using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour 
{
    public AudioMixer audioMixer;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Loader.Load(Loader.Scene.ClassicScene);
    }

    public void QuitGame()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Debug.Log("Bye bye!");
        Application.Quit();
    }

    public void HowToPlay()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Back()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
    }
}
