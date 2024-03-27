using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesContoller : MonoBehaviour
{
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
        activeObstacles.ForEach(o =>
        {
            o.UpdatePosition(Time.time);
            if (!o.IsInView()) o.Initialize(3, Time.time);
        });
    }
}
