using System.Linq;
using UnityEngine;

public class Game
{
  private readonly int shotCount = 5;
  private readonly int successCountTarget = 3;

  private int currentShot;
  private int successfulShotCount;
  private int failShotCount;
  private Shot[] shoots;
  public Game()
  {
    shoots = new Shot[shotCount];
    currentShot = 0;
    successfulShotCount = 0;
    failShotCount = 0;
  }
  public void ShotIsSuccessful()
  {
    successfulShotCount++;
    Debug.Log("success");
    shoots[currentShot].isSuccess = true;

    Managers.Score.AddScore(shoots[currentShot].score + 1);

    if (didWin())
    {
      Managers.Game.SetState(typeof(GameWonState));
      return;
    }

    //add next shot score bonus
    shoots[currentShot + 1].score += shoots[currentShot].score + 1;
    currentShot++;
  }

  public void ShotIsFail()
  {
    Debug.Log("fail");
    shoots[currentShot].isSuccess = false;
    currentShot++;
    failShotCount++;

    if (didLost())
    {
      Managers.Game.SetState(typeof(GameLostState));
      return;
    }

    //reset next shot bonus score
    shoots[currentShot].score = 0;
  }

  public bool didWin()
  {
    return successfulShotCount >= successCountTarget;
  }
  public bool didLost()
  {
    return failShotCount > shotCount - successCountTarget;
  }

}
