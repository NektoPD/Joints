using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pusher : MonoBehaviour
{
    [SerializeField] private Vector3 _force; 

    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _rigidbody.AddForce(_force);
        }
    }
}
