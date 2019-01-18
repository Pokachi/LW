using UnityEngine;

/// <summary>
/// Class representing a tile
/// </summary> 
public class Tile : MonoBehaviour {
    public TerrainBase terrain;
    public TerrainModifier terrainModifier;
    public Structure structure;
    public Point position;

    public Tile northTile;
    public Tile southTile;
    public Tile eastTile;
    public Tile westTile;

    public TileState tileState;

    Transform TerrainTransform {
        get {
            if (_terrainTransform == null) {
                GameObject instance = new GameObject("Terrain");
                instance.AddComponent<SpriteRenderer>();
                _terrainTransform = instance.transform;
                _terrainTransform.SetParent(transform);
            }
            return _terrainTransform;
        }
    }
    Transform _terrainTransform;

    Transform TerrainModifierTransform {
        get {
            if (_terrainModifierTransform == null) {
                GameObject instance = new GameObject("TerrainModifier");
                instance.AddComponent<SpriteRenderer>();
                _terrainModifierTransform = instance.transform;
                _terrainModifierTransform.SetParent(transform);
            }
            return _terrainModifierTransform;
        }
    }
    Transform _terrainModifierTransform;

    Transform StructureTransform {
        get {
            if (_structureTransform == null) {
                GameObject instance = new GameObject("Scruture");
                instance.AddComponent<SpriteRenderer>();
                _structureTransform = instance.transform;
                _structureTransform.SetParent(transform);
            }
            return _structureTransform;
        }
    }
    Transform _structureTransform;

    /// <summary>
    /// Render the tile with the current setting
    /// </summary>
    void Match() {
        // Terrain should never be null
        SpriteRenderer terrainSR = TerrainTransform.gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        TerrainTransform.localPosition = new Vector3(0.0f, 0.0f, terrain.layerOrder);
        terrainSR.sprite = terrain.GetSprite(northTile, southTile, eastTile, westTile, this);


        SpriteRenderer terrainModifierSR = TerrainModifierTransform.gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        if (terrainModifier != null) {
            TerrainModifierTransform.localPosition = new Vector3(0.0f, 0.0f, terrainModifier.layerOrder);
            terrainModifierSR.sprite = terrainModifier.GetSprite(northTile, southTile, eastTile, westTile, this);
        } else {
            terrainModifierSR.sprite = null;
        }

        SpriteRenderer structureSR = StructureTransform.gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        if (structure != null) {
            StructureTransform.localPosition = new Vector3(0.0f, 0.0f, structure.layerOrder);
            structureSR.sprite = structure.GetSprite(northTile, southTile, eastTile, westTile, this);
        } else {
            structureSR.sprite = null;
        }
    }

    public void SetPosition(Point position, bool isPreview) {
        this.position = position;
        transform.position = new Vector3(position.x * 0.5f + position.y * 0.5f, position.x * -0.25f + position.y * 0.25f, isPreview ? 3.0f : 0.0f);
    }

    /// <summary>
    /// Set the terrain of the the tile, Then if the tile changed, sent a notification so nebighouring tiles can act accordingly
    /// </summary>
    /// <param name="terrain"> Base such as grass, water, or dirt</param>
    public void SetTerrain(TerrainBase terrain) {
        bool tileChanged = false;

        if (terrain.IsPlaceable(this)) {
            if (this.terrain != terrain) {
                this.terrain = terrain;
                tileChanged = true;
            }
        }

        //Since we changed the terrain, we need to check if the terrainModifier is placable again
        if (this.terrainModifier != null && tileChanged == true && !this.terrainModifier.IsPlaceable(this)) {
            this.terrainModifier = null;
        }


        //Since we changed the terrain, we need to check if the structure is placable again
        if (this.structure != null && tileChanged == true && !this.structure.IsPlaceable(this)) {
            this.structure = null;
        }

        //Only update the graphics if it has changed.
        if (tileChanged) {
            Match();
            NotificationCenter.instance.PostNotification(new TerrainChangedNotification(this));
        }
    }

    /// <summary>
    /// Set the terrain modifier of the the tile, Then if the tile changed, sent a notification so nebighouring tiles can act accordingly
    /// </summary>
    /// <param name="terrainModifier"> Terrain modifier such as forest and rivers </param>
    public void SetTerrainModifier(TerrainModifier terrainModifier) {
        bool tileChanged = false;

        if (terrainModifier == null || (terrainModifier != null && terrainModifier.IsPlaceable(this))) {
            if (this.terrainModifier != terrainModifier) {
                this.terrainModifier = terrainModifier;
                tileChanged = true;
            }
        }

        //Since we changed the terrainModifier, we need to check if the structure is placable again
        if (this.structure != null && tileChanged == true && !this.structure.IsPlaceable(this)) {
            this.structure = null;
        }

        //Only update the graphics if it has changed.
        if (tileChanged) {
            Match();
            NotificationCenter.instance.PostNotification(new TerrainChangedNotification(this));
        }
    }

