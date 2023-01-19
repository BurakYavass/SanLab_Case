using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DragItem : ObjectID
{
    public DropPlace dropItem;
    public Transform firstParent;
    private Transform dropPlaceTransform;

    public bool onDropPlace = false;
    public bool holdable = false;
    private bool _dropPlaceAnimation;
    private bool _outDropPlaceAnimation;
    private bool _animationComplete = false;

    private float _per;
    private float _per2;

    [NonSerialized] public Vector3 firstPosition;
    [NonSerialized] public Vector3 firstRotation;

    [SerializeField] private Animation _animation;
    [SerializeField] private DragItem[] assembleItems;
    [SerializeField] private DragItem[] dissembleItems;

    private void Start()
    {
        firstPosition= transform.position;
        firstRotation = transform.eulerAngles;
        firstParent= transform.parent;
    }

    private void Update()
    {
        if (_dropPlaceAnimation)
        {
            _per += 2.5f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dropPlaceTransform.position, _per);
            transform.eulerAngles = dropPlaceTransform.eulerAngles;
            transform.parent = dropPlaceTransform.parent;
            onDropPlace = true;

            if (_animation && !_animation.isPlaying)
            {
                _animation.clip = _animation.GetClip("Bolt_Animation");
                _dropPlaceAnimation = false;
                _animation.Play();
            }
            if (_per >= 1)
                _dropPlaceAnimation = false;   
          
        }
        else
        {
            _per = 0;
        }

        if (_outDropPlaceAnimation)
        {
            if (_animation && onDropPlace)
            {
                if(!_animation.isPlaying && !_animationComplete)
                {
                    _animation.clip = _animation.GetClip("BoltRemove_Animation");
                    _animation.Play();
                }
                
                if (_animationComplete)
                {
                    MoveDefaultPosition();
                }
               
            }
            else
            {
                MoveDefaultPosition();
            }
        }
        else
        {
            _per2 = 0;
        }

    }

    private void MoveDefaultPosition()
    {
        _per2 += 2.5f * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, firstPosition, _per2);
        transform.eulerAngles = firstRotation;
        transform.parent = firstParent;
        if (_per2 >= 1)
        {
            _outDropPlaceAnimation = false;
        }
        onDropPlace = false;
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

            //if (holdable && _animation && !_animationComplete)           
            //    holdable = false;          
            //else if (holdable && _animation && _animationComplete)
            //    holdable = true;

        }      

        return holdable;
    }

    

    public void OnDropPlace(Transform transform , bool animation)
    {
        onDropPlace= true;
        _dropPlaceAnimation= true;
        dropPlaceTransform = transform;      
    }

    public void OutDropPlace()
    {
        //onDropPlace= false;
        _outDropPlaceAnimation = true; 
    }

    public void IsAnimationComplete()
    {
        _animationComplete= true;
    }
    
}
