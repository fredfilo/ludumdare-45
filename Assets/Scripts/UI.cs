using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private TextMeshProUGUI m_levelText;
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        m_levelText.text = "Level " + GameController.instance.scenes.GetLevelNumber();
    }
}
