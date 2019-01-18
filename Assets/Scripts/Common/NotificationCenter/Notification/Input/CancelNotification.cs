using System;

/// <summary>
/// Notification for when cancel button is pressed
/// </summary>
public class CancelNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.Cancel;
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
    public CancelNotification() {
    }
    #endregion
}
