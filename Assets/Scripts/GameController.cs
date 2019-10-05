using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const float FLOAT_COMPARISON_TOLERANCE = 0.0001f;
        
    // STATIC
    // -------------------------------------------------------------------------

    private static GameController s_instance;
    
    // PROPERTIES
    // -------------------------------------------------------------------------

    public bool isPaused;
    
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

    public void OnLevelCleared()
    {
        // TODO: Scene transition.
        
        LoadNextScene();
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void LoadScene(string sceneName)
    {
        m_numbers = new List<Number>();
        SceneManager.LoadScene(sceneName);
        isPaused = false;
    }
    
    private void LoadScene(int sceneIndex)
    {
        m_numbers = new List<Number>();
        SceneManager.LoadScene(sceneIndex);
        isPaused = false;
    }
}
