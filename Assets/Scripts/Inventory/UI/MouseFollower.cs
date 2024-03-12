using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    private Camera mainCam;

    [SerializeField]
    private UIInventoryItem item;

    public void Awake()
    {
        canvas= transform.root.GetComponent<Canvas>();
        mainCam= Camera.main;
        item= GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);

    }
    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,
            Input.mousePosition,canvas.worldCamera, out pos);
        transform.position = canvas.transform.TransformPoint(pos);
    }
    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
