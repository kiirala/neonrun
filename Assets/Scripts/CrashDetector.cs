using System;
using System.Linq;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    public float InvulnerabilitySeconds;

    private ObstaclesController obstacles;
    private ShipController ship;
    private CommonGameState state;
    private GameTime time;
    private BombController bombController;

    private float invulnerabilityEndTime;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = GetComponentInChildren<ObstaclesController>();
        ship = GetComponentInChildren<ShipController>();
        state = GetComponent<CommonGameState>();
        time = GetComponent<GameTime>();
        bombController = GetComponent<BombController>();

        invulnerabilityEndTime = float.MinValue;
    }

    void OnEnable()
    {
        GetComponent<BombController>().OnBombActivated += BombActivated;
        GetComponent<CommonGameState>().OnRestart += HandleRestart;
    }

    void OnDisable()
    {
        bombController.OnBombActivated -= BombActivated;
        state.OnRestart -= HandleRestart;
    }

    // Update is called once per frame
    void Update()
    {
        var colliding = obstacles.GetHitboxNearZero(ship.Lane, ship.HitboxRadius);
        if (colliding.Count() > 0 && time.Seconds > invulnerabilityEndTime)
        {
            state.Crash();
        }
        var grazes = obstacles.GetVisibleNearZero(ship.Lane, ship.VisibleRadius);
        state.Grazing = grazes.Count() > 0;
    }

    private void BombActivated()
    {
        invulnerabilityEndTime = time.Seconds + InvulnerabilitySeconds;
    }

    private void HandleRestart()
    {
        invulnerabilityEndTime = float.MinValue;
    }
}
