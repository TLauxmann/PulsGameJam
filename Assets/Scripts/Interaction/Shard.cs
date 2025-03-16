using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Shard : MonoBehaviour
{
    [SerializeField] private List<Shard> adjacentShards;
    [SerializeField] private GameObject compositeShard; // look for same name in onValidate
    [SerializeField] private bool lookForAdjacentShards = false;
    [SerializeField] private float adjacentShardRadius = 1.5f;
    private MoveUtils moveUtils;
    private Renderer shardRenderer;
    private int hoverHighlightLayer = 8;
    private int goldenHighlightLayer = 9;
    private int redHighlightLayer = 10;

    public bool IsMatched { get; private set; }


#if UNITY_EDITOR
    public void OnValidate()
    {
        if (compositeShard == null)
        {
            //Search for a game object with the same name to use as the composite shard (Editor only)
            GameObject[] allGameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            foreach (GameObject go in allGameObjects)
            {
                if (go.name == gameObject.name && go != gameObject)
                {
                    compositeShard = go;
                    break;
                }
            }
        }

        if (adjacentShards.Contains(this))
        {
            adjacentShards.Remove(this);
        }
        foreach (Shard shard in adjacentShards)
        {
            if(!shard.adjacentShards.Contains(this))
            {
                shard.adjacentShards.Add(this);
            }
        }
    }
#endif

    public List<Shard> GetAdjacentShards() => adjacentShards;

    public void Awake()
    {
        moveUtils = new MoveUtils();
        gameObject.tag = "Interactable";
        IsMatched = false;
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
        if (IsMatched) return;
        IsMatched = true;
        // Move to composite shard position
        StartCoroutine(moveUtils.MoveToPosition(transform, compositeShard.transform.position, compositeShard.transform.rotation, 5f, rotate: true));
    }

    private void AddOutline(int layer)
    {
        if ((shardRenderer.renderingLayerMask & (1 << layer)) == 0)
        {
            shardRenderer.renderingLayerMask |= (uint)(1 << layer);
        }
    }

    public void AddHoverOutline()
    {
        if ((shardRenderer.renderingLayerMask & ((1 << goldenHighlightLayer) | (1 << redHighlightLayer))) == 0)
        {
            AddOutline(hoverHighlightLayer);
        }
    }
    public void AddGoldenOutline()
    {
        RemoveHoverOutline();
        AddOutline(goldenHighlightLayer);
    }
    public void AddRedOutline()
    {
        RemoveHoverOutline();
        AddOutline(redHighlightLayer);
    }

    public void RemoveHoverOutline() { shardRenderer.renderingLayerMask &= (uint)~(1 << hoverHighlightLayer); }
    public void RemoveGoldenOutline() { shardRenderer.renderingLayerMask &= (uint)~(1 << goldenHighlightLayer); }
    public void RemoveRedOutline() { shardRenderer.renderingLayerMask &= (uint)~(1 << redHighlightLayer); }

}
