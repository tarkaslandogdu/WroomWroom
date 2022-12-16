using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] Canvas gameOverCanvas;

    [Header("Sounds")]
    [SerializeField] Image soundOff;
    [SerializeField] Image soundOn;
    [SerializeField] AudioSource sounds;
    [SerializeField] AudioSource crash; 

    [Header("Music")]
    [SerializeField] Image musicOff;
    [SerializeField] Image musicOn;
    [SerializeField] AudioSource music;

    GameObject player;
    bool mainManuActive = true;

    void Awake()
    {
        GameObject[] canvas = GameObject.FindGameObjectsWithTag("Ui");

        if (canvas.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
        SoundPreferance();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayCarSound();
    }

    //Buttons
    public void StartGame()
    {
        player.GetComponent<PlayerMovement>().gameRuning = true;
        mainManuActive = false;
        mainMenuCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void Settings()
    {
        if (mainManuActive)
        {
            settingsCanvas.gameObject.SetActive(true);
            mainMenuCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(false);
        }
        else if (!mainManuActive)
        {
            settingsCanvas.gameObject.SetActive(true);
            gameOverCanvas.gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>().gameRuning = false;
        }
    }

    public void Close()
    {
        if (mainManuActive)
        {
            settingsCanvas.gameObject.SetActive(false);
            mainMenuCanvas.gameObject.SetActive(true);
            gameOverCanvas.gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>().gameRuning = false;
        }
        else if (!mainManuActive)
        {
            settingsCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>().gameRuning = true;
        }
    }

    public void Home()
    {
        mainManuActive = true;
        settingsCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeathCanvas()
    {
        gameOverCanvas.gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().gameRuning = false;

        if (PlayerPrefs.GetInt("sounds") == 0) crash.Play();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Invoke(nameof(StartGame), .1f);
    }

    //SesKontorl
    public void Sound()
    {
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            PlayerPrefs.SetInt("sounds", 0);
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            sounds.gameObject.SetActive(true);
            crash.gameObject.SetActive(true);
            return;
        }
        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            PlayerPrefs.SetInt("sounds", 1);
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            sounds.gameObject.SetActive(false);
            crash.gameObject.SetActive(false);
            return;
        }
    }

    public void Music()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
            music.Play();
            return;
        }
        if (PlayerPrefs.GetInt("music") == 0)
        {
            PlayerPrefs.SetInt("music", 1);
            musicOn.gameObject.SetActive(false);
            musicOff.gameObject.SetActive(true);
            music.Stop();
            return;
        }
    }

    private void SoundPreferance()
    {
        if (PlayerPrefs.GetInt("sounds") == 0)
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            sounds.gameObject.SetActive(true);
            crash.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("sounds") == 1)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            sounds.gameObject.SetActive(true);
            crash.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("music") == 0)
        {
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
            music.Play();
        }
        if (PlayerPrefs.GetInt("music") == 1)
        {
            musicOn.gameObject.SetActive(false);
            musicOff.gameObject.SetActive(true);
            music.Stop();
        }
    }


    private void PlayCarSound()
    {
        if (PlayerPrefs.GetInt("sounds") == 0
            && player.GetComponent<PlayerMovement>().gameRuning
            && player.GetComponent<PlayerMovement>().alive)
        {
            if (sounds.isPlaying) return;
            if (!sounds.isPlaying) sounds.Play();
        }

        else if (!player.GetComponent<PlayerMovement>().gameRuning
                 && !player.GetComponent<PlayerMovement>().alive)
        {
            sounds.Stop();
        }
    }
}
