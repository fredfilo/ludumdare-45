using System;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private bool m_canMoveRight = true;
    [SerializeField] private bool m_canMoveLeft = true;
    [SerializeField] private bool m_canMoveUp = true;
    [SerializeField] private bool m_canMoveDown = true;

    [SerializeField] private LayerMask m_wallsLayer;
    [SerializeField] private Collider2D m_collider;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public bool canMoveRight => m_canMoveRight;
    
    public bool canMoveLeft => m_canMoveLeft;
    
    public bool canMoveUp => m_canMoveUp;
    
    public bool canMoveDown => m_canMoveDown;

    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public bool Push(Vector2 direction)
    {
        if (direction.x > 0 && !m_canMoveRight) {
            return false;
        }
        
        if (direction.x < 0 && !m_canMoveLeft) {
            return false;
        }
        
        if (direction.y > 0 && !m_canMoveUp) {
            return false;
        }
        
        if (direction.y < 0 && !m_canMoveDown) {
            return false;
        }

        return Move(direction);
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        
    }

    private bool Move(Vector3 direction)
    {
        if (!CanMoveTo(direction)) {
            return false;
        }
        
        transform.Translate(direction);

        return true;
    }

    private bool CanMoveTo(Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, direction.magnitude, m_wallsLayer);

        if (hits.Length == 0) {
            return true;
        }

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider && hit.collider.gameObject != gameObject) {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion")) {
            GameController.instance.ShowHowToRestartLevel();
        }
    }
}
