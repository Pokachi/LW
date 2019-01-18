using UnityEngine;

public class CameraRig : MonoBehaviour {
    private static readonly int cameraZPosition = -10;

    public float speed = 3f;
    public Transform follow;
    Transform _transform;

    void Awake() {
        _transform = transform;
    }

    private void Start() {
        NotificationCenter.instance.AddObserver(ProcessZoomInput, NotificationName.Zoom);
    }

    public void ProcessZoomInput(object sender, object payload) {
        Camera camera = gameObject.GetComponent<Camera>();
        float zoomValue = (float)payload;
        Debug.Log(zoomValue);
        if (camera.orthographicSize < 8 && zoomValue < 0) {
            camera.orthographicSize -= zoomValue;
        } else if (camera.orthographicSize > 2 && zoomValue > 0) {
            camera.orthographicSize -= zoomValue;
        }
    }

    void Update() {
        if (follow) {
            Vector3 pos = gameObject.GetComponent<Camera>().WorldToViewportPoint(follow.position);

            if (pos.x < 0.1) {
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_transform.position.x - 3, _transform.position.y, cameraZPosition), speed * Time.deltaTime);
            } else if (0.9 < pos.x) {
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_transform.position.x + 3, _transform.position.y, cameraZPosition), speed * Time.deltaTime);
            }
            if (pos.y < 0.1) {
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_transform.position.x, _transform.position.y - 3, cameraZPosition), speed * Time.deltaTime);
            } else if (0.9 < pos.y) {
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(_transform.position.x, _transform.position.y + 3, cameraZPosition), speed * Time.deltaTime);
            }
        }
    }
}