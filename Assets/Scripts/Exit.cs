using UnityEngine;

public class Exit : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private float m_requiredLevel = 1f;

    // ACCESSORS
    // -------------------------------------------------------------------------

    public float requiredLevel => m_requiredLevel;
}
