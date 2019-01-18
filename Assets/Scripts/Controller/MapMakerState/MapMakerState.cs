using UnityEngine;

public abstract class MapMakerState : State {
    protected MapMakerController owner;
    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public BoardCreator board { get { return owner.board; } }
    public LevelData levelData { get { return owner.levelData; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }

    protected virtual void Awake() {
        owner = GetComponent<MapMakerController>();
    }

    protected override void AddListeners() {
        NotificationCenter.instance.AddObserver(OnMove, NotificationName.DirectionalMovement);
    }

    protected override void RemoveListeners() {
        NotificationCenter.instance.RemoveObserver(OnMove, NotificationName.DirectionalMovement);
    }

    protected virtual void OnMove(object sender, object payload) {

    }
    
    protected virtual void SelectTile(Point p) {
        if (!board.tiles.ContainsKey(p))
            return;
        pos = p;
        tileSelectionIndicator.localPosition = new Vector3(pos.x * 0.5f + pos.y * 0.5f, pos.x * -0.25f + pos.y * 0.25f, 6.0f);
    }
}