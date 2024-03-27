using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleObstacleController : MonoBehaviour
{
    public float LaneTopCoordinate;
    public float LaneBottomCoordinate;
    public float NominalFallSeconds;

    public float LaneSpacing = 0.8f;
    public int ZeroXPositionLane = 3;

    private int lane;
    private float timeSpawned;
    private float latestElapsedTime;

    public void Initialize(int lane, float currentTime)
    {
        this.lane = lane;
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
        transform.localPosition = new((lane - ZeroXPositionLane) * LaneSpacing, position);
    }

    public bool IsInView() => latestElapsedTime <= NominalFallSeconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
