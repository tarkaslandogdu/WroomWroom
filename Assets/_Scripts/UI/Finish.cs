using UnityEngine;
using TMPro;

public class Finish : MonoBehaviour
{
    GameObject player;
    [SerializeField] Canvas endCanvas;
    [SerializeField] TMP_Text scoreCountTex = null;
    [SerializeField] TMP_Text bestScoreText = null;

    float boxCount;


    void Start()
    {
        endCanvas.gameObject.SetActive(false);
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxCount = player.GetComponent<Collector>().boxes.Count;
        scoreCountTex.text = "You Collected " + boxCount.ToString() + " Box";

        

        if (player.GetComponent<PlayerMovement>().finished == false)
        {
            endCanvas.gameObject.SetActive(false);
        }

    }

    public void FinishSeq()
    {
        player.GetComponent<Animator>().SetBool("Finish", true);
        Invoke(nameof(EndingCanvasFade), 2f);
    }

    void EndingCanvasFade()
    {
        endCanvas.gameObject.SetActive(true);
        if (boxCount > PlayerPrefs.GetInt("bestscore"))
        {
            PlayerPrefs.SetInt("bestscore", ((int)boxCount));
        }
        bestScoreText.text = "your best: " + PlayerPrefs.GetInt("bestscore");
    }

}
