using System;

/// <summary>
/// Notification for when zoom in or zoom out button is pressed
/// </summary>
public class ZoomNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.Zoom;
        }
    }

    public override Object Sender {
        get {
            return null;
        }
    }

    public override Object Payload {
        get {
            return zoomDirection;
        }
    }
    #endregion

    #region Private Fields
    private readonly float zoomDirection;
    #endregion

    #region Constructor
    public ZoomNotification(float zoomDirection) {
        this.zoomDirection = zoomDirection;
    }
    #endregion
}
