using System;
using UnityEngine;

public class BombAura : MonoBehaviour
{
    public float DurationSeconds;
    public float ScalingStart;
    public float ScalingEnd;

    private BombController controller;
    private GameTime time;
    private SpriteRenderer spriteRenderer;
    private CommonGameState gameState;

    private float activationTime;

    private readonly Color transparent = new(1, 1, 1, 0);

    void Start()
    {
        controller = GetComponentInParent<BombController>();
        time = GetComponentInParent<GameTime>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameState = GetComponent<CommonGameState>();
        activationTime = float.MinValue;
    }

    void OnEnable()
    {
        GetComponentInParent<BombController>().OnBombActivated += HandleBombActivated;
        GetComponentInParent<CommonGameState>().OnRestart += HandleRestart;
    }

    void OnDisable()
    {
        controller.OnBombActivated -= HandleBombActivated;
        gameState.OnRestart -= HandleRestart;
    }

    void Update()
    {
        if (time.Seconds <= activationTime + DurationSeconds)
        {
            var elapsed = time.Seconds - activationTime;
            var factor = (float)Math.Sin(elapsed / DurationSeconds * Math.PI / 2);
            spriteRenderer.color = new(1, 1, 1, 1 - factor);
            var scale = ScalingStart + factor * (ScalingEnd - ScalingStart);
            spriteRenderer.transform.localScale = new(scale, scale, 1);
        }
        else
        {
            spriteRenderer.color = transparent;
        }
    }

    void HandleBombActivated()
    {
        activationTime = time.Seconds;
    }

    void HandleRestart()
    {
        activationTime = float.MinValue;
    }
}