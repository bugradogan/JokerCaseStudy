using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] TMP_InputField dieInput1;
    [SerializeField] TMP_InputField dieInput2;
    [SerializeField] PlayerController playerController;

    public static int dieValue1;
    public static int dieValue2;

    public static int TotalDiceNumber;

    private int completedRollCount;
    private bool isAllRollCompleted;
    private int totalRepeat;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlayerMoveEnded, Restart);
        EventManager.AddHandler(GameEvent.OnRollCompleted, IsAllDiceRollCompleted);
    }
  
    public void OnRollButtonClick()
    {
        SetInpuField();
        EventManager.Broadcast(GameEvent.OnRollButtonClick);
    }

    void SetInpuField()
    {
        if (!string.IsNullOrEmpty(dieInput1.text))
        {
            int.TryParse(dieInput1.text, out dieValue1);
            if (dieValue1 > 6)
            {
                dieValue1 = Random.Range(1, 7);
                dieInput1.text = dieValue1.ToString();
            }
        }
        else
        {
            dieValue1 = Random.Range(1, 7);
            dieInput1.text = dieValue1.ToString();
        }

        if (!string.IsNullOrEmpty(dieInput2.text))
        {
            int.TryParse(dieInput2.text, out dieValue2);
            if (dieValue2 > 6)
            {
                dieValue2 = Random.Range(1, 7);
                dieInput2.text = dieValue2.ToString();
            }
        }
        else
        {
            dieValue2 = Random.Range(1, 7);
            dieInput2.text = dieValue2.ToString();
        }
        TotalDiceNumber = dieValue1 + dieValue2;
    }

    private IEnumerator TriggerEventRepeatedly()
    {
        yield return new WaitForSeconds(2f);
        if (totalRepeat < GameManager.REPEAT_COUNT)
        {
            OnRollButtonClick();
            totalRepeat++;
        }
        else
            totalRepeat = 0;
    }
    void Restart()
    {
        dieInput1.text = "";
        dieInput2.text = "";
        StartCoroutine(TriggerEventRepeatedly());
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerMoveEnded, Restart);
        EventManager.RemoveHandler(GameEvent.OnRollCompleted, IsAllDiceRollCompleted);
    }


    public void IsAllDiceRollCompleted()
    {
        completedRollCount++;
        if (completedRollCount >= 2)
            isAllRollCompleted = true;


    }

    private void Update()
    {
        if (isAllRollCompleted && !playerController.IsMove)
        {
            playerController.Move();
            isAllRollCompleted = false;
            completedRollCount = 0;

        }

    }

}
