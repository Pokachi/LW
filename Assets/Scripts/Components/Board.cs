using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour {
    [SerializeField] GameObject tilePrefab;
    
    [SerializeField] TerrainBaseMapper terrainBaseMapper;
    [SerializeField] StructureMapper structureMapper;
    [SerializeField] TerrainModifierMapper terrainModifierMapper;

    int width;
    int height;
    List<Player> players = new List<Player>();

    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    public void Load(LevelData levelData) {
        //Do nothing if we have nothing to load
        if (levelData == null) {
            Debug.Log("No level data to load,");
            return;
        }

        //Set the board size and player count first and then reset so that all tiles are created
        height = levelData.boardHeight;
        width = levelData.boardWidth;
        
        Reset(levelData.playerCount);

        foreach (TileData data in levelData.tileData) {
            Tile tile = tiles[data.position];
            tile.Load(data.position, terrainBaseMapper.LookUp(data.terrainBase), terrainModifierMapper.LookUp(data.terrainModifier), structureMapper.LookUp(data.structure), data.tileState);
        }
    }

    public void Reset(int playerCount) {
        //Create the players
        players.Clear();

        //Note that id = 0 is the "netural" player, so we always create playerCount + 1 players
        for (int i = 0; i <= playerCount; i++) {
            Player p = ScriptableObject.CreateInstance<Player>();
            p.id = i;
            p.color = (PlayerColor)i;
            players.Add(p);
        }

        //Destroy all child first
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);

        //Clear the tiles mapping next
        tiles.Clear();

        //Then draw the new board
        Rect map = new Rect(0.0f, 0.0f, (float)width, (float)height);
        DrawRect(map, TerrainBaseEnum.Grass, TerrainModifierEnum.Empty, StructureEnum.Empty, new TileState(0, TileState.State.Off, Direction.West, false));

        //Set neighboring tiles for each tile after we have spawned in all tiles
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile t = tiles[new Point(x, y)];
                t.RegisterNeighbors(x >= 0 && x < width - 1 ? tiles[new Point(x + 1, y)] : null,
                                    x >= 1 && x < width ? tiles[new Point(x - 1, y)] : null,
                                    y >= 0 && y < height - 1 ? tiles[new Point(x, y + 1)] : null,
                                    y >= 1 && y < height ? tiles[new Point(x, y - 1)] : null);
            }
        }
    }

    void DrawRect(Rect r, TerrainBaseEnum terrainBaseEnum, TerrainModifierEnum terrainModifierEnum, StructureEnum structureEnum, TileState tileState) {
        for (int y = (int)r.yMin; y < (int)r.yMax; y++) {
            for (int x = (int)r.xMin; x < (int)r.xMax; x++) {
                Point p = new Point(x, y);
                DrawSingle(p, terrainBaseEnum, terrainModifierEnum, structureEnum, tileState);
            }
        }
    }

    Tile GetOrCreate(Point p) {
        if (tiles.ContainsKey(p))
            return tiles[p];

        GameObject tileGO = Instantiate(tilePrefab) as GameObject;
        tileGO.transform.parent = transform;
        Tile t = tileGO.GetComponent<Tile>();
        t.SetPosition(p, false);

        tiles.Add(p, t);

        return t;
    }

    void DrawSingle(Point p, TerrainBaseEnum terrainBaseEnum, TerrainModifierEnum terrainModifierEnum, StructureEnum structureEnum, TileState tileState) {
        Tile t = GetOrCreate(p);

        t.SetTileState(tileState);

        if (terrainBaseEnum != TerrainBaseEnum.Empty) {
            TerrainBase terrainBase = terrainBaseMapper.LookUp(terrainBaseEnum);
            t.SetTerrain(terrainBase);
        }

        if (terrainModifierEnum != TerrainModifierEnum.Empty) {
            TerrainModifier terrainModifier = terrainModifierMapper.LookUp(terrainModifierEnum);
            t.SetTerrainModifier(terrainModifier);
        }

        if (structureEnum != StructureEnum.Empty) {
            Structure structure = structureMapper.LookUp(structureEnum);
            t.SetStructure(structure);
        }
    }
}