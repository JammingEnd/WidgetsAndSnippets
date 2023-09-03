using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    /// <summary>
    /// how does it work
    /// 1. make a bloonGroupDef script. object, fill it in. like how round 63 has like 20 cerams with very low interval
    /// 2. 
    /// </summary>




    public List<BloonWaveDef> WaveDefs;
    public PathNode _startPosition;

   
    private Dictionary<int, int> _bloonCounts = new Dictionary<int, int>();
  

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWave(WaveDefs[0]);
        }
    }
    public void StartWave(BloonWaveDef wave)
    {
        
        foreach (var group in wave.bloonGroupings)
        {
                StartCoroutine(StartGroupWithInterval(group)); 
        }
    }
    private IEnumerator StartGroupWithInterval(BloonGroupingDef bloonGrouping)
    {
        if (bloonGrouping.SpawnAfter > 0)
        {
            yield return new WaitForSeconds(bloonGrouping.SpawnAfter);
        }
      
        SpawnGroup(bloonGrouping);
    }

    private void SpawnGroup(BloonGroupingDef bloonGroup)
    {
        _bloonCounts.Add(_bloonCounts.Count + 1, bloonGroup.BloonCount);
        int newCountInst = _bloonCounts.Count;
        StartCoroutine(SpawnBloonGroup(bloonGroup, newCountInst));
    }

    private IEnumerator SpawnBloonGroup(BloonGroupingDef bloonGroup, int bloonCountIndex)
    {
        for (int i = 0; i < bloonGroup.BloonCount + 1; i++)
        {
            
            if (_bloonCounts.TryGetValue(bloonCountIndex, out int currentBloonCount))
            {
                if (currentBloonCount > 0)
                {
                    SpawnBloon(bloonGroup.BloonType);
                    _bloonCounts[bloonCountIndex] = _bloonCounts[bloonCountIndex] - 1;
                    yield return new WaitForSeconds(bloonGroup.SpawnInterval);
                }
                else
                {
                    
                }

            }
        }
       
    }

    private void SpawnBloon(GameObject bloon)
    {
      
       var NewBloon = Instantiate(bloon, _startPosition.transform.position, _startPosition.transform.rotation);
       if(NewBloon.TryGetComponent(out BloonStats bloonStats))
       {
            bloonStats.StartNode = _startPosition;
       }
    }
}
