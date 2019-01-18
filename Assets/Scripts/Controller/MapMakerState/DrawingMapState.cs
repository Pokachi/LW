using System;
using UnityEngine;

public class DrawingMapState : MapMakerState {

    TerrainBaseEnum selectedTerrain = TerrainBaseEnum.Grass;
    TerrainModifierEnum selectedTerrainModifier = TerrainModifierEnum.Empty;
    StructureEnum selectedStructure = StructureEnum.Empty;
    TileState tileState;
    TileLayerEnum currentTileLayer = TileLayerEnum.TerrainBase;

    private void Start() {
        NotificationCenter.instance.AddObserver(Draw, NotificationName.Confrim);
        NotificationCenter.instance.AddObserver(ChangeTileLayer, NotificationName.AuxiliaryLeft);
        NotificationCenter.instance.AddObserver(ChangeTileLayerObject, NotificationName.AuxiliaryRight);
        NotificationCenter.instance.AddObserver(UpdatePreviewTile, NotificationName.DirectionalMovement);
        NotificationCenter.instance.AddObserver(Save, NotificationName.Save);
        NotificationCenter.instance.AddObserver(Load, NotificationName.Load);
    }

    private void Draw(object sender, object payload) {
        owner.board.Draw(owner.pos, selectedTerrain, selectedTerrainModifier, selectedStructure, tileState);
    }

    private void UpdatePreviewTile(object sender, object payload) {
        owner.board.UpdatePreviewTile(owner.pos, selectedTerrain, selectedTerrainModifier, selectedStructure, tileState);
    }

    private void Save(object sender, object payload) {
        owner.board.Save();
    }
    
    private void Load(object sender, object payload) {
        owner.board.Load(owner.levelData);
    }

    private void ChangeTileLayer(object sender, object payload) {
        currentTileLayer = (TileLayerEnum)(((int)currentTileLayer + 1) % Enum.GetNames(typeof(TileLayerEnum)).Length);

        if (currentTileLayer == TileLayerEnum.TerrainBase) {
            selectedTerrain = TerrainBaseEnum.Grass;
            selectedStructure = StructureEnum.Empty;
            selectedTerrainModifier = TerrainModifierEnum.Empty;
        }
        if (currentTileLayer == TileLayerEnum.TerrainModifier) {
            selectedTerrain = TerrainBaseEnum.Empty;
            selectedStructure = StructureEnum.Empty;
            selectedTerrainModifier = TerrainModifierEnum.Destroy;
        }
        if (currentTileLayer == TileLayerEnum.Structure) {
            selectedTerrain = TerrainBaseEnum.Empty;
            selectedStructure = StructureEnum.Destroy;
            selectedTerrainModifier = TerrainModifierEnum.Empty;
        }

        owner.board.UpdatePreviewTile(owner.pos, selectedTerrain, selectedTerrainModifier, selectedStructure, tileState);
    }

    private void ChangeTileLayerObject(object sender, object payload) {
        if (currentTileLayer == TileLayerEnum.TerrainBase) {
            selectedTerrain = (TerrainBaseEnum)(((int)selectedTerrain + 1) % Enum.GetNames(typeof(TerrainBaseEnum)).Length);
            if (selectedTerrain == 0) {
                selectedTerrain = (TerrainBaseEnum) 1;
            }
        } else if (currentTileLayer == TileLayerEnum.TerrainModifier) {
            selectedTerrainModifier = (TerrainModifierEnum)(((int)selectedTerrainModifier + 1) % Enum.GetNames(typeof(TerrainModifierEnum)).Length);
            if (selectedTerrainModifier == 0) {
                selectedTerrainModifier = (TerrainModifierEnum) 1;
            }
        } else if (currentTileLayer == TileLayerEnum.Structure) {
            selectedStructure = (StructureEnum)(((int)selectedStructure + 1) % Enum.GetNames(typeof(StructureEnum)).Length);
            if (selectedStructure == 0) {
                selectedStructure = (StructureEnum) 1;
            }
        }

        owner.board.UpdatePreviewTile(owner.pos, selectedTerrain, selectedTerrainModifier, selectedStructure, tileState);
    }

    protected override void OnMove(object sender, object payload) {
        Point movement = (Point)payload;
        
        SelectTile(movement + pos);
    }
}