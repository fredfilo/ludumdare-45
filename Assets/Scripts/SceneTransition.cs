using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    // STATIC
    // -------------------------------------------------------------------------
    
    private static readonly int AnimatorKeyHide = Animator.StringToHash("hide");
    private static readonly int AnimatorKeyReveal = Animator.StringToHash("reveal");
    
    // PROPERTIES
    // -------------------------------------------------------------------------
    
    [SerializeField] private Image m_image;

    private Animator m_animator;

    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void HideScene()
    {
        m_animator.SetTrigger(AnimatorKeyHide);
    }
    
    public void RevealScene()
    {
        m_animator.SetTrigger(AnimatorKeyReveal);
    }

    public void OnSceneHidden()
    {
        GameController.instance.notifications.Notify(new SceneHiddenNotification());
    }
    
    public void OnSceneRevealed()
    {
        GameController.instance.notifications.Notify(new SceneRevealedNotification());
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        
        GameController.instance.scenes.RegisterSceneTransition(this);
    }
}
