public class Sand : TerrainBase {

    public Sand() {
        this.tilePriority = 2;
    }

    public override TerrainBaseEnum ToEnum() {
        return TerrainBaseEnum.Sand;
    }

}
