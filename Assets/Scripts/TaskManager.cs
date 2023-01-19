using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;
    
    [SerializeField] private List<DragItem> DragItems = new List<DragItem>();

    private bool complete = false;    

    private void Awake()
    {
        if (instance == null)
        {
            instance= this;
        }
    }
    private void Update()
    {
        if (DragItems.Count == 10)
            complete = true;
        else
            complete = false;
    }


    public void AddList(DragItem item) => DragItems.Add(item);
    

    public void RemoveList(DragItem item) => DragItems.Remove(item);
    
}
