using UnityEngine;

public class Mountain : TerrainModifier {
    
    public override TerrainModifierEnum ToEnum() {
        return TerrainModifierEnum.Mountain;
    }

    //Return Mountain_Snow if the terrain is snow, otherwise return Mountain_GrassDirt
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        if (thisTile.terrain.ToEnum() == TerrainBaseEnum.Snow) {
            return sprite[1];
        } else {
            return sprite[0];
        }
    }

    //Mountain can only be placed on Grass, Dirt, and Snow
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water || tile.terrain.ToEnum() == TerrainBaseEnum.Sand) {
            return false;
        }
        return true;
    }
}
