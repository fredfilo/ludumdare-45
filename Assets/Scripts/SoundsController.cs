using UnityEngine;

public class SoundsController : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_rejected;
    [SerializeField] private AudioClip m_levelCleared;
    [SerializeField] private AudioClip m_valueIncrement;
    [SerializeField] private AudioClip m_valueDecrement;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void PlayRejected()
    {
        m_audioSource.PlayOneShot(m_rejected);
    }
    
    public void PlayLevelCleared()
    {
        m_audioSource.PlayOneShot(m_levelCleared);
    }
    
    public void PlayValueIncrement()
    {
        m_audioSource.PlayOneShot(m_valueIncrement);
    }
    
    public void PlayValueDecrement()
    {
        m_audioSource.PlayOneShot(m_valueDecrement);
    }
}
