using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;
        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax; 
    }
    
}
