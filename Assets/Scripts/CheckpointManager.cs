using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private List<Checkpoint> _checkpoints;
    private Vector3 _lastCheckpoint = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        _checkpoints = new List<Checkpoint>();
        
        // Get every checkpoint as children of this object
        foreach (Transform child in transform)
        {
            var checkpoint = child.GetComponent<Checkpoint>();
            
            if (checkpoint != null)
            {
                _checkpoints.Add(checkpoint);
            }
        }
    }

    public void SetLastCheckpoint(Transform checkpoint)
    {
        _lastCheckpoint = checkpoint.position;

        UpdateCheckpoints();
    }
    
    public Vector3 GetLastCheckpoint()
    {
        return _lastCheckpoint;
    }
    
    private void UpdateCheckpoints()
    {
        foreach (var checkpoint in _checkpoints)
        {
            if (_lastCheckpoint.Equals(checkpoint.transform.position))
            {
                checkpoint.SetCheckpointActive(true);
            }
            else
            {
                checkpoint.SetCheckpointActive(false);
            }
        }
    }
}
