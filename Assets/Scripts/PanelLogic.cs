using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLogic : MonoBehaviour
{
    [SerializeField] Vector2 onScreenPos;
    [SerializeField] Vector2 offScreenPos;
    [Range(1f, 10f)] public float speed = 1f;

    private RectTransform rect;
    private float timer = 0f;
    private bool isOnScreen = false;

    
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = offScreenPos;
        timer = 1f;
    }

    
    void Update()
    {
        if (isOnScreen)
        {
            MovePanelUp();
        }
        else
        {
            MovePanelDown();
        }
    }

    private void MovePanelUp()
    {
        rect.anchoredPosition = Vector2.Lerp(offScreenPos, onScreenPos, timer);
        if (timer < 1f)
        {
            timer += Time.deltaTime * speed;
        }
    }

    private void MovePanelDown()
    {
        rect.anchoredPosition = Vector2.Lerp(onScreenPos, offScreenPos, timer);
        if (timer < 1f)
        {
            timer += Time.deltaTime * speed;
        }
    }

    public void ShowPanel(bool show)
    {
        isOnScreen = show;
        timer = 0f;
    }
}
