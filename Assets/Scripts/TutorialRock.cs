using UnityEngine;

public class TutorialRock : MonoBehaviour, INotificationListener
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite m_pressRSprite;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------
    
    public void OnNotification(Notification notification)
    {
        switch (notification.type) {
            case Notification.Type.POTION_BLOCKED:
                m_spriteRenderer.sprite = m_pressRSprite;
                break;
        }
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------
    
    private void Start()
    {
        GameController.instance.notifications.Subscribe(Notification.Type.POTION_BLOCKED, this);
    }

    private void OnDestroy()
    {
        GameController.instance.notifications.Unsubscribe(Notification.Type.POTION_BLOCKED, this);
    }
}
