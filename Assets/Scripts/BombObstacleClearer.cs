using UnityEngine;

public class BombObstacleClearer : MonoBehaviour
{
    public float AnimationTimeSeconds;
    public float VisibleSize;
    public float[] ClearAtHeight;
    public ObstaclesController Obstacles;

    private SpriteRenderer spriteRenderer;
    private GameTime time;
    private CommonGameState gameState;
    private BombController controller;
    private BoardConfiguration boardConfiguration;

    private float triggeredTime;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        time = GetComponentInParent<GameTime>();
        gameState = GetComponentInParent<CommonGameState>();
        controller = GetComponentInParent<BombController>();
        boardConfiguration = GetComponentInParent<BoardConfiguration>();

        triggeredTime = float.MinValue;
        spriteRenderer.enabled = false;
    }

    void OnEnable()
    {
        GetComponentInParent<CommonGameState>().OnRestart += HandleRestart;
        GetComponentInParent<BombController>().OnBombActivated += HandleBombActivated;
    }

    void OnDisable()
    {
        gameState.OnRestart -= HandleRestart;
        controller.OnBombActivated -= HandleBombActivated;        
    }

    void Update()
    {
        if (time.Seconds <= triggeredTime + AnimationTimeSeconds)
        {
            float bottom = boardConfiguration.InvisibleAfterCoordinate - VisibleSize;
            float top = boardConfiguration.LaneTopCoordinate + VisibleSize;
            var frac = (time.Seconds - triggeredTime) / AnimationTimeSeconds;
            var pos = bottom + (top - bottom) * frac;
            transform.localPosition = new(0, pos, 0);

            var centerBoardHeight = (pos - boardConfiguration.LaneZeroCoordinate) /
                (boardConfiguration.LaneTopCoordinate - boardConfiguration.LaneZeroCoordinate) *
                boardConfiguration.GameAreaHeight;
            var worldToBoardScale =
                boardConfiguration.GameAreaHeight /
                (boardConfiguration.LaneTopCoordinate - boardConfiguration.LaneZeroCoordinate);
            for (int i = 0; i < ClearAtHeight.Length; i++)
            {
                var boardPos = centerBoardHeight + ClearAtHeight[i] * worldToBoardScale;
                Obstacles.RemoveObstaclesBelow(i + boardConfiguration.MinLane, boardPos);
            }
        }
    }

    void HandleBombActivated()
    {
        triggeredTime = time.Seconds;
        spriteRenderer.enabled = true;
    }

    private void HandleRestart()
    {
        triggeredTime = float.MinValue;
        spriteRenderer.enabled = false;
    }

    void OnDrawGizmos()
    {
        boardConfiguration = GetComponentInParent<BoardConfiguration>();
        if (ClearAtHeight.Length != boardConfiguration.MaxLane - boardConfiguration.MinLane + 1)
        {
            Debug.Log($"Wrong number of lanes: have {ClearAtHeight.Length} " +
                $"but board is from {boardConfiguration.MinLane} to {boardConfiguration.MaxLane}");
            return;
        }
        var lines = new Vector3[ClearAtHeight.Length * 2];
        for (int i = 0; i < ClearAtHeight.Length; ++i)
        {
            var laneNr = i + boardConfiguration.MinLane;
            var laneNrShift = laneNr - boardConfiguration.ZeroXPositionLane;
            var laneCoordShift = laneNrShift * boardConfiguration.LaneSpacing;
            var r = boardConfiguration.LaneSpacing / 2;
            var h = ClearAtHeight[i];
            lines[i * 2] = transform.TransformPoint(laneCoordShift - r, h, 0);
            lines[i * 2 + 1] = transform.TransformPoint(laneCoordShift + r, h, 0);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLineList(lines);
    }
}
