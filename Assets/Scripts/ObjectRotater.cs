using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectRotater : ObjectID
{
   [SerializeField] private float _rotationSpeed;
   private float _objectAngle;

   public void ObjectRotate()
    {
        _objectAngle += Input.GetAxis("Mouse X") * _rotationSpeed;

        transform.localRotation = Quaternion.Euler(0,_objectAngle,0);
    }
}
