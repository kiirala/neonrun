using UnityEngine;

public class SingleObstacleController : MonoBehaviour
{
    public float HitboxRadius;
    public float VisibleRadius;

    public int Lane { get; private set; }
    public float BoardYPosition { get; private set; }

    private BoardConfiguration board;
    private float initialPosition;
    private float timeSpawned;

    public bool IsInView
    {
        get => transform.localPosition.y + VisibleRadius > board.InvisibleAfterCoordinate;
    }

    private float LaneZeroToTop { get => board.LaneTopCoordinate - board.LaneZeroCoordinate; }

    void Awake()
    {
        board = GetComponentInParent<BoardConfiguration>();    
    }

    public void Initialize(int lane, float positionAboveTop, float currentTime)
    {
        Lane = lane;
        initialPosition = board.GameAreaHeight + positionAboveTop;
        BoardYPosition = initialPosition;
        timeSpawned = currentTime;
    }

    public void UpdatePosition(float currentTime)
    {
        var elapsed = currentTime - timeSpawned;
        var fractionalPosition = elapsed / board.NominalFallSeconds;
        BoardYPosition = initialPosition - board.GameAreaHeight * fractionalPosition;
        var y = board.LaneZeroCoordinate + LaneZeroToTop / board.GameAreaHeight * BoardYPosition;
        transform.localPosition = new((Lane - board.ZeroXPositionLane) * board.LaneSpacing, y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HitboxRadius);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, VisibleRadius);
    }
}
