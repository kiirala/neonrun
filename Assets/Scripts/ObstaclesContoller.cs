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
        activeObstacles.ForEach(o => o.Initialize(3, Time.time));
    }

    // Update is called once per frame
    void Update()
    {
        if (Crashed) return;
        
        activeObstacles.ForEach(o =>
        {
            o.UpdatePosition(Time.time);
            if (!o.IsInView()) o.Initialize(3, Time.time);
        });
    }

    public IEnumerable<SingleObstacleController> GetInRange(int lane, float ymin, float ymax)
        => activeObstacles.Where(o => o.Lane == lane && o.YPos >= ymin && o.YPos <= ymax);
}
