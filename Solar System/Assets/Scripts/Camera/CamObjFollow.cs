using UnityEngine;
using System.Collections.Generic;

public class CamObjFollow : MonoBehaviour
{
    private Transform target; // The target object to follow
    private Transform secondTarget; // The second target object to follow
    public Vector3 offset;    // Offset from the target
    public float smoothSpeed = 0.125f; // Smoothness factor for movement
    public string targetName;
    public List<string> secondTargetNames; // List of second target names
    public Vector3 uiOffset;

    // Start is called before the first frame update
    void Start()
    {
        GetTarget(); // Get the target and the offset
    }

    // Update is called once per frame
    void Update()
    {
        GetTarget();
        if (target != null)
        {
            // Desired position
            Vector3 desiredPosition = target.position + offset + uiOffset;
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Smoothly rotate to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        }
    }

    void GetTarget()
    {
        GameObject targetObject = GameObject.Find(targetName);
        if (targetObject != null)
        {
            target = targetObject.transform;
            // Initialize the offset based on initial positions
            offset = new Vector3(target.transform.localScale.x, 0, 0);
            // round the offset up with no decimal
            if (offset.x < 1)
            {
                offset = new Vector3(1f, 0, 0);
            }
        }

        if (secondTargetNames != null && secondTargetNames.Count > 0)
        {
            float maxDistance = 0;
            foreach (string secondTargetName in secondTargetNames)
            {
                GameObject secondTargetObject = GameObject.Find(secondTargetName);
                if (secondTargetObject != null)
                {
                    float distance = Vector3.Distance(target.position, secondTargetObject.transform.position);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        secondTarget = secondTargetObject.transform;
                    }
                }
            }
            if (secondTarget != null)
            {
                // add the distance between the two targets to the offset
                offset += new Vector3(secondTarget.transform.localScale.x * 3, 0, 0);
                // round the offset up with no decimal
            }
        }
    }
}