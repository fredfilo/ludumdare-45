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
}
