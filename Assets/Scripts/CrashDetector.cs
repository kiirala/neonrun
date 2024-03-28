using System.Linq;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    private ObstaclesContoller obstacles;
    private ShipController ship;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = GetComponentInChildren<ObstaclesContoller>();
        ship = GetComponentInChildren<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        var colliding = obstacles.GetNearZero(ship.Lane, ship.HitboxRadius);
        if (colliding.Count() > 0)
        {
            obstacles.Crashed = true;
            ship.Crashed = true;
        }
    }
}
