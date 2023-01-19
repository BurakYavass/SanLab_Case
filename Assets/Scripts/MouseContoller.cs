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
    private Vector3 _offSet;


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
        Vector3 mousePos;


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;

            if (Physics.Raycast(ray, out hitObject))
            {
                var objectId = hitObject.transform.GetComponent<ObjectID>();
                if (objectId == null)
                    return;

                //Object type kontrolu
                if (objectId.Type == ObjectID.ObjectType.RotateItem)
                {
                    _rotateObject = hitObject.transform.GetComponent<ObjectRotater>();
                }                   
                else if(hitObject.transform.GetComponent<DragItem>().HoldAble())
                {
                    _dragItemSc = hitObject.transform.GetComponent<DragItem>();
                    _dropPlaceSc = _dragItemSc.dropItem;
                }

                                 

                if (_dragItemSc != null)
                {                 
                    _dragging = true;
                    _dropArea = false;
                    
                    Vector3 screenPoint = _camera.WorldToScreenPoint(hitObject.point);
                    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    mousePos = _camera.ScreenToWorldPoint(mousePos);

                    _offSet = mousePos - hitObject.point;
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
                Vector3 screenPoint = _camera.WorldToScreenPoint(_dragItemSc.transform.position);
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                mousePos = _camera.ScreenToWorldPoint(mousePos);

                //Drag object position change
                _dragItemSc.transform.position = mousePos - _offSet;

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
