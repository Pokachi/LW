using UnityEngine;

public abstract class Structure : TileLayer {

    public Structure() {
        this.layerOrder = 2.0f;
    }

    //Return Sprite based on player color
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        return sprite[(int)thisTile.tileState.ownerId];
    }

    //Can only placed on land and only when there is no terrain modifier
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water) {
            return false;
        }

        if (tile.terrainModifier != null) {
            return false;
        }

        return true;
    }

    public abstract StructureEnum ToEnum();

    public virtual void onInteract() {
        return;
    }
}