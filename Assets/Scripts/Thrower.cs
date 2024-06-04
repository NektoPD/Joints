using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
[RequireComponent(typeof(Rigidbody))]
public class Thrower : MonoBehaviour
{
    [SerializeField] private Transform _projectilePosition;
    [SerializeField] private Projectile _prefab;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _throwPosition;
    [SerializeField] private Vector3 _appliedForce;

    private Rigidbody _rigidbody;
    private SpringJoint _springJoint;
    private Projectile _projectile;
    private int _spawnDelay = 1;
    private int _destrictionTime = 5;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
        _startPosition = _springJoint.anchor;
    }

    private void Start()
    {
        _projectile = SpawnProjectile();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            _springJoint.anchor = _throwPosition;
            _rigidbody.WakeUp();
            _projectile.GetComponent<Rigidbody>().AddForce(_appliedForce);
            Destroy(_projectile.gameObject, _destrictionTime);
        }

        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            StartCoroutine(ReturnToDefaultPosition());
        }
    }

    private Projectile SpawnProjectile()
    {
        return Instantiate(_prefab, _projectilePosition.position, Quaternion.identity);
    }

    private IEnumerator ReturnToDefaultPosition()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

        while (_springJoint.anchor != _startPosition)
        {
            _springJoint.anchor = _startPosition;

            yield return delay;
        }

        _projectile = SpawnProjectile();
    }
}
