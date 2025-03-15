using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField] private List<Shard> adjacentShards;
    [SerializeField] private GameObject compositeShard;
    private MoveUtils moveUtils;

    private bool isInEndPosition = false;

    public List<Shard> GetAdjacentShards() => adjacentShards;

    public void Awake()
    {
        moveUtils = new MoveUtils();
        gameObject.tag = "Interactable";
        if (compositeShard == null)
        {
            Debug.LogError("Composite shard not set for " + name);
            return;
        }
        compositeShard.SetActive(false);
    }

    public void ShardMatched()
    {
        if (isInEndPosition) return;
        isInEndPosition = true;
        // Move to composite shard position
        StartCoroutine(moveUtils.MoveToPosition(transform, compositeShard.transform.position, compositeShard.transform.rotation, 5f, rotate: true));
    }

}
