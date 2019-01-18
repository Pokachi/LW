using System;

/// <summary>
/// Notification for when a tile changes
/// </summary>
public class TerrainChangedNotification : Notification {
    #region Properties
    public override NotificationName Name {
        get {
            return NotificationName.TerrainChanged;
        }
    }

    public override Object Sender {
        get {
            return (Object) changedTile;
        }
    }

    public override Object Payload {
        get {
            return null;
        }
    }
    #endregion

    #region Private Fields
    private readonly Tile changedTile;
    #endregion

    #region Constructor
    public TerrainChangedNotification(Tile tile) {
        changedTile = tile;
    }
    #endregion
}
