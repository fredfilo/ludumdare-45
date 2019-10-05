using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] [Range(0.1f, 0.9f)] private float m_moveSpeed = 0.5f;
    [SerializeField] private LayerMask m_wallsLayer;
    
    private bool m_isMoving = false;
    private Vector3 m_velocity;


    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (m_isMoving) {
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
        if (GameController.instance.IsNumberAtPosition(destination)) {
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
}
