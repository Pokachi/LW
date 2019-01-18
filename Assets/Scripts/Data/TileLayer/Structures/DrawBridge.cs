using UnityEngine;

public class DrawBridge : Structure {

    public override StructureEnum ToEnum() {
        return StructureEnum.DrawBridge;
    }

    //Return Player index * 2 + direction * 20 + 0 if the state is on (open), 1 otherwise (closed)
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {
        
        return sprite[(int)thisTile.tileState.ownerId * 2 + 20 * ((int)thisTile.tileState.direction) + (int)thisTile.tileState.structureState];
    }

    //Can place on anything except terrain modifier (road excluded)
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrainModifier != null && tile.terrainModifier.ToEnum() != TerrainModifierEnum.Road) {
            return false;
        }
       

        return true;
    }
}
