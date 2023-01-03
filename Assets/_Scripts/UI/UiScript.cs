using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UiScript : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;

    [Header("Main Menu")]
    [SerializeField] RectTransform startMenu;
    [SerializeField] RectTransform startMenuButtons;
    [SerializeField] Image fade;
    [SerializeField] TMP_Text levelCount;
    bool faded = false;
    [SerializeField] TMP_Text coins;

    [Header("Settings")]
    [SerializeField] RectTransform pauseButton;
    [SerializeField] RectTransform pauseScreenButtons;
    [SerializeField] RectTransform settingsButtonsMainS;

    [Header("Buttons")]
    [SerializeField] float inScreenPos;
    [SerializeField] float outOfScreenPos;
    [SerializeField] float buttonSlideSpeed;

    [Header("Joystick")]
    [SerializeField] float joysitickUpPos;
    [SerializeField] float joysitickDownPos;

    [Header("Finish Canvas")]
    //TODO end canvas movements
    [Header("Bools")]
    [SerializeField] bool mainManuActive = true;

    [Header("Player")][SerializeField] GameObject player;

    private void Start()
    {
        levelCount.text = "Level " + PlayerPrefs.GetInt("level").ToString();
        coins.text = "$ " + PlayerPrefs.GetInt("coins"); 
    }

    private void Update()
    {
        coins.text = "$ " + PlayerPrefs.GetInt("coins");
        if (Input.GetKeyDown(KeyCode.O))
        {
            NextLevel();
        }
    }

    //Buttons
    public void StartButton()
    {
        Invoke(nameof(StartGame), 1.75f);
        StartCoroutine(Fade());

        mainManuActive = false;
        UiMovement(startMenu, -1500);
        UiMovement(startMenuButtons, outOfScreenPos);
    }
    void StartGame()
    {
        player.GetComponent<PlayerMovement>().gameRuning = true;
        UiMovement(pauseButton, inScreenPos);
        JoystickMovement(joysitickUpPos);
    }

    public void Home()
    {
        mainManuActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Settings()
    {
        if (mainManuActive)
        {
            UiMovement(startMenuButtons, outOfScreenPos);
            UiMovement(settingsButtonsMainS, inScreenPos);
            JoystickMovement(joysitickDownPos);
            UiMovement(startMenu, -100);
        }
        else if (!mainManuActive)
        {
            player.GetComponent<PlayerMovement>().gameRuning = false;
            UiMovement(pauseButton, outOfScreenPos);
            UiMovement(pauseScreenButtons, inScreenPos);
            JoystickMovement(joysitickDownPos);
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("new", 0);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Close()
    {
        if (mainManuActive)
        {
            UiMovement(startMenuButtons, inScreenPos);
            UiMovement(settingsButtonsMainS, outOfScreenPos);
            JoystickMovement(joysitickDownPos);
            UiMovement(startMenu, 0);
        }
        else if (!mainManuActive)
        {
            UiMovement(pauseButton, inScreenPos);
            UiMovement(pauseScreenButtons, outOfScreenPos);
            player.GetComponent<PlayerMovement>().gameRuning = true;
            JoystickMovement(joysitickUpPos);
        }
    }

    void UiMovement(RectTransform rectTransform, float targetPos)
    {
        rectTransform.DOAnchorPosX(targetPos, buttonSlideSpeed);
    }

    void JoystickMovement(float pos)
    {
        player.GetComponent<PlayerMovement>().JoystickMove(pos);
    }

    IEnumerator Fade()
    {
        if (fade.color.a == 0 || !faded)
        {
            fade.DOFade(1, 1);
            yield return new WaitForSeconds(1);
            fade.DOFade(0, 1);
            faded = true;
        }
    }



}
