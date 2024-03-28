using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesContoller : MonoBehaviour
{
    public SingleObstacleController ObstaclePrefab;

    private CommonGameState state;

    private readonly List<SingleObstacleController> activeObstacles = new();
    private readonly List<SingleObstacleController> inactiveObstacles = new();

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponentInParent<CommonGameState>();
        inactiveObstacles.AddRange(GetComponentsInChildren<SingleObstacleController>());
    }

    // Update is called once per frame
    void Update()
    {
        if (state.Crashed) return;

        if (activeObstacles.Count == 0)
        {
            SpawnObstacle(1, 1);
            SpawnObstacle(2, 3);
            SpawnObstacle(3, 2);
            SpawnObstacle(4, 3);
            SpawnObstacle(5, 1);
        }

        activeObstacles.ForEach(o => { o.UpdatePosition(Time.time); });
        int i = 0;
        while (i < activeObstacles.Count)
        {
            if (!activeObstacles[i].IsInView)
            {
                inactiveObstacles.Add(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public IEnumerable<SingleObstacleController> GetNearZero(int lane, float range)
        => activeObstacles.Where(
            o => o.Lane == lane &&
            o.BoardYPosition + o.HitboxRadius >= -range &&
            o.BoardYPosition - o.HitboxRadius <= range);

    private void SpawnObstacle(int lane, int height)
    {
        SingleObstacleController obstacle;
        if (inactiveObstacles.Count > 0)
        {
            obstacle = inactiveObstacles[^1];
            inactiveObstacles.RemoveAt(inactiveObstacles.Count - 1);
        }
        else
        {
            obstacle = Instantiate(ObstaclePrefab, transform);
        }
        obstacle.Initialize(lane, height, Time.time);
        activeObstacles.Add(obstacle);
    }

}
