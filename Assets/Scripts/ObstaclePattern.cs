using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ObstaclePattern
{
    public readonly int Height;
    public readonly int Level;
    public readonly List<(int, float)> Obstacles;

    public ObstaclePattern(int level, List<(int, float)> obstacles)
    {
        Level = level;
        Obstacles = obstacles;
        Height = (int)Math.Ceiling(obstacles.Select(o => o.Item2).Max());
    }

    public static List<ObstaclePattern> ReadFrom(TextAsset text)
    {
        List<ObstaclePattern> patterns = new();
        var lines = text.text.Split('\n')
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l));

        int level = 1;
        int lineCount = 0;
        List<(int, float)> obstacles = null;

        foreach(var line in lines)
        {
            if (int.TryParse(line, out var nextPatternLevel))
            {
                if (obstacles is not null && obstacles.Count > 0)
                {
                    obstacles = obstacles.Select(o => (o.Item1, o.Item2 + lineCount)).ToList();
                    patterns.Add(new ObstaclePattern(level, obstacles));
                }
                level = nextPatternLevel;
                obstacles = new();
                lineCount = 0;
            }
            else
            {
                if (line.Length != 5)
                {
                    throw new InvalidDataException(
                        $"Expected a line of five characters, got \"{line}\" instead");
                }
                for (int i = 0; i < 5; ++i)
                {
                    if (line[i] == 'x') obstacles.Add((i + 1, -lineCount));
                }
                lineCount++;
            }
        }

        if (obstacles is not null && obstacles.Count > 0)
        {
            obstacles = obstacles.Select(o => (o.Item1, o.Item2 + lineCount)).ToList();
            patterns.Add(new ObstaclePattern(level, obstacles));
        }

        return patterns;
    }
}