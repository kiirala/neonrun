using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int MinLane = 1;
    public int MaxLane = 5;

    public int ZeroXPositionLane = 3;
    public float LaneSpacing = 0.8f;

    private int lane;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        lane = ZeroXPositionLane;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeLane(int direction)
    {
        if (direction > 0 && lane < MaxLane)
        {
            lane++;
        }
        if (direction < 0 && lane > MinLane)
        {
            lane--;
        }
        transform.localPosition = new Vector3((lane - ZeroXPositionLane) * LaneSpacing, transform.localPosition.y);
    }
}
