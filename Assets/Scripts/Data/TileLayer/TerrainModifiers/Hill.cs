using UnityEngine;

public class Hill : TerrainModifier {

    public override TerrainModifierEnum ToEnum() {
        return TerrainModifierEnum.Hill;
    }

    //Calculate the sprite based on tile terrain type
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        TerrainBaseEnum tileTerrainBaseEnum = thisTile.terrain.ToEnum();

        switch (tileTerrainBaseEnum) {
            case TerrainBaseEnum.Dirt:
                return sprite[1];
            case TerrainBaseEnum.Sand:
                return sprite[2];
            case TerrainBaseEnum.Snow:
                return sprite[3];
            default:
                return sprite[0];
        }
    }


    //Hill can only be placed on Grass, Dirt, Sand, Snow
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water) {
            return false;
        }
        return true;
    }
}
