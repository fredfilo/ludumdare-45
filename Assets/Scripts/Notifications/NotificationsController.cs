using System.Collections.Generic;
using UnityEngine;

public class NotificationsController
{
    // PROPERTIES
    // -------------------------------------------------------------------------
    
    private Dictionary<Notification.Type, List<INotificationListener>> m_types = new Dictionary<Notification.Type, List<INotificationListener>>();
    
    // PUBLIC METHODS
    // -------------------------------------------------------------------------

    public void Subscribe(Notification.Type type, INotificationListener listener)
    {
        if (!m_types.ContainsKey(type)) {
            m_types[type] = new List<INotificationListener>();
        }

        if (!m_types[type].Contains(listener)) {
            m_types[type].Add(listener);
        }
    }
    
    public void Unsubscribe(Notification.Type type, INotificationListener listener)
    {
        if (!m_types.ContainsKey(type)) {
            return;
        }
        
        if (!m_types[type].Contains(listener)) {
            return;
        }

        m_types[type].Remove(listener);
    }

    public void Notify(Notification notification)
    {
        Debug.Log("Notifying " + notification.type);
        if (!m_types.ContainsKey(notification.type)) {
            return;
        }

        foreach (INotificationListener listener in m_types[notification.type]) {
            listener.OnNotification(notification);
        }
    }
}
