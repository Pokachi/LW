using System;

/// <summary>
/// Notification for when wasd is pressed
/// </summary>
public class DirectionalMovementNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.DirectionalMovement;
        }
    }

    public override Object Sender {
        get {
            return null;
        }
    }

    public override Object Payload {
        get {
            return (Object)movement;
        }
    }
    #endregion

    #region Private Fields
    private readonly Point movement;
    #endregion

    #region Constructor
    public DirectionalMovementNotification(Point movement) {
        this.movement = movement;
    }
    #endregion
}
