using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const float FLOAT_COMPARISON_TOLERANCE = 0.0001f;
        
    // STATIC
    // -------------------------------------------------------------------------

    private static GameController s_instance;
    
    // PROPERTIES
    // -------------------------------------------------------------------------

    private List<Number> m_numbers = new List<Number>();
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public static GameController instance => s_instance;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void RegisterNumber(Number number)
    {
        if (m_numbers.Contains(number)) {
            return;
        }
        
        m_numbers.Add(number);
    }

    public bool IsNumberAtPosition(Vector3 position)
    {
        foreach (Number number in m_numbers) {
            if (Vector3.Distance(position, number.transform.position) < 0.25f) {
                return true;
            }
        }
        
        return false;
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void Awake()
    {
        if (s_instance == null) {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (s_instance != this) {
            Destroy(gameObject);
        }
    }
}
