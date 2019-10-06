using System;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private float m_value = 1f;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite m_emptySprite;
    
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
}
