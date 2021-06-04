using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public SpriteKeyValuePair[] Sprites;

  private Dictionary<char, Sprite> _spriteMap;

  [Serializable]
  public struct SpriteKeyValuePair
  {
    public char Key;
    public Sprite Value;
  }

  public Sprite GetSpriteFromChar(char c)
  {
    return _spriteMap[c];
  }

  // Singleton constructor
  public static GameManager Instance { get; private set; }

  // --- MonoBehaviour ---
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    _spriteMap = new Dictionary<char, Sprite>();
    foreach (SpriteKeyValuePair skvp in Sprites)
    {
      KeyValuePair<char, Sprite> kvp = _convertSpriteKeyValuePair(skvp);
      _spriteMap.Add(kvp.Key, kvp.Value);
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Application.Quit();
    }
  }

  // --- private ---
  private KeyValuePair<char, Sprite> _convertSpriteKeyValuePair(SpriteKeyValuePair skvp)
  {
    return new KeyValuePair<char, Sprite>(skvp.Key, skvp.Value);
  }
}
