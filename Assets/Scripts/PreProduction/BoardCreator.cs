using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BoardCreator : MonoBehaviour {
    
    const string folderName = "Maps";
    const string fileExtension = ".map";

    //Number of players supported on this map
    int playerCount = 9;

    //Size of the Map
    public int width = 21;
    public int height = 21;

    //Tile state data
    List<Player> players = new List<Player>();
    [SerializeField] TileState tileState;

    //Terrain to draw
    [SerializeField] TerrainBaseEnum terrainBase;
    [SerializeField] TerrainModifierEnum terrainModifier;
    [SerializeField] StructureEnum structure;

    //Prefabs
    [SerializeField] GameObject tilePrefab;

    //Look up tables translating saved data to terrain SO
    [SerializeField] TerrainBaseMapper terrainMapper;
    [SerializeField] TerrainModifierMapper terrainModifierMapper;
    [SerializeField] StructureMapper structureMapper;

    //List of Tiles in the current map
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    //Tile used for preview what will be placed on the map
    GameObject previewTile;

    string lastSavedPath;
    
    public void Save() {
        
        string folderPath = Path.Combine(Application.dataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string dataPath = Path.Combine(folderPath, name + fileExtension);
        lastSavedPath = dataPath;

        LevelData board = new LevelData();
        board.boardHeight = height;
        board.boardWidth = width;
        board.playerCount = playerCount;
        board.tileData = new List<TileData>();
        foreach (Point p in tiles.Keys) {
            Tile t = tiles[p];
            board.tileData.Add(new TileData(t.position,
                                            t.terrain.ToEnum(),
                                            t.terrainModifier == null ? TerrainModifierEnum.Empty : t.terrainModifier.ToEnum(),
                                            t.structure == null ? StructureEnum.Empty : t.structure.ToEnum(),
                                            t.tileState));
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate)) {
            binaryFormatter.Serialize(fileStream, board);
        }
        
    }


    //TODO: proper load
    public void Load(LevelData levelData) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (lastSavedPath != null) {
            using (FileStream fileStream = File.Open(lastSavedPath, FileMode.Open)) {
                LevelData data = (LevelData)binaryFormatter.Deserialize(fileStream);

                //Do nothing if we have nothing to load
                if (data == null) {
                    Debug.Log("No level data to load,");
                    return;
                }

                //Set the board size and player count first and then reset so that all tiles are created
                height = data.boardHeight;
                width = data.boardWidth;
                playerCount = data.playerCount;
                Reset(playerCount);

                foreach (TileData tileData in data.tileData) {
                    Tile tile = tiles[tileData.position];
                    tile.Load(tileData.position, terrainMapper.LookUp(tileData.terrainBase), terrainModifierMapper.LookUp(tileData.terrainModifier), structureMapper.LookUp(tileData.structure), tileData.tileState);
                }
            }
        }

    }

    void DrawRect(Rect r, TerrainBaseEnum terrainBaseEnum, TerrainModifierEnum terrainModifierEnum, StructureEnum structureEnum, TileState tileState) {
        for (int y = (int) r.yMin; y < (int) r.yMax; y++) {
            for (int x = (int) r.xMin; x < (int) r.xMax; x++) {
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

        if (terrainBaseEnum != TerrainBaseEnum.Empty) {
            TerrainBase terrainBase = terrainMapper.LookUp(terrainBaseEnum);
            t.SetTerrain(terrainBase);
        }

        t.SetTileState(tileState);

        if (terrainModifierEnum != TerrainModifierEnum.Empty) {
            TerrainModifier terrainModifier = terrainModifierMapper.LookUp(terrainModifierEnum);
            t.SetTerrainModifier(terrainModifier);
        }

        if (structureEnum != StructureEnum.Empty) {
            Structure structure = structureMapper.LookUp(structureEnum);
            t.SetStructure(structure);
        }
    }

    public void UpdatePreviewTile(Point pos, TerrainBaseEnum terrainBaseEnum, TerrainModifierEnum terrainModifierEnum, StructureEnum structureEnum, TileState tileState) {
        Tile currentTile = tiles[pos];

        Tile t = previewTile.GetComponent<Tile>();
        t.SetPosition(pos, true);
        
        TerrainBase terrainBase = terrainBaseEnum == TerrainBaseEnum.Empty ? currentTile.terrain : terrainMapper.LookUp(terrainBaseEnum);
        t.SetTerrain(terrainBase);

        t.SetTileState(tileState);

        TerrainModifier terrainModifier = terrainModifierEnum == TerrainModifierEnum.Empty ? currentTile.terrainModifier : terrainModifierMapper.LookUp(terrainModifierEnum);
        t.SetTerrainModifier(terrainModifier);

        Structure structure = structureEnum == StructureEnum.Empty ? currentTile.structure : structureMapper.LookUp(structureEnum);
        t.SetStructure(structure);


    }

    public void Draw(Point pos, TerrainBaseEnum terrainBase, TerrainModifierEnum terrainModifier, StructureEnum structure, TileState tileState) {
        DrawSingle(pos, terrainBase, terrainModifier, structure, tileState);
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

        //Create the preview tile
        previewTile = Instantiate(tilePrefab) as GameObject;
        previewTile.transform.parent = transform;
        Tile previeTileComponent = previewTile.GetComponent<Tile>();
        previeTileComponent.transform.position = new Vector3(0, 0, 3);
        previeTileComponent.SetPosition(new Point(0,0), true);
        previeTileComponent.SetTerrain(terrainMapper.LookUp(TerrainBaseEnum.Grass));

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
}
