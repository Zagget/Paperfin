using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositionOnly : MonoBehaviour
{
    [SerializeField] private Transform target; // The object to follow
    [SerializeField] private Vector3 offset = Vector3.zero; // Optional offset to maintain while following

    void Update()
    {
        if (target != null)
        {
            // Update the position to match the target's position, keeping the offset
            transform.position = target.position + offset;
        }
        else
        {
            Debug.LogWarning("Target is not assigned!");
        }
    }
}
