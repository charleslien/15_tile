using UnityEngine;
using UnityEngine.UI;

public class BoardTimer : MonoBehaviour
{
  public GameObject BoardGameObject;

  private Board _board;
  private Text _text;

  void Start()
  {
    _text = gameObject.GetComponent<Text>();
    _board = BoardGameObject.GetComponent<BoardScript>().BoardInstance;
  }

  void LateUpdate()
  {
    _text.text = _board.Time.ToString("F3");
  }
}
