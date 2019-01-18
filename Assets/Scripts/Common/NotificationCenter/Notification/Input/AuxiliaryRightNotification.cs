using System;

/// <summary>
/// Notification for when auxiliary right button is pressed
/// </summary>
public class AuxiliaryRightNotificiation : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.AuxiliaryRight;
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
    public AuxiliaryRightNotificiation() {
    }
    #endregion
}
