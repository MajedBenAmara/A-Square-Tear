using UnityEngine;

public class DiscHolder : Holder
{

    public float _DiscRotaionSpeed = 200f;
    [SerializeField] private AudioPlayer _AudioPlayer;
    public Transform FullDisc;

    private void Start()
    {
        _AudioPlayer.PlayOnDiscTurn();
    }

    private void Update()
    {
        // FullDisc is the game objectt that hold all discs weapon
        FullDisc.Rotate(Vector3.forward * Time.deltaTime * _DiscRotaionSpeed, Space.Self);
    }

    public void UpgradeDiscRotationSpeed(float speed)
    {
        _DiscRotaionSpeed += speed;
    }
}
