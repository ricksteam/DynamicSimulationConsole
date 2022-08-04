using System;
using System.Collections.Generic;
using UnityEngine;


public class Grid
{
    private readonly int _gridWidth;
    private readonly int _gridHeight;
    private readonly int _tileSize;

    private readonly Dictionary<GridPosition, Tile> _grid;

    public Grid(int width, int height, int tileSize)
    {
        _gridWidth = width;
        _gridHeight = height;
        _tileSize = tileSize;
        _grid = new Dictionary<GridPosition, Tile>();
         
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (var x = 0; x < _gridWidth; x++)
        {
            for (var z = 0; z < _gridHeight; z++)
            {
                var gridPosition = new GridPosition(x, z);
                var tile = new Tile(gridPosition);
                _grid.Add(gridPosition, tile);
            }
        }
        
    }
    

    public void LoopThroughGrid(Action<GridPosition, Tile> callback)
    { 
        for (var x = 0; x < _gridWidth; x++)
        {
            for (var z = 0; z < _gridHeight; z++)
            {
                var gridPosition = new GridPosition(x, z);
                var tile = GetTileAtPosition(gridPosition);
                callback(gridPosition, tile);
            }
        }
    }

    private void SetTileTypeAtGridPosition(GridPosition position, TileType tileType)
    {
        if (_grid.TryGetValue(position, out var tile))
        {
            tile.SetTileType(tileType);
        }
    }

    //public void SetTileAsStart(GridPosition gridPosition)
    //{
    //    SetTileTypeAtPosition(gridPosition, TileType.Start);
    //}

    //public void SetTileAsEnd(GridPosition gridPosition)
    //{
    //    SetTileTypeAtPosition(gridPosition, TileType.End);
    //}
    public void SetTileAsPath(GridPosition gridPosition)
    {
        SetTileTypeAtGridPosition(gridPosition, TileType.Road);
    }
    
    public void SetTileAsEmpty(GridPosition gridPosition)
    {
        SetTileTypeAtGridPosition(gridPosition, TileType.Empty);
    }


    public Tile GetTileAtPosition(GridPosition position)
    {
        return _grid.TryGetValue(position, out var tile) ? tile : null;
    }

    public Vector3 GetWorldPosition(GridPosition position)
    {
        return position.ToVector3() * _tileSize;
    }

    

    public bool IsTileOccupiedAtPosition(GridPosition position)
    {
        return GetTileAtPosition(position).IsOccupied();
    }

    public int Width() => _gridWidth;
    public int Height() => _gridHeight;



}
