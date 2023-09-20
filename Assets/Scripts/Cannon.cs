using System;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour
{
    public UnityEvent Shot;
    
    public event Action DirectionChanged;
    public event Action PowerChanged;

    [SerializeField] private Transform _gunRoot;
    [SerializeField] private Transform _gunEnd;

    [Space]
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _gunPower = 1;

    public float GunPower
    {
        get => _gunPower;
        set
        {
            _gunPower = value;
            PowerChanged?.Invoke();
        }
    }

    private void Update()
    {
        var hMove = Input.GetAxis("Horizontal");
        var vMove = Input.GetAxis("Vertical");

        if (hMove != 0 || vMove != 0)
        {
            transform.Rotate(Vector3.up, hMove * Time.deltaTime * _rotationSpeed);
            _gunRoot.Rotate(Vector3.left, vMove * Time.deltaTime * _rotationSpeed);
            
            DirectionChanged?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = BulletPool.Instance.GetNew();
        bullet.Init(_gunEnd.position, _gunEnd.rotation, _gunEnd.forward * GunPower);
        Shot?.Invoke();
    }
}