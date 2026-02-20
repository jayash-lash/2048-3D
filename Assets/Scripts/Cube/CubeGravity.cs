using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeGravity : MonoBehaviour
    {
        [SerializeField] private float _gravityMultiplier = 1.4f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(Physics.gravity * (_gravityMultiplier - 1f), ForceMode.Acceleration);
        }
    }
}