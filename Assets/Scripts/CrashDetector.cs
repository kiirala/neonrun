using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    public float HitboxRadius;

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
        var y = ship.HitboxYCenter;
        var colliding = obstacles.GetInRange(ship.Lane, y - HitboxRadius, y + HitboxRadius);
        if (colliding.Count() > 0)
        {
            obstacles.Crashed = true;
            ship.Crashed = true;
        }
    }
}
