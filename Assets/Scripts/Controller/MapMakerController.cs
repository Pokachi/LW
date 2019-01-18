using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakerController : StateMachine {
    //Size of the Map
    public int width = 21;
    public int height = 21;

    public int playerCount = 9;

    public CameraRig cameraRig;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public BoardCreator board;

    void Start() {
        ChangeState<InitMapMakerState>();
    }
}
