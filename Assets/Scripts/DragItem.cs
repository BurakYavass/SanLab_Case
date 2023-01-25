using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DragItem : ObjectID
{
    public DropPlace dropItem;
    public Transform firstParent;
    private Transform _dropPlaceTransform;

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
    [SerializeField] private AnimationClip assableAnimationClip;
    [SerializeField] private AnimationClip dissembleAnimationClip;
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
            _per += 2f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, _dropPlaceTransform.position, _per);
            transform.eulerAngles = _dropPlaceTransform.eulerAngles;
            transform.parent = _dropPlaceTransform.parent;
            onDropPlace = true;

            if (_animation && !_animation.isPlaying)
            {
                _animation.AddClip(assableAnimationClip,"clip1");
                _animation.clip = _animation.GetClip("clip1");
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
                    _animation.AddClip(dissembleAnimationClip, "clip2");
                    _animation.clip = _animation.GetClip("clip2");
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
            _animationComplete= false;
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
        }      

        return holdable;
    }

    

    public void OnDropPlace(Transform transform , bool animation)
    {
        onDropPlace= true;
        _dropPlaceAnimation= true;
        _dropPlaceTransform = transform;      
    }

    public void OutDropPlace()
    {    
        _outDropPlaceAnimation = true; 
    }

    public void IsAnimationComplete()
    {
        _animationComplete= true;
    }
    
}
