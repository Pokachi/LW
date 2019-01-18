using System.Collections.Generic;
using UnityEngine;

public abstract class TileLayer : ScriptableObject {
    [SerializeField] protected List<Sprite> sprite;
    public float layerOrder;

    public virtual Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        return sprite[0];
    }

    public virtual int GetMovementCost() {
        return 1;
    }
    public virtual int GetDefenseBonus() {
        return 1;
    }
    public virtual bool IsPlaceable(Tile tile) {
        return true;
    }
}
