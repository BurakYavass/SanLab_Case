using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MouseContoller : MonoBehaviour
{

    [SerializeField] private DragItem _dragItemSc;
    [SerializeField] private DropPlace _dropPlaceSc;
    [SerializeField] private ObjectRotater _rotateObject;
    
    private Camera _camera;
    private Vector3 _offset;
    private Vector3 _screenPoint;

    private bool _dragging = false;
    private bool _dropArea = false; 
    public bool complete = false;

 
    void Start()
    {
        _camera = Camera.main;       
    }

    
    void Update()
    {
        if (!complete)
            MouseController();
    }

    private void MouseController()
    {
        Vector3 mouseScreenPos;


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var objectId = hit.transform.GetComponent<ObjectID>();
                if (objectId == null)
                    return;

                //Object type kontrolu
                if (objectId.Type == ObjectID.ObjectType.RotateItem)
                {
                    _rotateObject = hit.transform.GetComponent<ObjectRotater>();
                }                   
                else if(hit.transform.GetComponent<DragItem>().HoldAble())
                {
                    _dragItemSc = hit.transform.GetComponent<DragItem>();
                    _dropPlaceSc = _dragItemSc.dropItem;
                }

                                 

                if (_dragItemSc != null)
                {                 
                    _dragging = true;
                    _dropArea = false;
                    
                    _screenPoint = _camera.WorldToScreenPoint(hit.point);
                    mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
                    mouseScreenPos = _camera.ScreenToWorldPoint(mouseScreenPos);

                    _offset = _dragItemSc.transform.position - mouseScreenPos;
                }
                else
                {
                    _dragging = false;
                    
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            // Drag Item move
            if (_dragging && !_rotateObject)
            {
                
                mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
                mouseScreenPos = _camera.ScreenToWorldPoint(mouseScreenPos);

                //Drag object position change
                _dragItemSc.transform.position = mouseScreenPos + _offset;

                var distance = Vector3.Distance(_dragItemSc.transform.position, _dropPlaceSc.transform.position);              
                if (distance < 0.8f)
                {                   
                    _dropArea = true;
                    _dropPlaceSc.RendererControl(true);
                }
                else
                {                 
                    _dropArea = false;
                    _dropPlaceSc.RendererControl(false);

                }
            }
            
            if (_rotateObject)
            {
                _rotateObject.ObjectRotate();
            }

        }

        else if (Input.GetMouseButtonUp(0))
        {
            
            if (_rotateObject) 
                _rotateObject = null;
            
            
            if (_dropPlaceSc == null && _dragItemSc == null)
                return;

            if (_dropArea)
            {
                _dropArea = false;
                _dragging = false;

                if (_dragItemSc != null)
                {                    
                    _dragItemSc.OnDropPlace(_dropPlaceSc.transform,true);
                    _dropPlaceSc.RendererControl(false);
                 
                }

                _dragItemSc = null;
                _dropPlaceSc = null;
            }
            else 
            {
                _dragging = false;
                _dropArea = false;
                _dragItemSc.OutDropPlace();

                _dragItemSc = null;
                _dropPlaceSc = null;
            }
        }
    }
}
