using System;

/// <summary>
/// Notification for when load button is pressed
/// </summary>
public class LoadNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.Load;
        }
    }

    public override Object Sender {
        get {
            return null;
        }
    }

    public override Object Payload {
        get {
            return null;
        }
    }
    #endregion

    #region Constructor
    public LoadNotification() {
    }
    #endregion
}
