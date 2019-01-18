public class Water : TerrainBase {

    public Water() {
        this.tilePriority = 0;
    }

    public override TerrainBaseEnum ToEnum() {
        return TerrainBaseEnum.Water;
    }
}
