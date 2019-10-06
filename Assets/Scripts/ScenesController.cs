using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    // STATIC
    // -------------------------------------------------------------------------

    private string s_menuSceneName = "Start";
    
    private string s_endSceneName = "End";
    
    private static string[] s_scenes = {
        "Tutorial_01",
        "Tutorial_02",
        "Tutorial_03",
        "Tutorial_04",
        "Tutorial_05",
        "Level_01",
        "Level_02",
        "Level_03"
    };
    
    // PROPERTIES
    // -------------------------------------------------------------------------
    
    private SceneTransition m_sceneTransition;
    private string m_nextSceneName;
    private int m_currentSceneIndex = 0;
    private bool m_currentSceneIsLevel;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public bool currentSceneIsLevel
    {
        get {
            string currentSceneName = SceneManager.GetActiveScene().name;

            return currentSceneName != s_menuSceneName && currentSceneName != s_endSceneName;
        }
    }
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public int GetLevelNumber()
    {
        return m_currentSceneIndex + 1;
    }
    
    public void RegisterSceneTransition(SceneTransition sceneTransition)
    {
        m_sceneTransition = sceneTransition;
        m_sceneTransition.RevealScene();
    }

    public void StartTransitionToNextSceneWithDelay(float delay)
    {
        Invoke(nameof(StartTransitionToNextScene), delay);
    }
    
    public void StartTransitionToFirstScene()
    {
        m_currentSceneIndex = 0;
        m_nextSceneName = s_scenes[m_currentSceneIndex];
        
        StartCoroutine(HideScene());
    }
    
    public void StartTransitionToNextScene()
    {
        if (m_currentSceneIndex >= s_scenes.Length - 1) {
            m_nextSceneName = s_endSceneName;
        }
        else {
            m_currentSceneIndex++;
            m_nextSceneName = s_scenes[m_currentSceneIndex];
        }
        
        StartCoroutine(HideScene());
    }
    
    public void ReloadScene()
    {
        m_nextSceneName = s_scenes[m_currentSceneIndex];
        StartCoroutine(HideScene());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(m_nextSceneName);
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private IEnumerator HideScene()
    {
        if (!m_sceneTransition) {
            yield return new WaitForSeconds(0.1f);
        }
        
        m_sceneTransition.HideScene();
    }
}
