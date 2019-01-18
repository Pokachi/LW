public class Grass : TerrainBase {

    public Grass() {
        this.tilePriority = 3;
    }

    public override TerrainBaseEnum ToEnum() {
        return TerrainBaseEnum.Grass;
    }
}
