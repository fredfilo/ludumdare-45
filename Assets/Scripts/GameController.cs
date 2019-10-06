using System.Collections;
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
    
    [SerializeField] private SoundsController m_soundsController;
    private List<Number> m_numbers = new List<Number>();
    private SceneTransition m_sceneTransition;
    private string m_nextSceneName;

    // ACCESSORS
    // -------------------------------------------------------------------------

    public static GameController instance => s_instance;

    public SoundsController sounds => m_soundsController;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void RegisterNumber(Number number)
    {
        if (m_numbers.Contains(number)) {
            return;
        }
        
        m_numbers.Add(number);
    }

    public void RegisterSceneTransition(SceneTransition sceneTransition)
    {
        m_sceneTransition = sceneTransition;
        m_sceneTransition.RevealScene();
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

    public void OnLevelCleared(string nextSceneName)
    {
        isPaused = true;
        m_soundsController.PlayLevelCleared();
        m_nextSceneName = nextSceneName;
        
        Invoke(nameof(LoadNextScene), 1f);
    }

    public void OnSceneHidden()
    {
        LoadScene(m_nextSceneName);
    }
    
    public void OnSceneRevealed()
    {
        isPaused = false;
    }

    public void ShowHowToRestartLevel()
    {
        Debug.Log("PRESS R TO RESTART THE LEVEL");
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

    private void Start()
    {
        Debug.Log("GameController Start");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) {
            ReloadScene();
        }
    }
    
    private IEnumerator HideScene()
    {
        isPaused = true;
        
        if (!m_sceneTransition) {
            yield return new WaitForSeconds(0.1f);
        }
        
        m_sceneTransition.HideScene();
    }
    
    private void ReloadScene()
    {
        m_nextSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(HideScene());
    }
    
    private void LoadNextScene()
    {
        Debug.Log("Scenes count = " + SceneManager.sceneCount);
//        m_nextSceneName = SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1).name;
        Debug.Log("next scene name = " + m_nextSceneName);
        StartCoroutine(HideScene());
    }
    
    private void LoadScene(string sceneName)
    {
        m_numbers = new List<Number>();
        SceneManager.LoadScene(sceneName);
    }
}
