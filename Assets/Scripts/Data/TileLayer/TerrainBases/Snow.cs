public class Snow : TerrainBase {

    public Snow() {
        this.tilePriority = 4;
    }

    public override TerrainBaseEnum ToEnum() {
        return TerrainBaseEnum.Snow;
    }
}
