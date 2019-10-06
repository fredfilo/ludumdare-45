using UnityEngine;

public class GameController : MonoBehaviour, INotificationListener
{
    public const float FLOAT_COMPARISON_TOLERANCE = 0.0001f;

    // STATIC
    // -------------------------------------------------------------------------

    private static GameController s_instance;

    // PROPERTIES
    // -------------------------------------------------------------------------

    public bool isPaused = true;
    
    [SerializeField] private SoundsController m_sounds;
    [SerializeField] private ScenesController m_scenes;
    private NotificationsController m_notifications;

    // ACCESSORS
    // -------------------------------------------------------------------------

    public static GameController instance => s_instance;

    public SoundsController sounds => m_sounds;

    public ScenesController scenes => m_scenes;

    public NotificationsController notifications => m_notifications;
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------
    
    public void OnNotification(Notification notification)
    {
        switch (notification.type) {
            case Notification.Type.LEVEL_CLEARED:
                isPaused = true;
                m_sounds.PlayLevelCleared();
                m_scenes.StartTransitionToNextSceneWithDelay(1f);
                break;
            case Notification.Type.SCENE_REVEAL_COMPLETE:
                isPaused = false;
                break;
            case Notification.Type.SCENE_HIDE_COMPLETE:
                m_scenes.LoadNextScene();
                break;
        }
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void Awake()
    {
        if (s_instance == null) {
            s_instance = this;
            m_notifications = new NotificationsController();
            m_notifications.Subscribe(Notification.Type.LEVEL_CLEARED, this);
            m_notifications.Subscribe(Notification.Type.SCENE_REVEAL_COMPLETE, this);
            m_notifications.Subscribe(Notification.Type.SCENE_HIDE_COMPLETE, this);
            
            DontDestroyOnLoad(gameObject);
        }

        if (s_instance != this) {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && m_scenes.currentSceneIsLevel) {
            m_scenes.ReloadScene();
        }
    }
}
