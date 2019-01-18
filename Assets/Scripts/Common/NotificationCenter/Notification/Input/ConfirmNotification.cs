using System;

/// <summary>
/// Notification for when confirm button is pressed
/// </summary>
public class ConfirmNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.Confrim;
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
    public ConfirmNotification() {
    }
    #endregion
}
