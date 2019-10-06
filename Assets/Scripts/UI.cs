using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private TextMeshProUGUI m_levelText;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnRestartGameButton()
    {
        Debug.Log("Restart");
        GameController.instance.scenes.StartTransitionToFirstScene();
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        if (GameController.instance.scenes.currentSceneIsLevel) {
            m_levelText.text = "Level " + GameController.instance.scenes.GetLevelNumber();
        }
    }
}