    /// <summary>
    /// Set the structure of the the tile, Then if the tile changed, sent a notification so nebighouring tiles can act accordingly
    /// </summary>
    /// <param name="structure"> Top most building such as keep or barrack </param>
    public void SetStructure(Structure structure) {
        bool tileChanged = false;

        if (structure == null || (structure != null && structure.IsPlaceable(this))) {
            if (this.structure != structure) {
                this.structure = structure;
                tileChanged = true;
            }
        }

        //Only update the graphics if it has changed.
        if (tileChanged) {
            Match();
            NotificationCenter.instance.PostNotification(new TerrainChangedNotification(this));
        }
    }

    /// <summary>
    /// Set the tile state of the the tile, Then if the tile changed, sent a notification so nebighouring tiles can act accordingly
    /// </summary>
    /// <param name="structure"> Top most building such as keep or barrack </param>
    public void SetTileState(TileState tileState) {
        bool tileChanged = false;

        if (this.tileState != tileState) {
            this.tileState = tileState;
            tileChanged = true;
        }
        
        //Only update the graphics if it has changed.
        if (tileChanged) {
            Match();
            NotificationCenter.instance.PostNotification(new TerrainChangedNotification(this));
        }
    }

    /// <summary>
    /// Set the terrain, modifier, and the structure of the the tile, Then if the tile changed, sent a notification so
    /// nebighouring tiles can act accordingly
    /// </summary>
    /// <param name="terrain"> Base such as grass, water, or dirt</param>
    /// <param name="terrainModifier"> Terrain modifier such as forest and rivers </param>
    /// <param name="structure"> Top most building such as keep or barrack </param>
    /// <param name="tileState"> Current state of the structure </param>
    public void SetTileData(TerrainBase terrain, TerrainModifier terrainModifier, Structure structure, TileState tileState) {
        bool tileChanged = false;
        
        if(this.tileState != tileState) {
            this.tileState = tileState;
            tileChanged = true;
        }

        if (terrain != null && terrain.IsPlaceable(this)) {
            if (this.terrain != terrain) {
                this.terrain = terrain;
                tileChanged = true;
            }
        }

        //Since we changed the terrain, we need to check if the terrainModifier is placable again
        if (this.terrainModifier != null && tileChanged == true && !this.terrainModifier.IsPlaceable(this)) {
            this.terrainModifier = null;
            tileChanged = true;
        } 

        if (terrainModifier == null  || (terrainModifier != null && terrainModifier.IsPlaceable(this))) {
            if (this.terrainModifier != terrainModifier) {
                this.terrainModifier = terrainModifier;
                tileChanged = true;
            }
        }

        //Since we changed the terrain, we need to check if the structure is placable again
        if (this.structure != null && tileChanged == true && !this.structure.IsPlaceable(this)) {
            this.structure = null;
            tileChanged = true;
        }

        if (structure == null || (structure != null && structure.IsPlaceable(this))) {
            if (this.structure != structure) {
                this.structure = structure;
                tileChanged = true;
            }
        }

        //Only update the graphics if it has changed.
        if (tileChanged) {
            Match();
            NotificationCenter.instance.PostNotification(new TerrainChangedNotification(this));
        }
    }

    /// <summary>
    /// Register the neighbors of this tile in all 4 directions, and listen to their changes
    /// </summary>
    /// <param name="east"> Tile on the east </param>
    /// <param name="west"> Tile on the west </param>
    /// <param name="north"> Tile on the north </param>
    /// <param name="south"> Tile on the south </param>
    public void RegisterNeighbors(Tile east, Tile west, Tile north, Tile south) {
        if (north != null) {
            NotificationCenter.instance.AddObserver(HandleNeighbouringTerrainChanged, NotificationName.TerrainChanged, north);
            this.northTile = north;
        }
        if (south != null) {
            NotificationCenter.instance.AddObserver(HandleNeighbouringTerrainChanged, NotificationName.TerrainChanged, south);
            this.southTile = south;
        }
        if (west != null) {
            NotificationCenter.instance.AddObserver(HandleNeighbouringTerrainChanged, NotificationName.TerrainChanged, west);
            this.westTile = west;
        }
        if (east != null) {
            NotificationCenter.instance.AddObserver(HandleNeighbouringTerrainChanged, NotificationName.TerrainChanged, east);
            this.eastTile = east;
        }
    }

    /// <summary>
    /// If the neighbor tile changed, we may need to update the graphics
    /// </summary>
    /// <param name="sender"> Tile that changed </param>
    /// <param name="payload"> N/A </param>
    public void HandleNeighbouringTerrainChanged(object sender, object payload) {
        Match();
    }

    /// <summary>
    /// Load the tile data
    /// </summary>
    /// <param name="position"> Position of the tile </param>
    /// <param name="terrain"> Base such as grass, water, or dirt</param>
    /// <param name="terrainModifier"> Terrain modifier such as forest and rivers </param>
    /// <param name="structure"> Top most building such as keep or barrack </param>
    /// <param name="owner"> Owner of this tile </param>
    public void Load (Point position, TerrainBase terrain, TerrainModifier terrainModifier, Structure structure, TileState structureState) {
        this.position = position;
        this.terrain = terrain;
        this.terrainModifier = terrainModifier;
        this.structure = structure;
        this.tileState = structureState;

        transform.position = new Vector3(position.x * 0.5f + position.y * 0.5f, position.x * -0.25f + position.y * 0.25f, 0.0f);
        Match();
        NotificationCenter.instance.PostNotification(new TerrainChangedNotification(this));
    }
}
