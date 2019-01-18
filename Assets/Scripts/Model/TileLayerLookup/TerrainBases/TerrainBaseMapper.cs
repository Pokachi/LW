using System.Collections.Generic;
using UnityEngine;

public class TerrainBaseMapper : ScriptableObject {
    [SerializeField] Snow snow;
    [SerializeField] Water water;
    [SerializeField] Grass grass;
    [SerializeField] Dirt dirt;
    [SerializeField] Sand sand;

    Dictionary<TerrainBaseEnum, TerrainBase> map = new Dictionary<TerrainBaseEnum, TerrainBase>();

    public TerrainBase LookUp(TerrainBaseEnum terrainEnum) {
        if (map.Count == 0) {
            InitializeMap();
        }
        return map[terrainEnum];
    }

    private void InitializeMap() {
        map.Add(TerrainBaseEnum.Empty, grass);
        map.Add(TerrainBaseEnum.Snow, snow);
        map.Add(TerrainBaseEnum.Water, water);
        map.Add(TerrainBaseEnum.Grass, grass);
        map.Add(TerrainBaseEnum.Dirt, dirt);
        map.Add(TerrainBaseEnum.Sand, sand);
    }
}
