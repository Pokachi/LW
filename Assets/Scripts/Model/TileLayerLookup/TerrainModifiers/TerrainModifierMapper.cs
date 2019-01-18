using System.Collections.Generic;
using UnityEngine;

public class TerrainModifierMapper : ScriptableObject {
    [SerializeField] Road road;
    [SerializeField] Forest forest;
    [SerializeField] Reef reef;
    [SerializeField] Hill hill;
    [SerializeField] Mountain mountain;
    [SerializeField] River river;

    Dictionary<TerrainModifierEnum, TerrainModifier> map = new Dictionary<TerrainModifierEnum, TerrainModifier>();

    public TerrainModifier LookUp(TerrainModifierEnum terrainModifierEnum) {
        if (map.Count == 0) {
            InitializeMap();
        }
        return map[terrainModifierEnum];
    }

    private void InitializeMap() {
        map.Add(TerrainModifierEnum.Empty, null);
        map.Add(TerrainModifierEnum.Destroy, null);
        map.Add(TerrainModifierEnum.Forest, forest);
        map.Add(TerrainModifierEnum.Mountain, mountain);
        map.Add(TerrainModifierEnum.Hill, hill);
        map.Add(TerrainModifierEnum.Reef, reef);
        map.Add(TerrainModifierEnum.River, river);
        map.Add(TerrainModifierEnum.Road, road);
    }
}
