using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The handler delegate used to process a notification.
///     Argument 1 is the sender of the notification
///     Argument 2 is the notification payload
/// </summary>
using Handler = System.Action<object, object>;

public class NotificationCenter {

    #region Private Fields
    // Dictionary that maps a notification to a mapping of sender and the listeners listening to that sender
    private Dictionary<NotificationName, Dictionary<object, List<Handler>>> notificationTable = new Dictionary<NotificationName, Dictionary<object, List<Handler>>>();
    // Set of handlers that are currently being processed.
    private HashSet<Handler> inProcessHandlers = new HashSet<Handler>();
    #endregion
    
    #region Singleton
    // Creates a singleton. I.e. there should only be a single NotificationCenter in the project
    public readonly static NotificationCenter instance = new NotificationCenter();
    // Make the constructor private so no other instance can be instantiated
    private NotificationCenter() { }
    #endregion

    #region Methods
    public void AddObserver(Handler handler, NotificationName notification) {
        AddObserver(handler, notification, null);
    }

    public void AddObserver(Handler handler, NotificationName notification, object sender) {
        //No reason to add a null handler to the notification table. It's more likely an error has occured
        if (handler == null) {
            Debug.LogError("Attempt to add null event handler for the notification " + notification);
            return;
        }
        
        if (!notificationTable.ContainsKey(notification)) {
            notificationTable.Add(notification, new Dictionary<object, List<Handler>>());
        }

        Dictionary<object, List<Handler>> senderTable = notificationTable[notification];

        //If no sender is specified, we use the notification center as sender. This works because Noticiation Center itself should
        //never sent notifications, thus we can use it as a placeholder for the "null" sender key instead of creating a new key for "null"
        object key = sender ?? (this);
        
        if (!senderTable.ContainsKey(key)) {
            senderTable.Add(key, new List<Handler>());
        }

        List<Handler> handlers = senderTable[key];
        
        if (!handlers.Contains(handler)) {
            handlers.Add(handler);
        }
    }

    public void RemoveObserver(Handler handler, NotificationName notification) {
        RemoveObserver(handler, notification, null);
    }
    public void RemoveObserver(Handler handler, NotificationName notification, object sender) {
        if (handler == null) {
            Debug.LogError("Attempt to remove null event handler for the notification" + notification);
            return;
        }

        if (!notificationTable.ContainsKey(notification)) {
            return;
        }

        Dictionary<object, List<Handler>> senderTable = notificationTable[notification];
        object key = sender ?? (this);

        if (!senderTable.ContainsKey(key)) {
            return;
        }

        List<Handler> handlers = senderTable[key];

        if (handlers.Contains(handler)) {
            handlers.Remove(handler);
        }
    }

    public void PostNotification(Notification notification) {
        NotificationName name = notification.Name;

        if (!notificationTable.ContainsKey(name)) {
            return;
        }

        Dictionary<object, List<Handler>> senderTable = notificationTable[name];

        object sender = notification.Sender;

        if (sender != null && senderTable.ContainsKey(sender)) {
            List<Handler> handlers = senderTable[sender];
            inProcessHandlers = new HashSet<Handler>(handlers);
            foreach (Handler handler in inProcessHandlers) {
                handler(sender, notification.Payload);
            }
            inProcessHandlers.Clear();
        }

        if (senderTable.ContainsKey(this)) {
            List<Handler> handlers = senderTable[this];
            inProcessHandlers = new HashSet<Handler>(handlers);
            foreach (Handler handler in inProcessHandlers) {
                handler(this, notification.Payload);
            }
            inProcessHandlers.Clear();
        }
    }
    #endregion
}
