using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    [SerializeField]
    float _movementSpeed = 1;
    [SerializeField]
    float _turnSpeed = 1;
    [SerializeField]
    Transform _startPosition;
    [SerializeField]
    Transform _rotationTransform;
    [SerializeField]
    Transform _movementTransform;

    [SerializeField]
    Animator _MovementAnimator;
    Rigidbody _rb;
    Ray _cameraRay;
    RaycastHit[] hits;
    Dictionary<Renderer, Material> _transparentObjects = new Dictionary<Renderer, Material>();
    List<Renderer> _objectsToRestore = new List<Renderer>();
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        _MovementAnimator.SetBool("Sleeping", true);
        _movementTransform = transform;
        _rb = GetComponent<Rigidbody>();
        _movementTransform.localPosition = _startPosition.localPosition;
        _movementTransform.localRotation = _startPosition.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_MovementAnimator.GetCurrentAnimatorStateInfo(0).IsName("Standing Up"))
        {
            _MovementAnimator.SetBool("Sleeping", false);
        }
        else if (_MovementAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk Blend Tree"))
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            _MovementAnimator.SetFloat("WalkX", movement.x);
            _MovementAnimator.SetFloat("WalkY", movement.y);

            var up = Vector3.forward * Time.deltaTime * _movementSpeed * movement.y;
            _movementTransform.Translate(up);

            
            var targetRotation = _movementTransform.localRotation * Quaternion.Euler(0, 120 * movement.x, 0);
            _movementTransform.rotation = Quaternion.Slerp(_rotationTransform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
            
        }
    }

}
