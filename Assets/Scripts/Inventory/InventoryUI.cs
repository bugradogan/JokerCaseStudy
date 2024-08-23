using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI appleText;
    [SerializeField] TextMeshProUGUI pearText;
    [SerializeField] TextMeshProUGUI strawberryText;
    [SerializeField] Button rollerButton;
    [SerializeField] TMP_Dropdown dropdown;


    private void Awake()
    {
        EventManager.AddHandler(GameEvent.OnInventoryChanged, OnInventoryChanged);
        EventManager.AddHandler(GameEvent.OnRollButtonClick, OnPlayerMoveStarted);
        EventManager.AddHandler(GameEvent.OnPlayerMoveEnded, OnPlayerMoveEnded);
        UpdateUI();
    }

    private void Start()
    {
        AddOptionsToDropdown();
    }

    void AddOptionsToDropdown()
    {
        List<string> dropOptions = new List<string>();
        dropdown.ClearOptions();
        for (int i = 1; i <= 50; i++)
        {
            dropOptions.Add(i.ToString());
        }
        dropdown.AddOptions(dropOptions);
    }

    public void SetValue(int value)
    {
        GameManager.REPEAT_COUNT = value;
    }

    

    void OnPlayerMoveStarted()
    {
        rollerButton.interactable = false;
    }

    void OnPlayerMoveEnded()
    {
        rollerButton.interactable = true;
    }


    void OnInventoryChanged()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        appleText.text = Inventory.Instance.GetItemCount(ItemFactory.CreateItemData("Apple")).ToString();
        pearText.text = Inventory.Instance.GetItemCount(ItemFactory.CreateItemData("Pear")).ToString();
        strawberryText.text = Inventory.Instance.GetItemCount(ItemFactory.CreateItemData("Strawberry")).ToString();
    }

    private void OnDestroy()
    {
        EventManager.RemoveHandler(GameEvent.OnInventoryChanged, OnInventoryChanged);
        EventManager.RemoveHandler(GameEvent.OnRollButtonClick, OnPlayerMoveStarted);
        EventManager.RemoveHandler(GameEvent.OnPlayerMoveEnded, OnPlayerMoveEnded);
    }
}
