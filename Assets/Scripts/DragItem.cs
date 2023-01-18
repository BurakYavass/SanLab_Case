using System;
using UnityEngine;

public class DragItem : ObjectID
{
    private Collider _collider;

    public DropPlace dropItem;

    public Vector3 firstPosition;
    public Vector3 firstRotation;
    public Transform firstParent;


    public bool dropPlace = false;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        firstPosition= transform.position;
        firstRotation = transform.eulerAngles;
        firstParent= transform.parent;
    }

   

 
}
