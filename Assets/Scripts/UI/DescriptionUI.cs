using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject descriptionPanel;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] string description;

    private bool isPanelOpen;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPanelOpen)
        {
            descriptionPanel.SetActive(true);
            isPanelOpen = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
        isPanelOpen = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = description;
    }
}
