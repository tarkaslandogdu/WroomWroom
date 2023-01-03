using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerModel")] [SerializeField] GameObject playerIndex;
    [Header("Movement")]
    public float moveSpeed = 2f;
    [SerializeField] float horizontalSpeed = 2f;
    [SerializeField] float turnSpeed = 8f;
    [SerializeField] Joystick joystick;

    [Header("Components")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Collector collector;

    [Header("LevelBoundary")]
    [SerializeField] float leftSide = -2.5f;
    [SerializeField] float rightSide = 2.5f;

    [Header("Bools")]
    public bool gameRuning = false;
    public bool finished = false;
    public bool crashed = false;

    [SerializeField] GameObject canvas;

    void Start() => playerIndex.transform.GetChild(PlayerPrefs.GetInt("playerindex")).transform.gameObject.SetActive(true);

    void Update() => Movement();

    void Movement()
    {
        if (finished || !gameRuning || crashed) return;

        float turnVectorJoystick = joystick.Horizontal * turnSpeed;

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward, Space.World);
        transform.rotation = Quaternion.Euler(new Vector3(0, turnVectorJoystick, 0));

        //Joystick movement
        if (joystick.Horizontal < 0)
        {
            if (this.gameObject.transform.position.x > leftSide)
            {
                transform.Translate(horizontalSpeed * Time.deltaTime * Vector3.left);
                transform.rotation = Quaternion.Euler(new Vector3(0, turnVectorJoystick, 0));
            }
        }
        if (joystick.Horizontal > 0)
        {
            if (this.gameObject.transform.position.x < rightSide)
            {
                transform.Translate(-1 * horizontalSpeed * Time.deltaTime * Vector3.left);
                transform.rotation = Quaternion.Euler(new Vector3(0, turnVectorJoystick, 0));
            }
        }

        GetComponent<Animator>().SetFloat("Turning", turnVectorJoystick);
    }

    public void JoystickMove(float upDownDeger) => joystick.gameObject.transform.DOMoveY(upDownDeger, .5f);

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
        {
            canvas.GetComponent<Finish>().FinishSeq();
        }
    }


}
