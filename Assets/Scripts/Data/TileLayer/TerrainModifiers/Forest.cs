using UnityEngine;

public class Forest : TerrainModifier {

    public override TerrainModifierEnum ToEnum() {
        return TerrainModifierEnum.Forest;
    }

    //Calculate the sprite based on tile terrain type
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        TerrainBaseEnum tileTerrainBaseEnum = thisTile.terrain.ToEnum();

        switch (tileTerrainBaseEnum) {
            case TerrainBaseEnum.Dirt:
                return sprite[1];
            case TerrainBaseEnum.Snow:
                return sprite[2];
            default:
                return sprite[0];
        }
    }

    //Forest can only placed on Grass, Dirt, and Snow
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water || tile.terrain.ToEnum() == TerrainBaseEnum.Sand) {
            return false;
        }
        return true;
    }
}
