using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshFilter _bulletMesh;
    [SerializeField] private float      _distortion = 0.1f;

    [Space]
    [SerializeField] private RBody _rBody;
    [SerializeField] private int _bouncesToExplosion = 6;

    private int _totalBouncesToExplosion;

    private readonly Vector3[] _vertices = new[]
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, -0.5f),
        new Vector3(+0.5f, +0.5f, -0.5f),
        new Vector3(+0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, +0.5f, +0.5f),
        new Vector3(+0.5f, +0.5f, +0.5f),
        new Vector3(+0.5f, -0.5f, +0.5f),
        new Vector3(-0.5f, -0.5f, +0.5f),
    };

    private readonly int[] _triangles = new int[]
    {
        0, 1, 2, 0, 2, 3,
        4, 5, 6, 4, 6, 7,
        8, 9, 10, 8, 10, 11,
        12, 13, 14, 12, 14, 15,
        16, 17, 18, 16, 18, 19,
        20, 21, 22, 20, 22, 23,
    };

    private void Awake()
    {
        _rBody.Bounced           += OnBounced;
        _totalBouncesToExplosion =  _bouncesToExplosion;
    }
    
    private void OnDestroy()
    {
        _rBody.Bounced -= OnBounced;
    }

    public void Init(Vector3 position, Quaternion rotation, Vector3 push)
    {
        transform.position  = position;
        transform.rotation  = rotation;
        _rBody.Velocity     = push;
        _bouncesToExplosion = _totalBouncesToExplosion;

        var mesh = _bulletMesh.mesh;

        var distorted = _vertices.Select(x => x + Random.insideUnitSphere * _distortion).ToArray();

        var vertices = new Vector3[]
        {
            distorted[0], distorted[1], distorted[2], distorted[3],
            distorted[3], distorted[2], distorted[5], distorted[6],
            distorted[6], distorted[5], distorted[4], distorted[7],
            distorted[7], distorted[4], distorted[1], distorted[0],
            distorted[1], distorted[4], distorted[5], distorted[2],
            distorted[0], distorted[3], distorted[6], distorted[7],
        };

        mesh.vertices  = vertices;
        mesh.triangles = _triangles;
        mesh.RecalculateNormals();
    }

    private void OnBounced(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Stand>(out var stand)) 
            stand.DrawHit(hit);

        _bouncesToExplosion--;
        
        if (_bouncesToExplosion <= 0) 
            Explode();
    }

    private void Explode()
    {
        gameObject.SetActive(false);
        var explosion = ExplosionPool.Instance.GetNew();
        explosion.transform.position = transform.position;
    }
    
}