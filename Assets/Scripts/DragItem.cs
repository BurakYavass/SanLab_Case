using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragItem : ObjectID
{
    public DropPlace dropItem;

    public Vector3 firstPosition;
    public Vector3 firstRotation;
    public Transform firstParent;

    public bool onDropPlace = false;
    public bool holdable = false;

    [SerializeField] private DragItem[] assembleItems;
    [SerializeField] private DragItem[] dissembleItems;

    private void Start()
    {
        firstPosition= transform.position;
        firstRotation = transform.eulerAngles;
        firstParent= transform.parent;
    }


    public bool HoldAble()
    {
       
        if (!onDropPlace)
        {
            holdable = assembleItems.All(x => x.onDropPlace == true);
        }
        else
        {
            holdable = dissembleItems.All(x => x.onDropPlace == false);
        }
            
        return holdable;
    }

    public void OnDropPlace()
    {
        //TaskManager.instance.AddList(this);
    }

    public void OutDropPlace()
    {
        //TaskManager.instance.RemoveList(this);
    }

    
}
