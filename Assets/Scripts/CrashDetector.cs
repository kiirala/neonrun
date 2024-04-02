using System.Linq;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    private ObstaclesController obstacles;
    private ShipController ship;
    private CommonGameState state;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = GetComponentInChildren<ObstaclesController>();
        ship = GetComponentInChildren<ShipController>();
        state = GetComponent<CommonGameState>();
    }

    // Update is called once per frame
    void Update()
    {
        var colliding = obstacles.GetNearZero(ship.Lane, ship.HitboxRadius);
        if (colliding.Count() > 0)
        {
            state.Crash();
        }
    }
}
