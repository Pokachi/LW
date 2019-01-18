using System.Collections.Generic;
using UnityEngine;

public class StructureMapper : ScriptableObject {
    [SerializeField] Archery archery;
    [SerializeField] Barrack barrack;
    [SerializeField] Blacksmith blacksmith;
    [SerializeField] Bridge bridge;
    [SerializeField] DrawBridge drawBridge;
    [SerializeField] Gate gate;
    [SerializeField] House house;
    [SerializeField] Keep keep;
    [SerializeField] Port port;
    [SerializeField] SiegeWorkshop siegeWorkshop;
    [SerializeField] Stable stable;
    [SerializeField] Tower tower;
    [SerializeField] TownHall townHall;
    [SerializeField] Wall wall;
    [SerializeField] Fence fence;

    Dictionary<StructureEnum, Structure> map = new Dictionary<StructureEnum, Structure>();

    public Structure LookUp(StructureEnum structureEnum) {
        if (map.Count == 0) {
            InitializeMap();
        }
        return map[structureEnum];
    }

    private void InitializeMap() {
        map.Add(StructureEnum.Empty, null);
        map.Add(StructureEnum.Destroy, null);
        map.Add(StructureEnum.Archery, archery);
        map.Add(StructureEnum.Barrack, barrack);
        map.Add(StructureEnum.Blacksmith, blacksmith);
        map.Add(StructureEnum.Bridge, bridge);
        map.Add(StructureEnum.DrawBridge, drawBridge);
        map.Add(StructureEnum.Gate, gate);
        map.Add(StructureEnum.House, house);
        map.Add(StructureEnum.Keep, keep);
        map.Add(StructureEnum.Port, port);
        map.Add(StructureEnum.SiegeWorkshop, siegeWorkshop);
        map.Add(StructureEnum.Stable, stable);
        map.Add(StructureEnum.Tower, tower);
        map.Add(StructureEnum.TownHall, townHall);
        map.Add(StructureEnum.Wall, wall);
        map.Add(StructureEnum.Fence, fence);
    }
}
