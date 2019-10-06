public class Notification
{
    // STATIC
    // -------------------------------------------------------------------------

    public enum Type
    {
        LEVEL_CLEARED,
        SCENE_REVEAL_START,
        SCENE_REVEAL_COMPLETE,
        SCENE_HIDE_START,
        SCENE_HIDE_COMPLETE
    }
    
    // PROPERTIES
    // -------------------------------------------------------------------------

    protected Type m_type;
    
    // ACCESSORS
    // -------------------------------------------------------------------------

    public Type type => m_type;
}
