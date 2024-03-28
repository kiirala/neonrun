using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    private ScoreTracker tracker;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        tracker = GetComponentInParent<ScoreTracker>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var score = string.Format("{0:d6}", tracker.Score);
        text.text = score;
    }
}
