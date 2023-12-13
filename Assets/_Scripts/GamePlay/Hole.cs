using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

  public static event Action OnTargetHit = delegate { };

  [Header("MOVEMENT SETTINGS")]
  [SerializeField] private bool isMoveable = true;
  [SerializeField] private float moveSpeed = 2.0f;
  [SerializeField] private float closeThreshold = 0.2f;
  [SerializeField] private float waitDelay = 2.0f;
  [SerializeField] private Transform[] movePoints;

  private Vector3 targetPos = Vector3.zero;
  private Vector3 startPos;
  private float firstSpeed;
  private void OnTriggerEnter(Collider other) => OnTargetHit();

  IEnumerator Start()
  {
    startPos = transform.position;
    firstSpeed = moveSpeed;
    moveSpeed = 0;

    if (!isMoveable)
      yield return null;

    while (true)
    {
      targetPos = transform.position;
      // do not move
      yield return new WaitForSeconds(waitDelay);
      // choose new location
      targetPos = movePoints[UnityEngine.Random.Range(0, movePoints.Length)].position;
      // wait until we arrive
      while ((transform.position - targetPos).magnitude > closeThreshold)
      {
        yield return null;
      }
    }
  }

  private void OnEnable()
  {
    GameWonState.OnGameWon += ResetPositionAndStop;
    GameLostState.OnGameLost += ResetPositionAndStop;
    ResetState.OnReset += RestartMovement;
    GamePlayState.OnGamePlayActivated += GamePlay;
  }
  private void OnDisable()
  {
    GameWonState.OnGameWon -= ResetPositionAndStop;
    GameLostState.OnGameLost -= ResetPositionAndStop;
    ResetState.OnReset -= RestartMovement;
    GamePlayState.OnGamePlayActivated -= GamePlay;
  }
  void Update()
  {
    if (!isMoveable)
      return;

    if ((transform.position - targetPos).magnitude > closeThreshold)
    {
      transform.position += (targetPos - transform.position).normalized * moveSpeed * Time.deltaTime;
    }
  }
  private void ResetPositionAndStop()
  {
    moveSpeed = 0;
    transform.position = startPos;
    targetPos = movePoints[UnityEngine.Random.Range(0, movePoints.Length)].position;
  }
  private void RestartMovement()
  {
    moveSpeed = firstSpeed;
  }

  private void GamePlay(bool isActive)
  {
    if (!isActive) return;
    moveSpeed = firstSpeed;

  }
}
