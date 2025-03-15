using System.Collections.Generic;
using UnityEngine;

//
public class Shard : MonoBehaviour
{
    [SerializeField] private List<Shard> adjacentShards;
    [SerializeField] private GameObject compositeShard; // look for same name in onValidate
    private MoveUtils moveUtils;
    private Renderer shardRenderer;
    private int goldenHighlightLayer = 9;

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
        shardRenderer = GetComponent<Renderer>();
    }

    public void ShardMatched()
    {
        if (isInEndPosition) return;
        isInEndPosition = true;
        // Move to composite shard position
        StartCoroutine(moveUtils.MoveToPosition(transform, compositeShard.transform.position, compositeShard.transform.rotation, 5f, rotate: true));
    }

    public void HighlightGolden()
    {
        if ((shardRenderer.renderingLayerMask & (1 << goldenHighlightLayer)) == 0)
        {
            shardRenderer.renderingLayerMask |= (uint)(1 << goldenHighlightLayer);
        }
    }

    public void UnhighlightGolden()
    {
        shardRenderer.renderingLayerMask = 1 << 0;
    }

}
