using System;
using UnityEngine;

public class DropPlace : ObjectID
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private DragItem _dragItem;

    

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }


    public void RendererControl(bool visiable)
    {
        if (visiable)
        {
            _renderer.enabled = true;
        }
        else
        {
            _renderer.enabled = false;
        }
    }
}
