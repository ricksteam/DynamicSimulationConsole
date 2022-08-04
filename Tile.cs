public class Tile
{
     private GridPosition _gridPosition;

     private TileType _tileType;

     public Tile(GridPosition gridPosition)
     {
          _tileType = TileType.Empty;
          _gridPosition = gridPosition;
     }

     public void SetTileType(TileType tileType)
     {
          _tileType = tileType;
     }

     public bool IsOccupied() => _tileType != TileType.Empty;
     public TileType GetTileType() => _tileType;
}


public enum TileType
{
     Empty,
     Road
}