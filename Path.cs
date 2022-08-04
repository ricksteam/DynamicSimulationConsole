using System.Collections.Generic;

public class Path
{
    private readonly Grid _grid;
    private readonly List<GridPosition> _path;

    private GridPosition _startPosition;
    private GridPosition _endPosition;

    public Path(Grid grid)
    {
        _grid = grid;
        _path = new List<GridPosition>();
    }

    public void Clear()
    {
        _path.Clear();
    }
    public GridPosition GetStartPosition() => _startPosition;
    public GridPosition GetEndPosition() => _endPosition;
    
    public void Create(GridPosition startPosition, GridPosition endPosition, int pathZ)
    {
        _startPosition = startPosition;
        _endPosition = endPosition;
        
        //_grid.SetTileAsStart(_startPosition);
        //_grid.SetTileAsEnd(_endPosition);
        
        _path.Add(_startPosition);
        GeneratePath(pathZ); 
        _path.Add(_endPosition);
    }

    private void GeneratePath(int pathZ)
    {
        _path.Clear();
        // TODO: Implement A*
        
        _grid.LoopThroughGrid(((gridPosition, tile) =>
        {
            if (gridPosition == null) return;
            
            if (gridPosition.Z == pathZ &&  tile.GetTileType() == TileType.Empty)
            {
                _grid.SetTileAsPath(gridPosition);
                _path.Add(gridPosition);
            }
        }));
    }
    
    public List<GridPosition> AsList() => _path;

    public Queue<GridPosition> AsQueue()
    {
        var newQueue = new Queue<GridPosition>();
        foreach (GridPosition position in _path)
        {
            newQueue.Enqueue(position);
        }

        return newQueue;
    }
}
