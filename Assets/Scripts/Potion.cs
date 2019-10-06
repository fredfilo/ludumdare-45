using System;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private float m_value = 1f;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite m_emptySprite;
    [SerializeField] private LayerMask m_wallsLayer;

    private GameObject m_rockOnTop;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public float value => m_value;

    public bool isEmpty => Math.Abs(m_value) < 0.00001f;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void Empty()
    {
        m_value = 0;
        m_spriteRenderer.sprite = m_emptySprite;
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock")) {
            m_rockOnTop = other.gameObject;
            CheckAccessibility();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == m_rockOnTop) {
            m_rockOnTop = null;
        }
    }

    private void CheckAccessibility()
    {
        int blockedSides = 0;

        if (IsSideBlocked(Vector3.right)) {
            blockedSides++;
        }
        
        if (IsSideBlocked(Vector3.left)) {
            blockedSides++;
        }
        
        if (IsSideBlocked(Vector3.up)) {
            blockedSides++;
        }
        
        if (IsSideBlocked(Vector3.down)) {
            blockedSides++;
        }

        Debug.Log("potion blocked sides = " + blockedSides);
        if (blockedSides >= 3) {
            GameController.instance.notifications.Notify(new PotionBlockedNotification());
        }
    }
    
    private bool IsSideBlocked(Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, direction.magnitude, m_wallsLayer);

        if (hits.Length == 0) {
            return false;
        }

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.gameObject != m_rockOnTop) {
                return true;
            }
        }

        return false;
    }
}
