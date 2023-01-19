using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;
    private MouseContoller _mouseContoller;   

    [SerializeField] private GameObject _uIPanel;
    [SerializeField] private Transform _dragDropTask;
    [SerializeField] private List<DragItem> _dragItems = new List<DragItem>();

    public bool complete = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Transform child in _dragDropTask.transform)
        {
            _dragItems.Add(child.GetComponent<DragItem>());
        }
    }

    private void Start()
    {
        _mouseContoller = GetComponent<MouseContoller>();        
    }

    
    private void Update()
    {
        complete = _dragItems.All(x => x.onDropPlace == true);

        if (complete)
        {
            _mouseContoller.complete = true;
            _uIPanel.SetActive(true);
        }
        else
        {
            _mouseContoller.complete = false;
            _uIPanel.SetActive(false);
        }

    }
}
