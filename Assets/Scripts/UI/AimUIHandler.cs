using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimUIHandler : MonoBehaviour
{
    //Local variables
    Image aimImage;
    Transform playerShip;
    RectTransform canvasRectTransform;

    void Awake()
    {
        aimImage = GetComponent<Image>();
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        //Ship is destroyed or not assigned
        if (playerShip == null)
            aimImage.enabled = false;
        else
        {
            //Follow cursor
            Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            Vector2 proportionalPosition = new Vector2(viewportPosition.x * canvasRectTransform.sizeDelta.x, viewportPosition.y * canvasRectTransform.sizeDelta.y);
            Vector2 uiOffset = new Vector2((float)canvasRectTransform.sizeDelta.x / 2f, (float)canvasRectTransform.sizeDelta.y / 2f);
            transform.localPosition = Vector2.Lerp(transform.localPosition, proportionalPosition - uiOffset, Time.deltaTime * 30);
        }
    }

}
