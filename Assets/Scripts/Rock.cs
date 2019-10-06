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

    [SerializeField] private Rock[] m_linkedRocks;
    [SerializeField] private GameObject m_linkSprite;
    [SerializeField] private Color m_linkColor = new Color(0, 0, 0, 1f);
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public bool canMoveRight => m_canMoveRight;
    
    public bool canMoveLeft => m_canMoveLeft;
    
    public bool canMoveUp => m_canMoveUp;
    
    public bool canMoveDown => m_canMoveDown;

    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public bool Push(Vector2 direction, GameObject by)
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

        return Move(direction, by);
    }

    public bool PushByLink(Vector2 direction, GameObject by)
    {
        return Move(direction, by);
    }

    public void SetLinkIconActive()
    {
        m_linkSprite.SetActive(true);
    }

    public void SetLinkColor(Color color)
    {
        m_linkSprite.GetComponent<SpriteRenderer>().color = color;
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        if (m_linkedRocks.Length > 0) {
            SetLinkIconActive();
            SetLinkColor(m_linkColor);
            foreach (Rock linkedRock in m_linkedRocks) {
                linkedRock.SetLinkIconActive();
                linkedRock.SetLinkColor(m_linkColor);
            }
        }
    }

    private bool Move(Vector3 direction, GameObject by)
    {
        if (!CanMoveTo(direction)) {
            return false;
        }
        
        transform.Translate(direction);

        foreach (Rock linkedRock in m_linkedRocks) {
            if (linkedRock.gameObject != by) {
                linkedRock.PushByLink(direction, gameObject);
            }
        }

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
}
