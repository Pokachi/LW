using UnityEngine;

public class Road : TerrainModifier {

    //Road should be beneath other buildings/modifiers
    public Road() : base() {
        this.layerOrder = 1.0f;
    }

    public override TerrainModifierEnum ToEnum() {
        return TerrainModifierEnum.Road;
    }

    //Calculate the orientation based on neighbouring tiles
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {

        int spritePosition = (IsTileRoadOrStructure(northTile) ? 1 : 0) +
                             (IsTileRoadOrStructure(southTile) ? 2 : 0) +
                             (IsTileRoadOrStructure(eastTile) ? 4 : 0) +
                             (IsTileRoadOrStructure(westTile) ? 8 : 0);

        return sprite[spritePosition];
    }

    //Helper method to check if the Tile's TerrainModifier is road or structure (obsticles like walls or fence excluded)
    bool IsTileRoadOrStructure(Tile tile) {
        return tile != null && ((tile.terrainModifier != null && tile.terrainModifier.ToEnum() == TerrainModifierEnum.Road) || 
            (tile.structure != null && tile.structure.ToEnum() != StructureEnum.Wall && tile.structure.ToEnum() != StructureEnum.Fence && tile.structure.ToEnum() != StructureEnum.Gate));
    }

    //Road can only be placed on Grass, Dirt, Sand, Snow
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water) {
            return false;
        }
        return true;
    }
}
