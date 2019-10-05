using UnityEngine;

public class Operation : MonoBehaviour
{
    public enum Type
    {
        NONE,
        ADDITION,
        SUBTRACTION,
        MULTIPLICATION,
        DIVISION
    }
    
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private Type m_type;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    
    [SerializeField] private Sprite m_additionSprite;
    [SerializeField] private Sprite m_subtractionSprite;
    [SerializeField] private Sprite m_multiplicationSprite;
    [SerializeField] private Sprite m_divisionSprite;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public Type type => m_type;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public int Calculate(int leftNumber, int rightNumber)
    {
        switch (m_type) {
            case Type.ADDITION:
                return leftNumber + rightNumber;
            case Type.SUBTRACTION:
                return leftNumber - rightNumber;
            case Type.MULTIPLICATION:
                return leftNumber * rightNumber;
            case Type.DIVISION:
                return leftNumber / rightNumber;
            default:
                return leftNumber;
        }
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        Sprite sprite = null;
        
        switch (m_type) {
            case Type.ADDITION:
                sprite = m_additionSprite;
                break;
            case Type.SUBTRACTION:
                sprite = m_subtractionSprite;
                break;
            case Type.MULTIPLICATION:
                sprite = m_multiplicationSprite;
                break;
            case Type.DIVISION:
                sprite = m_divisionSprite;
                break;
        }

        m_spriteRenderer.sprite = sprite;
    }
}
