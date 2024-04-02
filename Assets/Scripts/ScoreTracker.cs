using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int ScorePerNominalSecond;
    public float GrazeMultiplier;
    public float FocusedMultiplier;

    public int Score { get; private set; }

    private CommonGameState state;
    private GameTime time;
    private float fractionalScore;

    void Start()
    {
        state = GetComponent<CommonGameState>();
        time = GetComponent<GameTime>();

        fractionalScore = 0;
    }

    void FixedUpdate()
    {
        if (!state.Crashed)
        {
            var toAdd = Time.fixedDeltaTime * ScorePerNominalSecond;
            if (state.Grazing) toAdd *= GrazeMultiplier;
            if (state.Focused) toAdd *= FocusedMultiplier;
            toAdd += fractionalScore;
            var intScore = (int)toAdd;
            Score += intScore;
            fractionalScore = toAdd - intScore;
        }
    }
}
