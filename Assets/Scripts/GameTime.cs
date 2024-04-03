using UnityEngine;

public class GameTime : MonoBehaviour
{
    public float FocusedMultiplier;
    public float SpeedupFactor;

    public float Seconds { get; private set; }
    public float DeltaSeconds { get; private set; }

    private CommonGameState state;

    public void Restart()
    {
        Seconds = 0;
    }

    void Start()
    {
        Seconds = 0;
        state = GetComponent<CommonGameState>();
    }

    void Update()
    {
        if (state.Crashed) return;
        var deltaTime = Time.deltaTime;
        deltaTime *= 1 + Seconds * (SpeedupFactor - 1);
        if (state.Focused) deltaTime *= FocusedMultiplier;
        DeltaSeconds = deltaTime;
        Seconds += deltaTime;
    }
}
