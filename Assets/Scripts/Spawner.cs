using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameEvents events;
    public List<Gate> gateList = new();
    private List<Vector2> gateDefaultPos = new();
    public float gap;
    int headIndex, tailIndex;

    private void Awake()
    {
        headIndex = 0;
        tailIndex = gateList.Count-1;
        RecordGateDefaultPos();
    }
    private void OnEnable()
    {
        events.PlayerPassedGate += SpawnGate;
        events.RestartGame += ResetGates;
    }


    private void OnDisable()
    {
        events.PlayerPassedGate -= SpawnGate;
        events.RestartGame -= ResetGates;
    }

    void RecordGateDefaultPos()
    {
        for (int i = 0; i < gateList.Count; i++)
            gateDefaultPos.Add(gateList[i].transform.position);
    }

    private void SpawnGate()
    {
        StartCoroutine(GatePoolCycle());
    }

    IEnumerator GatePoolCycle()
    {
        float distance = Random.Range(12f, 24f);
        tailIndex = (headIndex + gateList.Count - 1) % gateList.Count;
        float targetPos = gateList[tailIndex].transform.localPosition.x + distance;
        yield return new WaitForSeconds(1.5f);
        gateList[headIndex].SetGateColor();
        gateList[headIndex].transform.localPosition = Vector2.right * targetPos;
        headIndex = (headIndex + 1) % gateList.Count;
    }

    void ResetGates()
    {
        headIndex = 0;
        for(int i = 0; i < gateList.Count;i++)
            gateList[i].transform.position = gateDefaultPos[i];
    }
}
