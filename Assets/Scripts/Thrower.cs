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
    private KeyCode _pushButton = KeyCode.Q;
    private KeyCode _returnButton = KeyCode.E;
    private bool _canLaunch;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _springJoint = GetComponent<SpringJoint>();
        _startPosition = _springJoint.anchor;
        _canLaunch = true;
    }

    private void Start()
    {
        _projectile = SpawnProjectile();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_pushButton) && _canLaunch == true)
        {
            _canLaunch = false;
            _springJoint.anchor = _throwPosition;
            _rigidbody.WakeUp();
            _projectile.Rigidboy.AddForce(_appliedForce);
            Destroy(_projectile.gameObject, _destrictionTime);
        }

        if(Input.GetKeyDown(_returnButton) & _canLaunch == false) 
        {
            StartCoroutine(ReturnToDefaultPosition());
            _canLaunch = true;
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
