using System;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : ObjectID
{
    public DropPlace dropItem;

    public Vector3 firstPosition;
    public Vector3 firstRotation;
    public Transform firstParent;

    public bool dropPlace = false;
    public bool holdable = false;

    [SerializeField] private DragItem[] assembleItems;
    [SerializeField] private DragItem[] dissembleItems;

    public int assembleCount;
    private int dissembleCount;

    private void Start()
    {
        firstPosition= transform.position;
        firstRotation = transform.eulerAngles;
        firstParent= transform.parent;
    }


    public bool HoldAble()
    {
        for (int i = 0; i < assembleItems.Length; i++)
        {
            if (assembleItems[i].dropPlace)
            {
                assembleCount++;
            }
        }

        if (assembleCount == assembleItems.Length)
        {
            holdable = true;
        }
        else
        {
            holdable = false;
            assembleCount= 0;
        }
        return holdable;
    }

    public void OnDropPlace()
    {
        TaskManager.instance.AddList(this);
    }

    public void OutDropPlace()
    {
        TaskManager.instance.RemoveList(this);
    }

    
}
