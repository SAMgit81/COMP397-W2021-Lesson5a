using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPanelContoller : MonoBehaviour
{
    public Vector2 offScreenPostion;
    public Vector2 onScreenPostion;

    [Range(0.1f, 3.0f)]
    public float speed = 1.0f;
    public float timer = 0.0f;
    public bool isOnScreen = false;

    public RectTransform rectTransform;
    public CameraController playerCamera;
    public Pausable pausable;
   
    // Start is called before the first frame update
    void Start()
    {
        pausable = FindObjectOfType<Pausable>();
        playerCamera = FindObjectOfType<CameraController>();
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = offScreenPostion;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerCamera.enabled = isOnScreen;
            isOnScreen = !isOnScreen;
            timer = 0.0f;

            if (isOnScreen)
            {
                Cursor.lockState = CursorLockMode.None;
                playerCamera.enabled = false;
            }
            else
            {
                playerCamera.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (isOnScreen)
        {
            MoveControllPanelDown();
        }
        else
        {
            MoveControllPanelUp();
        }
    }

    private void MoveControllPanelDown()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(offScreenPostion, onScreenPostion, timer);

        if(timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }
    }

    private void MoveControllPanelUp()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(onScreenPostion, offScreenPostion, timer);

        if (timer < 1.0f)
        {
            timer += Time.deltaTime * speed;
        }

        if (pausable.isGamePaused)
        {
            pausable.TogglePause();
        }
    }
}
