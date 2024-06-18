using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform followingTarget;
    [Range(-1f, 1f)] public float parallaxStrength;
    public bool disableVerticalParallax;
    private Vector3 targetPreviousPosition;
    


    private void Awake()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }
        targetPreviousPosition = followingTarget.position;
    }

    private void Update()
    {
        Vector3 delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
            delta.y = 0f;

        targetPreviousPosition = followingTarget.position;

        Vector3 newPos = transform.position + delta * parallaxStrength;
        transform.position = newPos;
    }
}
