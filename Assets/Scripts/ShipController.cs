using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int MinLane = 1;
    public int MaxLane = 5;

    public int ZeroXPositionLane = 3;
    public float LaneSpacing = 0.8f;

    public float HitboxYPosition;

    public int Lane { get; private set; }
    public bool Crashed { get; set; }

    void Start()
    {
        Lane = ZeroXPositionLane;
    }

    public void ChangeLane(int direction)
    {
        if (Crashed) return;

        if (direction > 0 && Lane < MaxLane)
        {
            Lane++;
        }
        if (direction < 0 && Lane > MinLane)
        {
            Lane--;
        }
        transform.localPosition =
            new Vector3((Lane - ZeroXPositionLane) * LaneSpacing, transform.localPosition.y);
    }

    public float HitboxYCenter { get => transform.localPosition.y + HitboxYPosition; }
}
