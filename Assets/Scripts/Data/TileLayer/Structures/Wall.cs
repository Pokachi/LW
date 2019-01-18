using UnityEngine;

public class Wall : Structure {
    public override StructureEnum ToEnum() {
        return StructureEnum.Wall;
    }

    //Calculate the orientation based on neighbouring tiles Direction only matters if there's a single tile, otherwise let auto tiling take care of it
    public override Sprite GetSprite(Tile northTile, Tile southTile, Tile eastTile, Tile westTile, Tile thisTile) {

        int spritePosition = (isTileObsticle(northTile) ? 1 : 0) +
                             (isTileObsticle(southTile) ? 2 : 0) +
                             (isTileObsticle(eastTile) ? 4 : 0) +
                             (isTileObsticle(westTile) ? 8 : 0);

        return sprite[spritePosition == 0 && (thisTile.tileState.direction == Direction.South || thisTile.tileState.direction == Direction.North) ? 16 : spritePosition];
    }

    //Helper method to check if the Tile's Structure is wall, Fence, Gate, or DrawBridge
    bool isTileObsticle(Tile tile) {
        return tile != null && (tile.structure != null && (tile.structure.ToEnum() == StructureEnum.Fence || tile.structure.ToEnum() == StructureEnum.Wall || tile.structure.ToEnum() == StructureEnum.Gate || tile.structure.ToEnum() == StructureEnum.DrawBridge));
    }
}
