using System;

[Serializable]
public struct TileData {
    public Point position;
    public TerrainBaseEnum terrainBase;
    public TerrainModifierEnum terrainModifier;
    public StructureEnum structure;
    public TileState tileState;

    public TileData(Point position, TerrainBaseEnum terrainBase, TerrainModifierEnum terrainModifier, StructureEnum structure, TileState structureState) {
        this.position = position;
        this.terrainBase = terrainBase;
        this.terrainModifier = terrainModifier;
        this.structure = structure;
        this.tileState = structureState;
    }
}
