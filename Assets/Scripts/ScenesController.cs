using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    // STATIC
    // -------------------------------------------------------------------------

    private string s_menuSceneName = "Menu";
    
    private string s_endSceneName = "Tutorial_01";
    
    private static string[] s_scenes = new[] {
        "Tutorial_01",
        "Tutorial_02"
    };
    
    // PROPERTIES
    // -------------------------------------------------------------------------
    
    private SceneTransition m_sceneTransition;
    private string m_nextSceneName;
    private int m_currentSceneIndex = 0;
    private bool m_currentSceneIsLevel;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public bool currentSceneIsLevel => m_currentSceneIsLevel;
    
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
    
    public void StartTransitionToNextScene()
    {
        if (m_currentSceneIndex >= s_scenes.Length - 1) {
            m_currentSceneIsLevel = false;
            m_nextSceneName = s_endSceneName;
        }
        else {
            m_currentSceneIsLevel = true;
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
