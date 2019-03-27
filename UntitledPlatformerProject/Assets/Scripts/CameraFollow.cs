using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    Player player;

    [SerializeField]
    Transform cameraFocus;

    [SerializeField]
    Vector3 cameraOffset;

    Vector3 moveVelocity;

    Vector2 moveVelocity2D;

    float velocity;

    [SerializeField]
    float smoothCameraTiming;

    Vector3 oldPosition;

    [SerializeField]
    float cameraPanSpeed;

    private void Awake() {

        if (cameraFocus != null) {
            transform.position = cameraFocus.position + cameraOffset;
        }
    }

    void Start() {

        if (player != null) {
            player.playerMoved += FollowPlayer;
            FollowPlayer();
        }
    }

    public void FollowPlayer() {

        if (!Utilities.VectorEquals(cameraFocus.position, oldPosition)) {

            Vector3 cameraPosition = cameraFocus.position + cameraOffset;

            Vector3 smoothMove = Vector3.SmoothDamp(transform.position, cameraPosition, ref moveVelocity, smoothCameraTiming);

            transform.position = smoothMove;

            oldPosition = cameraFocus.position;
        }
    }

    public void MoveCamera(Vector2 newPosition) {

        Vector2 desiredLocation = newPosition * cameraPanSpeed;

        Vector2 smoothPosition = Vector2.SmoothDamp(newPosition, desiredLocation, ref moveVelocity2D, smoothCameraTiming);

        Vector2 coordinates = smoothPosition * Time.deltaTime;

        transform.Translate(coordinates);
    }
}
