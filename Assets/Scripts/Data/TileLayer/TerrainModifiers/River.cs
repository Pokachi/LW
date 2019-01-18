using UnityEngine;

public class River : TerrainModifier {
    
    public override TerrainModifierEnum ToEnum() {
        return TerrainModifierEnum.River;
    }

    //Calculate the orientation based on neighbouring tiles
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {

        int spritePosition = (IsTileRiverOrWater(northTile) ? 1 : 0) +
                             (IsTileRiverOrWater(southTile) ? 2 : 0) +
                             (IsTileRiverOrWater(eastTile) ? 4 : 0) +
                             (IsTileRiverOrWater(westTile) ? 8 : 0);

        return sprite[spritePosition];
    }

    //Helper method to check if Tile's TerrainModifier is River
    bool IsTileRiverOrWater(Tile tile) {
        return tile != null && ((tile.terrainModifier != null && tile.terrainModifier.ToEnum() == TerrainModifierEnum.River) || (tile.terrain.ToEnum() == TerrainBaseEnum.Water));
    }

    //River can only be placed on Grass, Dirt, Sand, Snow
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water) {
            return false;
        }
        return true;
    }
}
