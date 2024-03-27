using UnityEngine;

public class SingleObstacleController : MonoBehaviour
{
    public float LaneTopCoordinate;
    public float LaneBottomCoordinate;
    public float NominalFallSeconds;

    public float LaneSpacing = 0.8f;
    public int ZeroXPositionLane = 3;

    public int Lane { get; private set; }
    private float timeSpawned;
    private float latestElapsedTime;

    public float YPos { get => transform.localPosition.y; }

    public void Initialize(int lane, float currentTime)
    {
        Lane = lane;
        timeSpawned = currentTime;
        latestElapsedTime = 0;
    }

    public void UpdatePosition(float currentTime)
    {
        var elapsed = currentTime - timeSpawned;
        latestElapsedTime = elapsed;
        var fractionalPosition = elapsed / NominalFallSeconds;
        var position =
            LaneTopCoordinate + fractionalPosition * (LaneBottomCoordinate - LaneTopCoordinate);
        transform.localPosition = new((Lane - ZeroXPositionLane) * LaneSpacing, position);
    }

    public bool IsInView() => latestElapsedTime <= NominalFallSeconds;

}
