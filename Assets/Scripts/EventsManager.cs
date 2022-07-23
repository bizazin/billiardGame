using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<Vector3> OnDirectionChoosed;
    public static Action OnBallCicked;
}
