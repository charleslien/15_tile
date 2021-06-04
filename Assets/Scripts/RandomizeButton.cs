using UnityEngine;
using UnityEngine.UI;

public class RandomizeButton : MonoBehaviour
{
  public GameObject BoardGameObject;
  
  private Board _board;

  void Start()
  {
    gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    _board = BoardGameObject.GetComponent<BoardScript>().BoardInstance;
  }

  private void OnClick()
  {
    _board.Randomize();
  }
}
