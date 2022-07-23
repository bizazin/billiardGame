using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleIcon : MonoBehaviour
{
    [SerializeField] private Sprite _circleSprite;
    public float SpriteRadius => _circleSprite.bounds.size.x / 2;

    private void OnEnable()
    {
        EventsManager.OnBallCicked += DisableIcon;
    }

    private void DisableIcon()
    {
        gameObject.SetActive(false);
    }

    public void DrawIcon(Vector3 direction)
    {
        transform.position = direction;
    }

    private void OnDisable()
    {
        EventsManager.OnBallCicked -= DisableIcon;
    }

}
