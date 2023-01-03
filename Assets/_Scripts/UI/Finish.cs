using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    [SerializeField] Canvas endCanvas;
    [SerializeField] TMP_Text scoreCountTex = null;
    [SerializeField] TMP_Text bestScoreText = null;

    Collector collector;
    [SerializeField] GameObject player;
    float boxCount;
    int coins;

    void Start()
    {
        endCanvas.gameObject.SetActive(false);
        collector = player.GetComponent<Collector>();
        coins = PlayerPrefs.GetInt("coins");
    }

    void Update()
    {
        boxCount = collector.boxes.Count;
        scoreCountTex.text = boxCount.ToString() + " Box Collected!";

        if (player.GetComponent<PlayerMovement>().finished == false)
        {
            endCanvas.gameObject.SetActive(false);
        }
    }

    public void FinishSeq()
    {
        player.GetComponent<Animator>().SetBool("Finish", true);
        Invoke(nameof(Fin), 1);
    }

    public void EndingCanvasFade()
    {
        endCanvas.gameObject.SetActive(true);
        if (boxCount > PlayerPrefs.GetInt("bestscore"))
        {
            PlayerPrefs.SetInt("bestscore", (int)boxCount);
        }
        bestScoreText.text = "your best is " + PlayerPrefs.GetInt("bestscore") + " box";

        PlayerPrefs.SetInt("coins", coins += (int)boxCount);
    }

    void Fin()
    {
        StartCoroutine(player.GetComponent<Collector>().FinishSeq(.1f));
    }
}
