using UnityEngine;

public class Exit : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private string m_nextSceneName;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public string nextSceneName => m_nextSceneName;
}
