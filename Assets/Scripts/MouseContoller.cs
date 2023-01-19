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
    
    

    private Camera Camera;
    private Vector3 offSet;


    private bool dragging = false;
    private bool dropArea = false; 
    public bool complete = false;

 
    void Start()
    {
        Camera = Camera.main;       
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
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
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
                    //Hit ettigimiz objeyi toDrag objesine atiyoruz
                    //_dragItemSc = hitObject.transform;
                    
                    dragging = true;
                    dropArea = false;
                    

                    Vector3 screenPoint = Camera.WorldToScreenPoint(hitObject.point);
                    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    mousePos = Camera.ScreenToWorldPoint(mousePos);

                    offSet = mousePos - hitObject.point;
                }
                else
                {
                    dragging = false;
                    
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            // Drag Item move
            if (dragging && !_rotateObject)
            {
                Vector3 screenPoint = Camera.WorldToScreenPoint(_dragItemSc.transform.position);
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                mousePos = Camera.ScreenToWorldPoint(mousePos);

                //Drag object position change
                _dragItemSc.transform.position = mousePos - offSet;

                var distance = Vector3.Distance(_dragItemSc.transform.position, _dropPlaceSc.transform.position);              
                if (distance < 0.8f)
                {                   
                    dropArea = true;
                    _dropPlaceSc.RendererControl(true);
                }
                else
                {                 
                    dropArea = false;
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

            if (dropArea)
            {
                dropArea = false;
                dragging = false;

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
                dragging = false;
                dropArea = false;
                _dragItemSc.OutDropPlace();

                _dragItemSc = null;
                _dropPlaceSc = null;
            }
        }
    }
}
