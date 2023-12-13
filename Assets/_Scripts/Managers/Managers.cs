using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(ScoreManager))]

public class Managers : MonoBehaviour
{
  public static GameManager Game { get; private set; }
  public static UIManager UI { get; private set; }
  public static ScoreManager Score { get; private set; }


  void Awake()
  {
    Game = GetComponent<GameManager>();
    UI = GetComponent<UIManager>();
    Score = GetComponent<ScoreManager>();

  }
}
