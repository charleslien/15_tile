using System;
using System.Collections.Generic;

public class Board
{
  /* Ending Position
   *  1  2  3  4
   *  5  6  7  8
   *  9 10 11 12
   * 13 14 15  0
   */
  private static int _zero = 0;
  public static ISet<int> Tiles = new HashSet<int>
  {
    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
  };
  private static Dictionary<int, (int, int)> _endingPositionByTile =
    new Dictionary<int, (int, int)>()
    {
      {1, (0, 3) },
      {2, (1, 3) },
      {3, (2, 3) },
      {4, (3, 3) },
      {5, (0, 2) },
      {6, (1, 2) },
      {7, (2, 2) },
      {8, (3, 2) },
      {9, (0, 1) },
      {10, (1, 1) },
      {11, (2, 1) },
      {12, (3, 1) },
      {13, (0, 0) },
      {14, (1, 0) },
      {15, (2, 0) },
      {_zero, (3, 0) },
    };
  private static Dictionary<(int, int), int> _endingTileByPosition =
   new Dictionary<(int, int), int>()
   {
      {(0, 3), 1 },
      {(1, 3), 2 },
      {(2, 3), 3 },
      {(3, 3), 4 },
      {(0, 2), 5 },
      {(1, 2), 6 },
      {(2, 2), 7 },
      {(3, 2), 8 },
      {(0, 1), 9 },
      {(1, 1), 10 },
      {(2, 1), 11 },
      {(3, 1), 12 },
      {(0, 0), 13 },
      {(1, 0), 14 },
      {(2, 0), 15 },
      {(3, 0), _zero },
   };

  private Dictionary<(int, int), int> _tileByPosition;
  private Dictionary<int, (int, int)> _positionByTile;
  private (int, int) _zeroPosition;
  public float Time;
  public bool InProgress;

  public bool HasBeenUpdated;

  public Board()
  {
    _initialize();
  }

  private void _initialize()
  {
    _tileByPosition = new Dictionary<(int, int), int>(_endingTileByPosition);
    _positionByTile = new Dictionary<int, (int, int)>(_endingPositionByTile);
    _zeroPosition = (3, 0);

    HasBeenUpdated = true;
    InProgress = false;
    Time = 0f;
  }

  public bool IsSolved()
  {
    foreach (int tile in Tiles)
    {
      if (_positionByTile[tile] != _endingPositionByTile[tile])
      {
        return false;
      }
    }
    return true;
  }

  public int GetTile((int, int) position)
  {
    return _tileByPosition[position];
  }

  public (int, int) GetPosition(int tile)
  {
    return _positionByTile[tile];
  }

  public bool Move((int, int) position)
  {
    if (!_canMove(position))
    {
      return false;
    }

    _move(position);
    return true;
  }
  private bool _canMove((int, int) position)
  {
    if (!_tileByPosition.ContainsKey(position))
    {
      return false;
    }

    if (position == _zeroPosition)
    {
      return false;
    }

    return position.Item1 == _zeroPosition.Item1 || position.Item2 == _zeroPosition.Item2;
  }

  private void _move((int, int) position)
  {
    int x = position.Item1;
    int y = position.Item2;

    int dx = Math.Sign(x - _zeroPosition.Item1);
    int dy = Math.Sign(y - _zeroPosition.Item2);

    for (int i = _zeroPosition.Item1, j = _zeroPosition.Item2; (i, j) != (x, y); i += dx, j += dy)
    {
      _setTilePosition(_tileByPosition[(i + dx, j + dy)], (i, j));
    }
    _setTilePosition(_zero, (x, y));
  }

  /** This may leave the board in an incorrect state. */
  private void _setTilePosition(int tile, (int, int) position)
  {
    _tileByPosition[position] = tile;
    _positionByTile[tile] = position;
    if (tile == _zero)
    {
      _zeroPosition = position;
    }
    HasBeenUpdated = true;
  }

  public void Randomize()
  {
    _initialize();
    Random rand = new Random();

    bool timesSwappedEven = true;
    foreach (int tile in Tiles)
    {
      int targetTile = _getRandomTile(rand);
      if (tile == targetTile)
      {
        continue;
      }
      _swapTiles(tile, targetTile);
      timesSwappedEven = !timesSwappedEven;
    }

    if (timesSwappedEven ^ (_zeroPosition.Item1 + _zeroPosition.Item2) % 2 == 1)
    {
      // swap any 2 non-zero tiles
      _swapTiles(1, 2);
    }
  }

  private int _getRandomTile(Random rand)
  {
    byte[] bytes = new byte[1];
    rand.NextBytes(bytes);
    return bytes[0] % 16;
  }

  private void _swapTiles(int tile1, int tile2)
  {
    (int, int) position1 = _positionByTile[tile1];
    (int, int) position2 = _positionByTile[tile2];

    _setTilePosition(tile1, position2);
    _setTilePosition(tile2, position1);
  }
}
