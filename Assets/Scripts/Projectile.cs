using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
internal class Projectile : MonoBehaviour
{
    private Rigidbody _body;

    public Rigidbody Rigidboy => _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
}