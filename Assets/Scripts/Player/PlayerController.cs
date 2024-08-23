using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float duration = .5f;
    [SerializeField] AudioSource audioSource;

    private bool isMove;
    public bool IsMove { get { return isMove; } }

    private bool isMoveEnded;
    private Vector2 startingPosition;
    private int totalMoveCount;
    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        startingPosition = transform.position;
        duration = animator.runtimeAnimatorController.animationClips[0].length;
    }
    IEnumerator MoveCor()
    {
        for (int i = 0; i < DiceRoller.TotalDiceNumber; i++)
        {
            if (totalMoveCount >= GameManager.MAX_GRID_COUNT - 1)
            {
                transform.position = startingPosition;
                totalMoveCount = 0;
                yield return new WaitForSeconds(1f);
            }
            isMove = true;
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;
            Vector3 targetPosition = startPosition + Vector3.forward * 5;
            animator.SetBool("Jump", true);
            audioSource.Play();
            while (elapsedTime < duration / 4)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (duration / 4));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            animator.SetBool("Jump", false);
            totalMoveCount++;
            yield return new WaitForSeconds(duration / 2);

        }
        EventManager.Broadcast(GameEvent.OnPlayerMoveEnded);
        isMove = false;
        isMoveEnded = true;
        myCollider.enabled = false;
        myCollider.enabled = true;
    }

    public void Move()
    {
        isMoveEnded = false;
        StartCoroutine(MoveCor());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            Grid grid = other.GetComponent<Grid>();
            if (isMoveEnded)
            {
                grid.CollectItem();
            }
            else
            {
                if (isMove)
                    grid.SetGridColor(Color.grey);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grid"))
        {
            Grid grid = other.GetComponent<Grid>();
            if (!isMoveEnded)
                grid.SetGridColor(Color.red);

        }
    }

   
}
