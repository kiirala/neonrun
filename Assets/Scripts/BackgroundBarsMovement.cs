using UnityEngine;

public class BackgroundBarsMovement : MonoBehaviour
{
    public float RelativeSpeedMultiplier;

    private GameTime time;
    private BoardConfiguration board;

    // Start is called before the first frame update
    void Start()
    {
        time = GetComponentInParent<GameTime>();
        board = GetComponentInParent<BoardConfiguration>();    
    }

    // Update is called once per frame
    void Update()
    {
        var boardZeroToTop = board.LaneTopCoordinate - board.LaneZeroCoordinate;
        var fullBoardsFallen = time.Seconds / board.NominalFallSeconds;
        var movement = fullBoardsFallen * boardZeroToTop * RelativeSpeedMultiplier;
        var frac = movement - (int)movement;
        transform.localPosition = new Vector3(0, -frac);
    }
}
