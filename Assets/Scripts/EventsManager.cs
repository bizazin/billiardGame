using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action OnBallKicked;
    public static Action OnBallStopped;

    public static Action OnGameOver;
    public static Action OnGameWin;
}

