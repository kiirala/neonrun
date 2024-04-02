using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int ScorePerNominalSecond;
    public float GrazeMultiplier;

    public int Score { get; private set; }

    private CommonGameState state;
    private float fractionalScore;

    void Start()
    {
        state = GetComponent<CommonGameState>();
        fractionalScore = 0;
    }

    void Update()
    {
        if (!state.Crashed)
        {
            var toAdd = Time.deltaTime * ScorePerNominalSecond + fractionalScore;
            if (state.Grazing) toAdd *= GrazeMultiplier;
            var intScore = (int)toAdd;
            Score += intScore;
            fractionalScore = toAdd - intScore;
        }
    }
}
