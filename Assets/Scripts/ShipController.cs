using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float HitboxYPosition;
    public float HitboxRadius;

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
        transform.localPosition = new Vector3(
            (Lane - board.ZeroXPositionLane) * board.LaneSpacing,
            transform.localPosition.y);
    }

    public float HitboxYCenter { get => transform.localPosition.y + HitboxYPosition; }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, HitboxYPosition), HitboxRadius);
    }
}
