using System;

/// <summary>
/// Notification for when auxiliary left button is pressed
/// </summary>
public class AuxiliaryLeftNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.AuxiliaryLeft;
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
    public AuxiliaryLeftNotification() {
    }
    #endregion
}
