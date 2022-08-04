using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VehicleAI : MonoBehaviour
{
    private GridPosition _currentPosition;

    [SerializeField] private float moveSpeed = 1f;

    private Grid _grid;
    private Queue<GridPosition> _path;
    
    
    private void Start()
    {
        _grid = GridGenerator.Instance.GetGrid();
        _path = GridGenerator.Instance.GetPath().AsQueue();
        _currentPosition = _path.Dequeue();
    } 

    private void Update()
    {
        var vectorPos = _grid.GetWorldPosition(_currentPosition);
        vectorPos.z -= 0.5f;
        vectorPos.y = 0.5f;
        if (Vector3.Distance(transform.position, vectorPos) <= 0.025f)
        {
            transform.position = vectorPos;
            if (_path.Count > 0)
            {
                _currentPosition = _path.Dequeue();
            }
            else
            {
                
                Destroy(gameObject);
            }
            
        }
        else
        {
            var dir = (vectorPos - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(dir), 5 * Time.deltaTime);
        }
        
    }
}
