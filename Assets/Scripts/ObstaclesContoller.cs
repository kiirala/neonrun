using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesContoller : MonoBehaviour
{
    public bool Crashed { get; set; }

    private readonly List<SingleObstacleController> activeObstacles = new();

    // Start is called before the first frame update
    void Start()
    {
        activeObstacles.AddRange(GetComponentsInChildren<SingleObstacleController>());
        activeObstacles.ForEach(o => o.Initialize(3, 1.0f, Time.time));
    }

    // Update is called once per frame
    void Update()
    {
        if (Crashed) return;
        
        activeObstacles.ForEach(o =>
        {
            o.UpdatePosition(Time.time);
            if (!o.IsInView) o.Initialize(3, 1.0f, Time.time);
        });
    }

    public IEnumerable<SingleObstacleController> GetNearZero(int lane, float range)
        => activeObstacles.Where(
            o => o.Lane == lane &&
            o.BoardYPosition + o.HitboxRadius >= -range &&
            o.BoardYPosition - o.HitboxRadius <= range);
}
