using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
  public float X, Y;
  public TextAlignment Alignment;

  private List<GameObject> _spriteGameObjects;
  private List<SpriteRenderer> _spriteRenderers;
  private Text _text;

  void Start()
  {
    _spriteGameObjects = new List<GameObject>();
    _spriteRenderers = new List<SpriteRenderer>();
    _text = gameObject.GetComponent<Text>();
    _text.enabled = false;
  }

  void LateUpdate()
  {
    switch (Alignment)
    {
      case TextAlignment.Center:
        break;
      case TextAlignment.Left:
        break;
      case TextAlignment.Right:
        _renderTextAlignRight();
        break;
    }
  }

  private void _renderTextAlignRight()
  {
    while (_spriteGameObjects.Count < _text.text.Length)
    {
      GameObject newGO = new GameObject();
      newGO.name = gameObject.name + "_" + _spriteGameObjects.Count;
      newGO.transform.position = new Vector3(X - (_spriteGameObjects.Count * 0.3125f) - 0.125f, Y, 0);

      SpriteRenderer newSR = newGO.AddComponent<SpriteRenderer>();
      newSR.transform.localScale = new Vector3(6.25f, 6.25f, 1f);

      _spriteGameObjects.Add(newGO);
      _spriteRenderers.Add(newSR);
    }

    for (int i = 0; i < _spriteRenderers.Count; i++)
    {
      if (i >= _text.text.Length)
      {
        _spriteRenderers[i].enabled = false;
        continue;
      }

      char toRender = _text.text[_text.text.Length - 1 - i];
      _spriteRenderers[i].sprite = GameManager.Instance.GetSpriteFromChar(toRender);
      _spriteRenderers[i].enabled = true;
    }
  }
}
