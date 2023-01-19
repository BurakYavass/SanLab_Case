using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;
    //public ManagerState managerState;
    

    [SerializeField] private List<DragItem> DragItems = new List<DragItem>();

    private void Awake()
    {
        if (instance == null)
        {
            instance= this;
        }
    }


    public void AddList(DragItem item) => DragItems.Add(item);
    

    public void RemoveList(DragItem item) => DragItems.Remove(item);
    
}
