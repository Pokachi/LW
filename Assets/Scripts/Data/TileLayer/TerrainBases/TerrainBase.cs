public abstract class TerrainBase : TileLayer {
    protected int tilePriority;
    public TerrainBase() {
        this.layerOrder = 0.0f;
    }

    public abstract TerrainBaseEnum ToEnum();
}
