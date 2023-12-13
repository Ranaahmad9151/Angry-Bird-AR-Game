using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
  [SerializeField] private GameObject uiPanel;
  [SerializeField] private GameObject startButton;
  [SerializeField] private GameObject restartButton;
  [SerializeField] private Image[] successImages;
  [SerializeField] private Text scoreText;
  [SerializeField] private GameObject winText;
  [SerializeField] private GameObject lostText;
  [SerializeField] private Color successColor;


  public void EnableMenuPanel() => uiPanel.SetActive(true);
  public void DisableMenuPanel() => uiPanel.SetActive(false);
  public void EnableStartButton() => startButton.SetActive(true);
  public void DisableStartButton() => startButton.SetActive(false);
  public void EnableRestartButton() => restartButton.SetActive(true);
  public void DisableRestartButton() => restartButton.SetActive(false);
  public void EnableScoreText() => scoreText.gameObject.SetActive(true);
  public void DisableScoreText() => scoreText.gameObject.SetActive(false);
  public void EnableWinText() => winText.SetActive(true);
  public void DisableWinText() => winText.SetActive(false);

  public void EnableLostText() => lostText.SetActive(true);
  public void DisableLostText() => lostText.SetActive(false);

  private void OnEnable()
  {
    WinTrigger.OnSuccess += ShotSuccess;

  }
  private void OnDisable()
  {
    WinTrigger.OnSuccess -= ShotSuccess;

  }


  private void ShotSuccess()
  {
    for (int i = 0; i < successImages.Length; i++)
    {
      if (!successImages[i].color.Equals(successColor))
      {
        successImages[i].color = successColor;
        break;
      }

    }
  }

  public void SetScore(int score)
  {
    scoreText.text = "SCORE : " + score.ToString();
  }
  public void ResetUI()
  {
    DisableMenuPanel();
    DisableStartButton();
    DisableMenuPanel();
    ResetColors();
  }
  public void ResetColors()
  {
    for (int i = 0; i < successImages.Length; i++)
    {
      successImages[i].color = Color.white;

    }
  }

}
