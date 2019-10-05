using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] [Range(0.1f, 0.9f)] private float m_moveSpeed = 0.5f;
    [SerializeField] private LayerMask m_wallsLayer;
    [SerializeField] private SpriteRenderer m_numberSpriteRenderer;
    [SerializeField] private Sprite[] m_availableNumberSprites;
    [SerializeField] private TextMeshPro m_text;
    
    private int m_value;
    private Operation.Type m_currentOperationType = Operation.Type.NONE;
    private bool m_isMoving = false;
    private Vector3 m_velocity;
    private Operation m_operation;

    // ACCESSORS
    // -------------------------------------------------------------------------
    
    public int value => m_value;
    
    public bool hasOperation => (m_currentOperationType != Operation.Type.NONE);

    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void Start()
    {
        SetNumberSprite();
    }
    
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (m_isMoving || GameController.instance.isPaused) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            MoveToward(Vector2.right);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            MoveToward(Vector2.left);
        } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            MoveToward(Vector2.up);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            MoveToward(Vector2.down);
        }
    }

    private void MoveToward(Vector3 offset)
    {
        Vector3 destination = transform.position + offset;
        
        // Check if there is a number there.
        if (!hasOperation && GameController.instance.IsNumberAtPosition(destination)) {
            return;
        }
        
        // Check if there is a wall there.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, offset.magnitude, m_wallsLayer);
        if (hit.collider) {
            return;
        }

        StartCoroutine(MoveTo(destination));
    }

    private IEnumerator MoveTo(Vector3 destination)
    {
        m_isMoving = true;
        float distance;
        Vector3 direction = (destination - transform.position).normalized;
        
        do {
            Vector3 currentPosition = transform.position;
            distance = Vector3.Distance(currentPosition, destination);
            float moveDistance = Mathf.Lerp(0, distance, m_moveSpeed);
            transform.position = currentPosition + (direction * moveDistance);
            yield return null;
        } while (distance > 0.15f);

        transform.position = destination;
        m_isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Number number = other.GetComponent<Number>();
        if (number) {
            Debug.Log("Hit number: " + number.value);
            if (hasOperation) {
                OperateNumber(number);
                Destroy(other.gameObject);
            }
            return;
        }
        
        Operation operation = other.GetComponent<Operation>();
        if (operation) {
            Debug.Log("Set operation: " + operation.type);
            m_currentOperationType = operation.type;
            Destroy(other.gameObject);
        }
    }

    private void SetNumberSprite()
    {
        m_text.text = m_value.ToString();
    }
    
    private void OperateNumber(Number number)
    {
        if (!hasOperation) {
            return;
        }

        int valueBefore = m_value;
        
        switch (m_currentOperationType) {
            case Operation.Type.ADDITION:
                m_value += number.value;
                break;
            case Operation.Type.SUBTRACTION:
                m_value -= number.value;
                break;
            case Operation.Type.MULTIPLICATION:
                m_value *= number.value;
                break;
            case Operation.Type.DIVISION:
                m_value /= number.value;
                break;
        }

        if (m_value > valueBefore) {
            GameController.instance.sounds.PlayValueIncrement();
        } else if (m_value < valueBefore) {
            GameController.instance.sounds.PlayValueDecrement();
        }
        
        SetNumberSprite();
    }
}
