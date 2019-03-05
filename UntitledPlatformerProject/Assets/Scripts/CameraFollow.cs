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

    [SerializeField]
    float smoothCameraTiming;

    Vector3 oldPosition;


    private void Awake() {

        transform.position = cameraFocus.position + cameraOffset;

    }

    void Start() {

        FollowPlayer();
        player.playerMoved += FollowPlayer;
    }

    public void FollowPlayer() {

        if (!Utilities.VectorEquals(cameraFocus.position, oldPosition)) {

            Vector3 cameraPosition = cameraFocus.position + cameraOffset;

            Vector3 smoothMove = Vector3.SmoothDamp(transform.position, cameraPosition, ref moveVelocity, smoothCameraTiming);

            transform.position = smoothMove;

            oldPosition = cameraFocus.position;
        }
    }
}
