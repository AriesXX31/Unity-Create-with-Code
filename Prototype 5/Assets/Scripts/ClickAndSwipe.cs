using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    private bool swiping = false;
    private AudioSource playerAudio;
    public AudioClip swipeSound;
    public AudioClip swipingSound;

    public ShakeCamera shakeCamera;

    void Start()
    {
        shakeCamera = GameObject.Find("Main Camera").GetComponent<ShakeCamera>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerAudio.PlayOneShot(swipingSound, 1.0f);
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            //When hit objects camera shake
            StartCoroutine(shakeCamera.Shaking());

            //Sound effect when we swipe object and destroy it
            playerAudio.Stop();
            playerAudio.PlayOneShot(swipeSound, 1.0f);

            //Destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
