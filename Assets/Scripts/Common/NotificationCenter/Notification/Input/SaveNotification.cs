using System;

/// <summary>
/// Notification for when save button is pressed
/// </summary>
public class SaveNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.Save;
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
    public SaveNotification() {
    }
    #endregion
}
