using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MatchPicker : MonoBehaviour
{
    public UnityAction OnMatched;

    [SerializeField] private Inspector inspector;
    [SerializeField] private ClickDetector clickDetector;

    private Shard[] matches = new Shard[2];
    private bool checksMatches = false;

    private void MarkAsMatch(GameObject clickedObject)
    {
        if (inspector.isExamining) return;

        Shard clickedShard = clickedObject.GetComponent<Shard>();
        if (clickedShard == null || checksMatches) return;

        checksMatches = true;
        if (matches[0] == null) // select first shard
        {
            matches[0] = clickedShard;
            clickedShard.AddGoldenOutline();
            checksMatches = false;
        }
        else if (matches[0] == clickedShard) // deselect first shard
        {
            matches[0] = null;
            clickedShard.RemoveGoldenOutline();
            checksMatches = false;
        }
        else if (matches[0].GetAdjacentShards().Contains(clickedShard)) // match with first shard
        {
            clickedShard.AddGoldenOutline();
            matches[1] = clickedShard;
            StartCoroutine(Matched());
        }
        else // no match
        {
            matches[0].RemoveGoldenOutline(); 
            matches[0].AddRedOutline();
            matches[1] = clickedShard;
            matches[1].AddRedOutline();
            StartCoroutine(NoMatch());
        }
    }
    private IEnumerator Matched()
    {
        yield return new WaitForSeconds(1f);
        matches[0].ShardMatched();
        matches[1].ShardMatched();
        matches[0].RemoveGoldenOutline();
        matches[1].RemoveGoldenOutline();
        ResetShards();
        checksMatches = false;
        yield return new WaitForSeconds(3f);
        OnMatched?.Invoke();
    }
    private IEnumerator NoMatch()
    {
        yield return new WaitForSeconds(1f);
        matches[0].RemoveRedOutline();
        matches[1].RemoveRedOutline();
        ResetShards();
        checksMatches = false;
    }


    private void ResetShards()
    {
        matches[0] = null;
        matches[1] = null;
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
