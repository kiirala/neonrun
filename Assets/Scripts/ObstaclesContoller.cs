using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public int SecondsPerObstacleLevel;
    public float SpacingBetweenPatterns;
    public SingleObstacleController ObstaclePrefab;
    public TextAsset[] ObstaclePatterns;

    private CommonGameState state;
    private GameTime time;
    private BoardConfiguration boardConfiguration;

    private float nextSpawnTime;

    private readonly List<SingleObstacleController> activeObstacles = new();
    private readonly List<SingleObstacleController> inactiveObstacles = new();

    private readonly Dictionary<int, List<ObstaclePattern>> patterns = new();

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponentInParent<CommonGameState>();
        time = GetComponentInParent<GameTime>();
        boardConfiguration = GetComponentInParent<BoardConfiguration>();
        inactiveObstacles.AddRange(GetComponentsInChildren<SingleObstacleController>());
        ReadPatterns();
        nextSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state.Crashed) return;

        if (time.Seconds >= nextSpawnTime)
        {
            var pattern = PickObstaclePattern();
            pattern.Obstacles.ForEach(o => SpawnObstacle(o.Item1, o.Item2));
            nextSpawnTime = time.Seconds +
                boardConfiguration.SingleLineSeconds * (pattern.Height + SpacingBetweenPatterns);
        }

        activeObstacles.ForEach(o => { o.UpdatePosition(time.Seconds); });
        int i = 0;
        while (i < activeObstacles.Count)
        {
            if (!activeObstacles[i].IsInView)
            {
                activeObstacles[i].gameObject.SetActive(false);
                inactiveObstacles.Add(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public IEnumerable<SingleObstacleController> GetHitboxNearZero(int lane, float range)
        => activeObstacles.Where(
            o => o.Lane == lane &&
            o.BoardYPosition + o.HitboxRadius >= -range &&
            o.BoardYPosition - o.HitboxRadius <= range);

    public IEnumerable<SingleObstacleController> GetVisibleNearZero(int lane, float range)
        => activeObstacles.Where(
            o => o.Lane == lane &&
            o.BoardYPosition + o.VisibleRadius >= -range &&
            o.BoardYPosition - o.VisibleRadius <= range);
    
    public void RemoveObstaclesBelow(int lane, float height)
    {
        var toRemove = activeObstacles
            .Where(o => o.Lane == lane && o.BoardYPosition < height)
            .ToList();
        activeObstacles.RemoveAll(o => o.Lane == lane && o.BoardYPosition < height);
        toRemove.ForEach(o => o.gameObject.SetActive(false));
        inactiveObstacles.AddRange(toRemove);
    }

    public void Bomb()
    {
        // activeObstacles.ForEach(o => o.gameObject.SetActive(false));
        // inactiveObstacles.AddRange(activeObstacles);
        // activeObstacles.Clear();
        nextSpawnTime = time.Seconds +
            boardConfiguration.SingleLineSeconds * SpacingBetweenPatterns;
    }

    public void Restart()
    {
        activeObstacles.ForEach(o => o.gameObject.SetActive(false));
        inactiveObstacles.AddRange(activeObstacles);
        activeObstacles.Clear();
        nextSpawnTime = 0;
    }

    private void SpawnObstacle(int lane, float height)
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
        obstacle.gameObject.SetActive(true);
        obstacle.Initialize(lane, height, time.Seconds);
        activeObstacles.Add(obstacle);
    }

    private void ReadPatterns()
    {
        foreach (var f in ObstaclePatterns)
        {
            var filePatterns = ObstaclePattern.ReadFrom(f);
            foreach (var p in filePatterns)
            {
                if (!patterns.ContainsKey(p.Level)) patterns.Add(p.Level, new());
                patterns[p.Level].Add(p);
            }
        }
    }

    private ObstaclePattern PickObstaclePattern()
    {
        var level = (int)(time.Seconds / SecondsPerObstacleLevel + 1);
        while (!patterns.ContainsKey(level)) level--;
        while (level > 1 && UnityEngine.Random.value > 0.5) level--;
        var list = patterns[level];
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
