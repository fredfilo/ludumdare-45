using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private int m_value;
    [SerializeField] private SpriteRenderer m_numberSpriteRenderer;
    [SerializeField] private Sprite[] m_availableNumberSprites;
    [SerializeField] private TextMeshPro m_text;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public int value => m_value;
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        GameController.instance.RegisterNumber(this);
        m_value = int.Parse(m_text.text);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) {
            return;
        }

        Destroy(gameObject);
    }
}
