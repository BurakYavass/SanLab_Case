using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectRotater : ObjectID
{
   [SerializeField] private float _rotationSpeed;
   [SerializeField] private Collider _capsuleCollider;
   private float _objectAngle;

    private void Start()
    {
        if (!_capsuleCollider)
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _capsuleCollider.enabled = true;
        }
        else
        {
            _capsuleCollider.enabled = false;
        }
    }

    public void ObjectRotate()
    {
        _objectAngle += -Input.GetAxis("Mouse X") * _rotationSpeed;

        transform.localRotation = Quaternion.Euler(0, _objectAngle, 0);
    }
}
