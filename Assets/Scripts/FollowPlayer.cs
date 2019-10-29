using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("Follow Player")]
    [SerializeField] GameObject playerToFollow;
    [SerializeField] Vector3 offset = new Vector3(0f, 5f, -5f);
    
    // Update is called once per frame
    void Update()
    {
        float adjustedZ = (playerToFollow.transform.position.z + offset.z);
        transform.position = new Vector3(offset.x, offset.y, adjustedZ);
    }
}
