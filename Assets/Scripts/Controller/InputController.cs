using UnityEngine;

public class InputController : MonoBehaviour {

    Repeater horizontal = new Repeater("Horizontal");
    Repeater veritical = new Repeater("Vertical");
    Repeater zoom = new Repeater("Zoom");

    //Update is called once per frame
    void Update() {

        //Check if directional key is pressed and set out an notification
        int x = horizontal.Update();
        int y = veritical.Update();

        if (x != 0 || y != 0) {
            NotificationCenter.instance.PostNotification(new DirectionalMovementNotification(new Point(x, y)));
        }

        int zoomValue = zoom.Update();
        if (zoomValue != 0) {
            NotificationCenter.instance.PostNotification(new ZoomNotification(zoomValue));
        }


        //Check if other keys are pressed and set out an notification
        if (Input.GetButton("Confirm")) {
            NotificationCenter.instance.PostNotification(new ConfirmNotification());
        }
        if (Input.GetButton("Cancel")) {
            NotificationCenter.instance.PostNotification(new CancelNotification());
        }

        if (Input.GetButtonUp("Auxiliary Left")) {
            NotificationCenter.instance.PostNotification(new AuxiliaryLeftNotification());
        }
        if (Input.GetButtonUp("Auxiliary Right")) {
            NotificationCenter.instance.PostNotification(new AuxiliaryRightNotificiation());
        }

        if (Input.GetButtonUp("Save")) {
            NotificationCenter.instance.PostNotification(new SaveNotification());
        }
        if (Input.GetButtonUp("Load")) {
            NotificationCenter.instance.PostNotification(new LoadNotification());
        }
    }


    //Class to help with determining if the key is held down.
    class Repeater {
        const float threshold = 0.5f;
        const float rate = 0.1f;
        float _next;
        bool _hold;
        string _axis;

        public Repeater(string axisName) {
            _axis = axisName;
        }

        public int Update() {
            int retValue = 0;
            int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
            if (value != 0) {
                if (Time.time > _next) {
                    retValue = value;
                    _next = Time.time + (_hold ? rate : threshold);
                    _hold = true;
                }
            } else {
                _hold = false;
                _next = 0;
            }
            return retValue;
        }
    }
}
