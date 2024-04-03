using System;
using UnityEngine;

public class FlashingSparks : MonoBehaviour
{
    public float FlashDurationSeconds;
    public float RiseTimeFraction;

    private bool isFlashing;
    private float startTime;

    private GameTime time;
    private CommonGameState state;
    private SpriteRenderer spriteRenderer;

    private readonly Color transparent = new(1, 1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        time = GetComponentInParent<GameTime>();
        state = GetComponentInParent<CommonGameState>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = transparent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlashing && state.Grazing)
        {
            isFlashing = true;
            startTime = time.Seconds;
            spriteRenderer.flipX = UnityEngine.Random.value >= 0.5;
            spriteRenderer.flipY = UnityEngine.Random.value >= 0.5;
        }
        else if (isFlashing && !state.Grazing)
        {
            isFlashing = false;
            spriteRenderer.color = transparent;
        }
        if (isFlashing)
        {
            var flashRound = (time.Seconds - startTime) / FlashDurationSeconds;
            var fraction = flashRound - (int)flashRound;
            float alpha = 1;
            if (fraction < RiseTimeFraction)
            {
                fraction /= RiseTimeFraction;
                alpha = ((float)-Math.Cos(fraction * Math.PI) + 1) / 2;
            }
            else
            {
                fraction = (fraction - RiseTimeFraction) / (1 - RiseTimeFraction);
                alpha = ((float)Math.Cos(fraction * Math.PI) + 1) / 2;
            }
            spriteRenderer.color = new(1, 1, 1, alpha);
        }
    }
}
