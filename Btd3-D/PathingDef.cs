using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingDef : MonoBehaviour
{
    private float _baseSpeed, _currentSpeed;

    private PathNode _targetNode;

    public void IniSetStats(float baseStat, PathNode node)
    {
        _baseSpeed = baseStat;
        _currentSpeed = _baseSpeed;
        _targetNode = node.NextNode;
        this.transform.LookAt(_targetNode.transform);
    }

    public void SetSpeed(float multiplier)
    {
        _currentSpeed -= _baseSpeed * multiplier;
    }

    private void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _targetNode.transform.position, _currentSpeed * 0.1f);
    }

    private void Update()
    {
        if(_targetNode.NextNode == null)
        {
            kill();
        }
        if(_targetNode != null)
        {
            var distance = Vector3.Distance(this.transform.position, _targetNode.transform.position);

            if (distance < 0.2)
            {
                _targetNode = _targetNode.NextNode;
                Vector3 nodePos = new Vector3(_targetNode.transform.position.x, 0, _targetNode.transform.position.z);
                this.transform.LookAt(nodePos);
            }
        }
        
    }

    private void kill()
    {
        Destroy(this.gameObject);
    }
}
