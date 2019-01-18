public class Reef : TerrainModifier {

    public override TerrainModifierEnum ToEnum() {
        return TerrainModifierEnum.Reef;
    }

    //Reef can only be placed on Water
    public override bool IsPlaceable(Tile tile) {
        if (tile.terrain.ToEnum() == TerrainBaseEnum.Water) {
            return true;
        }
        return false;
    }
}
