using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public event Action PlayerAtGate, PlayerPassedGate, PlayerNoEntry;
    public event Action RestartGame;

    public void RaisePlayerAtGate() => PlayerAtGate?.Invoke();
    public void RaisePlayerPassed() => PlayerPassedGate.Invoke();
    public void RaisePlayerNoEntry() => PlayerNoEntry?.Invoke();
    public void RaiseGameRestart() => RestartGame?.Invoke();
}
