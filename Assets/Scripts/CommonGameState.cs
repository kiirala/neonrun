using UnityEngine;

public class CommonGameState : MonoBehaviour
{
    public GameObject CrashedOverlay;

    public bool Crashed { get; private set; }
    public bool Grazing { get; set; }
    public bool Focused { get; set; }

    private ObstaclesController obstacles;

    void Start()
    {
        obstacles = GetComponentInChildren<ObstaclesController>();
    }

    public void Crash()
    {
        Crashed = true;
        CrashedOverlay.SetActive(true);
    }

    public void Bomb()
    {
        if (!Crashed) obstacles.Bomb();
    }

    public void Restart()
    {
        Crashed = false;
        Grazing = false;
        Focused = false;
        obstacles.Restart();
        GetComponent<ScoreTracker>().Restart();
        GetComponentInChildren<ShipController>().Restart();
        CrashedOverlay.SetActive(false);
    }
}
