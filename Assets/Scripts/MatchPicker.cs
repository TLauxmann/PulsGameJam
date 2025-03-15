using UnityEngine;

public class MatchPicker : MonoBehaviour
{
    [SerializeField] private Inspector inspector;
    [SerializeField] private ClickDetector clickDetector;

    private Shard[] matches = new Shard[2];

    private void MarkAsMatch(GameObject clickedObject)
    {
        if (inspector.isExamining) return;

        Shard clickedShard = clickedObject.GetComponent<Shard>();
        if (clickedShard == null) return;

        if (matches[0] == null)
        {
            matches[0] = clickedShard;
            Debug.Log("First shard selected");
            //TODO: Highlight the shard
        }
        else if (matches[0] == clickedShard)
        {
            matches[0] = null;
            Debug.Log("First shard deselected");
            //TODO: Unhighlight the shard
        }
        else if (matches[0].GetAdjacentShards().Contains(clickedShard))
        {
            Debug.Log("Match found!");
            matches[0].ShardMatched();
            matches[0] = null;
            clickedShard.ShardMatched();
        }
    }

    private void OnEnable()
    {
        clickDetector.OnMatchItem += MarkAsMatch;
    }

    private void OnDisable()
    {
        clickDetector.OnMatchItem -= MarkAsMatch;
    }


}
