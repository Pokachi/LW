using UnityEngine;

public class Bridge : Structure {

    public override StructureEnum ToEnum() {
        return StructureEnum.Bridge;
    }

    //west and east direction are west east sprite, north and south direction are north south direction
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        if (thisTile.tileState.direction == Direction.West || thisTile.tileState.direction == Direction.East) {
            return sprite[0];
        }
        return sprite[1];
    }


    //Can only be placed on water with no terrain modifier
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrainModifier != null) {
            return false;
        }

        if (tile.terrain.ToEnum() != TerrainBaseEnum.Water) {
            return false;
        }

        return true;
    }
}
