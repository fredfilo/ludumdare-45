using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private LayerMask m_wallsLayer;
    
    private Rigidbody2D m_rigidBody;
    private bool m_isMoving = false;
    private Vector3 m_velocity;


    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    // Start is called before the first frame update
    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
        // Check if we can go there.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, offset.magnitude, m_wallsLayer);
        if (hit.collider) {
            return;
        }

        StartCoroutine(MoveTo(transform.position + offset));
    }

    private IEnumerator MoveTo(Vector3 destination)
    {
        m_isMoving = true;
        m_rigidBody.velocity = (destination - transform.position) * m_moveSpeed;
        float distance;
        
        do {
            distance = Vector3.Distance(transform.position, destination);
            yield return null;
        } while (distance > 0.1f);

        m_rigidBody.velocity = Vector3.zero;
        transform.position = destination;
        m_isMoving = false;
    }
}
