using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public UnityAction OnLevelCompleted;

    [SerializeField] private Shard[] shards;
    [SerializeField] private MatchPicker matchPicker;

#if UNITY_EDITOR
    public void OnValidate()
    {
        //Search for Shards in children
        if (shards != null && shards.Length == 0) shards = GetComponentsInChildren<Shard>();
    }
#endif

    private void CheckForWin()
    {
        foreach (Shard shard in shards)
        {
            if (!shard.IsMatched)
            {
                return;
            }
        }
        OnLevelCompleted?.Invoke();
    }

    private void OnEnable()
    {
        matchPicker.OnMatched += CheckForWin;
    }

    private void OnDisable()
    {
        matchPicker.OnMatched -= CheckForWin;
    }
}
