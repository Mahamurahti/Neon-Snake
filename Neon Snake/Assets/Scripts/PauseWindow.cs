using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour{

    private static PauseWindow instance;

    private Button resumeButton;
    private Button mainMenuButton;

    private void Awake()
    {
        instance = this;
        resumeButton = transform.Find("resumeButton").GetComponent<Button>();
        mainMenuButton = transform.Find("goToMainMenu").GetComponent<Button>();
        Hide();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    public static void ShowStatic()
    {
        instance.Show();
        SoundManager.PlaySound(SoundManager.Sound.Pause);
    }

    public static void HideStatic()
    {
        instance.Hide();
        SoundManager.PlaySound(SoundManager.Sound.Pause);
    }

    public void BackToMainMenu()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
