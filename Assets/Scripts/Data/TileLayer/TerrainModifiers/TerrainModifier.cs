public abstract class TerrainModifier : TileLayer {
    public TerrainModifier() {
        this.layerOrder = 2.0f;
    }

    public abstract TerrainModifierEnum ToEnum();
}