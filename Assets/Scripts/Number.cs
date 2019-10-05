using UnityEngine;

public class Number : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private int m_value;
    [SerializeField] private SpriteRenderer m_numberSpriteRenderer;
    [SerializeField] private Sprite[] m_availableNumberSprites;

    // ACCESSORS
    // -------------------------------------------------------------------------

    public int value => m_value;
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        GameController.instance.RegisterNumber(this);
        SetNumberSprite();
    }

    private void SetNumberSprite()
    {
        if (m_value < 0 || m_value > m_availableNumberSprites.Length) {
            return;
        }

        m_numberSpriteRenderer.sprite = m_availableNumberSprites[m_value];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) {
            return;
        }

        Destroy(gameObject);
    }
}
