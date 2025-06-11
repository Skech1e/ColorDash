using System.Collections.Generic;
using UnityEngine;

public class GroundPoolManager : MonoBehaviour
{
    [SerializeField]
    Transform GroundTile;
    [SerializeField]
    GameEvents events;

    Vector2 startPos;
    float playerX;
    public float maxDelta;

    public List<Transform> poolList = new();
    public List<Vector2> poolPos = new();
    int headIndex, tailIndex;

    public bool isGameOver;

    private void Awake()
    {
        InitializePool();
    }

    private void OnEnable()
    {
        events.RestartGame += Reset;
        events.PlayerNoEntry += SetGameOverStatus;
    }
    private void OnDisable()
    {
        events.RestartGame -= Reset;
        events.PlayerNoEntry -= SetGameOverStatus;
    }

    private void Start()
    {
        startPos = Player.instance.transform.position;
        playerX = startPos.x;
    }
    void InitializePool()
    {
        int poolSize = transform.childCount;
        for (int i = 0; i < poolSize; i++)
        {
            poolList.Add(transform.GetChild(i));
            poolPos.Add(transform.GetChild(i).localPosition);
        }
        headIndex = 0;
        tailIndex = poolList.Count - 1;
        maxDelta = GroundTile.localScale.x;
    }

    private void Update()
    {
        if (!isGameOver)
            PoolCycle();
    }

    void PoolCycle()
    {
        Transform player = Player.instance.transform;
        float delta = player.position.x - playerX;
        float targetPos = 0f;
        while (delta > maxDelta)
        {
            tailIndex = (headIndex + poolList.Count - 1) % poolList.Count;
            targetPos = poolList[tailIndex].localPosition.x + maxDelta;
            poolList[headIndex].localPosition = Vector2.right * targetPos;
            headIndex = (headIndex + 1) % poolList.Count;
            playerX += maxDelta;
            delta = player.position.x - playerX;
        }
    }

    void SetGameOverStatus() => isGameOver = !isGameOver;

    private void Reset()
    {
        headIndex = 0;
        tailIndex = poolList.Count - 1;
        for (int i = 0; i < poolList.Count; i++)
            poolList[i].localPosition = poolPos[i];
        playerX = startPos.x;
        SetGameOverStatus();
    }
}
