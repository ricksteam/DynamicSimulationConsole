
public class GridGenerator 
{
    private readonly Grid _grid;
    private readonly Path _path;


    private int _gridWidth = 10;
    private int _gridHeight = 10;
    private int _tileSize = 3;
    private bool _generatePath;

    public GridGenerator(int gridWidth, int gridHeight)
    {
        _gridWidth = gridWidth;
        _gridHeight = gridHeight;
        _grid = new Grid(_gridWidth, _gridHeight, _tileSize);
        _path = new Path(_grid);
    }

    public void Spawn()
    {
        
        
        var zPath = (int)Math.Round(_grid.Height() / 2f);

        var startPosition = new GridPosition(0, zPath);
        var endPosition = new GridPosition(_grid.Width() - 1, zPath);

        if (_generatePath)
        {
            _path.Create(startPosition, endPosition, zPath);
        }
        else
        {
            _path.Clear();
            _grid.LoopThroughGrid((gridPosition, tile) =>
            {
                tile.SetTileType(TileType.Empty);
            }); 
        }

        //GridVisualizer.Instance.BuildGrid(_grid);
    }

    public Path GetPath() => _path;
    public Grid GetGrid() => _grid;
    
}
