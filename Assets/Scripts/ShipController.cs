using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float HitboxYPosition;
    public float HitboxRadius;
    public float VisibleRadius;

    public int Lane { get; private set; }

    private BoardConfiguration board;
    private CommonGameState state;

    void Start()
    {
        board = GetComponentInParent<BoardConfiguration>();
        state = GetComponentInParent<CommonGameState>();
        Lane = board.ZeroXPositionLane;
    }

    public void ChangeLane(int direction)
    {
        if (state.Crashed) return;

        if (direction > 0 && Lane < board.MaxLane)
        {
            Lane++;
        }
        if (direction < 0 && Lane > board.MinLane)
        {
            Lane--;
        }
        UpdateSpritePosition();
    }

    public float HitboxYCenter { get => transform.localPosition.y + HitboxYPosition; }

    public void Restart()
    {
        Lane = board.ZeroXPositionLane;
        UpdateSpritePosition();
    }

    void OnDrawGizmos()
    {
        var center = transform.position + new Vector3(0, HitboxYPosition);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, HitboxRadius);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(center, VisibleRadius);
    }

    private void UpdateSpritePosition()
    {
        transform.localPosition = new Vector3(
            (Lane - board.ZeroXPositionLane) * board.LaneSpacing,
            transform.localPosition.y);
    }
}
