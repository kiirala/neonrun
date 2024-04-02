using UnityEngine;

public class GameTime : MonoBehaviour
{
    public float FocusedMultiplier;

    public float Seconds { get; private set; }
    public float DeltaSeconds { get; private set; }

    private CommonGameState state;

    void Start()
    {
        Seconds = 0;
        state = GetComponent<CommonGameState>();
    }

    void Update()
    {
        if (state.Crashed) return;
        var deltaTime = Time.deltaTime;
        if (state.Focused) deltaTime *= FocusedMultiplier;
        DeltaSeconds = deltaTime;
        Seconds += deltaTime;
    }
}
