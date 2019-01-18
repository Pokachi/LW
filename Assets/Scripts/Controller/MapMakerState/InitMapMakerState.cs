using System.Collections;
using UnityEngine;

public class InitMapMakerState : MapMakerState {
    public override void Enter() {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init() {
        Point p;
        if (levelData != null && levelData.tileData.Count != 0) {
            board.Load(levelData);
            if (levelData.tileData.Count != levelData.boardHeight * levelData.boardWidth) {
                Debug.Log("Invalid level data file");
                yield break;
            }
            p = new Point((int)levelData.tileData[0].position.x, (int)levelData.tileData[0].position.y);
        } else {
            board.width = owner.width;
            board.height = owner.height;
            board.Reset(owner.playerCount);
            p = new Point(0, 0);
        }
        SelectTile(p);
        yield return null;
        owner.ChangeState<DrawingMapState>();
    }
}