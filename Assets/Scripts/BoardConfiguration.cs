using UnityEngine;

public class BoardConfiguration : MonoBehaviour
{
    public float LaneTopCoordinate;
    public float LaneZeroCoordinate;
    public float InvisibleAfterCoordinate;

    public float GameAreaHeight = 10;
    public float NominalFallSeconds;

    public int MinLane = 1;
    public int MaxLane = 5;

    public float LaneSpacing = 0.8f;
    public int ZeroXPositionLane = 3;

    void OnDrawGizmos()
    {
        var lanex = new float[MaxLane - MinLane + 1];
        for (int i = MinLane; i <= MaxLane; i++)
        {
            lanex[i - MinLane] = (i - ZeroXPositionLane) * LaneSpacing;
        }
        var borderXMin = lanex[0] - LaneSpacing / 2;
        var borderXMax = lanex[^1] + LaneSpacing / 2;

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(
            transform.TransformPoint(borderXMin, LaneZeroCoordinate, 0),
            transform.TransformPoint(borderXMax, LaneZeroCoordinate, 0));

        var lanelines = new Vector3[(MaxLane - MinLane + 1) * 2];
        for (int lane = MinLane; lane <= MaxLane; lane++)
        {
            var i = lane - MinLane;
            lanelines[i * 2] = transform.TransformPoint(lanex[i], LaneTopCoordinate, 0);
            lanelines[i * 2 + 1] = transform.TransformPoint(lanex[i], InvisibleAfterCoordinate, 0);
        }
        Gizmos.DrawLineList(lanelines);

        var boundingbox = new Vector3[4]
        {
            new(borderXMin, LaneTopCoordinate),
            new(borderXMax, LaneTopCoordinate),
            new(borderXMax, InvisibleAfterCoordinate),
            new(borderXMin, InvisibleAfterCoordinate),
        };
        for (int i = 0; i < boundingbox.Length; i++)
        {
            boundingbox[i] = transform.TransformPoint(boundingbox[i]);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLineStrip(boundingbox, true);


    }
}
