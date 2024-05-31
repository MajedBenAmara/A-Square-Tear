using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float _BossProjectilsDistance = 2f;
    private float[] _BossProjectilRotaionSpeed = { 2.5f, -2.5f };
    [SerializeField]
    private Transform[] _BossProjectils;


    private void Update()
    {
        // make the boss projectils rotate around him 
        for (int i = 0; i < _BossProjectils.Length; i++)
        {
            _BossProjectils[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * _BossProjectilRotaionSpeed[i]), Mathf.Sin(Time.time * _BossProjectilRotaionSpeed[i]), 0) * _BossProjectilsDistance;
        }
    }
}
