using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _PlayerPosition;
    [SerializeField] private float _PosXConstrain = 77f, _PosYConstrain = 33f, _NegXConstrain = -77f, _NegYConstrain = -27f;
    [SerializeField] private float _Speed;

    void Update()
    {
        // if the player is not dead
        if (_PlayerPosition != null)
        {
            // we limite or clamp the player coordinate value and stor them, so if they are inside the limites that we defined them we reurn them as they are
            // if not we return the limite or wonstrain value we defined
            float clampedX = Mathf.Clamp(_PlayerPosition.position.x, _NegXConstrain, _PosXConstrain);
            float clampedY = Mathf.Clamp(_PlayerPosition.position.y, _NegYConstrain, _PosYConstrain);
            // we use the lerp func here to smooth the transition of the camera
            transform.position = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, -10), _Speed);
        }

    }



}
