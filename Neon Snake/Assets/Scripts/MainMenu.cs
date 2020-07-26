using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    public void Back()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
    }
}
