using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject trail;
    [SerializeField] float alignmentSpeed = 0.5f; 
    [SerializeField] float rollForce = 10f; 
    [SerializeField] float torqueForce = 5f; 
    [SerializeField] float slowDownThreshold = 0.1f; 
    [SerializeField] int diceID;

    private int targetFace = 1;
    private bool aligning = false;
    private bool isRolling;
    private bool isRollCompleted;
    private Vector3 startPosition;

    // Dice faces
    private Vector3[] diceFaces = {
      new Vector3(0, 0, 0), // 1
      new Vector3(0, 90, -90), // 2
      new Vector3(90, 0, 0), // 3
      new Vector3(-90, 0, 0), // 4
      new Vector3(0, -90, 90), // 5
      new Vector3(0, -90, 180) // 6
  };

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
        startPosition = transform.position;
        rb.maxAngularVelocity = 10f;
    }

    public void Roll()
    {
        isRollCompleted = false;
        rb.isKinematic = false;
        trail.SetActive(true);
        SetTargetFace();
        RollDice();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnRollButtonClick, Roll);
    }

    void FixedUpdate()
    {
        if (isRolling)
        {
            if (rb.velocity.magnitude < slowDownThreshold && rb.angularVelocity.magnitude < slowDownThreshold)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                aligning = true;
                isRolling = false;
            }

        }

        if (aligning)
        {
            AlignFaceUp(targetFace);
        }
    }

    IEnumerator ResetDice()
    {
        trail.SetActive(false);
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        transform.localPosition = startPosition;
        EventManager.Broadcast(GameEvent.OnRollCompleted);
    }

    void RollDice()
    {

        isRolling = true;
        aligning = false;
        Vector3 direction = (target.position - transform.position).normalized;

        Vector3 randomTorque = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        );


        rb.AddForce(direction * rollForce, ForceMode.Impulse);
        rb.AddTorque(randomTorque * torqueForce, ForceMode.Impulse);
    }

    void AlignFaceUp(int faceNumber)
    {
        faceNumber = Mathf.Clamp(faceNumber, 1, 6) - 1;
        Quaternion targetRotation = Quaternion.Euler(diceFaces[faceNumber]);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, alignmentSpeed * Time.fixedDeltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            aligning = false;
            isRollCompleted = true;
            StartCoroutine(ResetDice());
        }
    }

    public void SetTargetFace()
    {
        if (diceID == 0)
            targetFace = DiceRoller.dieValue1;
        else
            targetFace = DiceRoller.dieValue2;
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnRollButtonClick, Roll);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Grid"))
            if (!audioSource.isPlaying)
                audioSource.Play();
    }
}
