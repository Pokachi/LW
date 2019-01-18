public class Dirt : TerrainBase {

    public Dirt() {
        this.tilePriority = 1;
    }

    public override TerrainBaseEnum ToEnum() {
        return TerrainBaseEnum.Dirt;
    }
}
