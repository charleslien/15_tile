using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BoardScript : MonoBehaviour
{
  public Board BoardInstance { get; private set; }
  public Text timer;

  void Awake()
  {
    BoardInstance = new Board();
  }

  void Update()
  {
    if (BoardInstance.InProgress)
    {
      BoardInstance.Time += Time.deltaTime;
    }

    if (Input.GetMouseButton(/* LMB */ 0) && !BoardInstance.IsSolved())
    {
      Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      (int, int) positionClicked = ((int)Math.Round(mouseWorldPosition.x), (int)Math.Round(mouseWorldPosition.y));
      if (BoardInstance.Move(positionClicked))
      {
        BoardInstance.InProgress = true;
      }
    }

    if (!BoardInstance.HasBeenUpdated)
    {
      return;
    }

    foreach (int tile in Board.Tiles)
    {
      GameObject tileObject = GameObject.Find("Tile " + tile);
      (int, int) tilePosition = BoardInstance.GetPosition(tile);
      tileObject.transform.position = new Vector3(tilePosition.Item1, tilePosition.Item2, 0);
    }
    BoardInstance.HasBeenUpdated = false;

    if (BoardInstance.IsSolved())
    {
      BoardInstance.InProgress = false;
    }
  }
}
