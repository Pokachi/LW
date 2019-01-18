public class Port : Structure {
    public override StructureEnum ToEnum() {
        return StructureEnum.Port;
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
