using UnityEngine;

public class CamObjFollow : MonoBehaviour
{
    private Transform target; // The target object to follow
    private Transform secondTarget; // The second target object to follow
    public Vector3 offset;    // Offset from the target
    public float smoothSpeed = 0.125f; // Smoothness factor for movement
    public string targetName;
    public string secondTargetName;

    // Start is called before the first frame update
    void Start()
    {
        GetTarget(); // Get the target and the offset
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Desired position
            Vector3 desiredPosition = target.position + offset;
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Smoothly rotate to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        }
        else
        {
            GetTarget(); // Get the target and the offset
        }
    }

    void GetTarget()
    {
        target = GameObject.Find(targetName).transform;
        if (target != null)
        {
            // Initialize the offset based on initial positions
            offset = new Vector3(target.transform.localScale.x, 0, 0);
            // round the offset up with no decimal
            if (offset.x < 1)
            {
                offset = new Vector3(5f, 0, 0);
            }
        }

        if (secondTargetName != null)
        {
            secondTarget = GameObject.Find(secondTargetName).transform;
            if (secondTarget != null)
            {
                // add the distance between the two targets to the offset
                offset += new Vector3(secondTarget.transform.localScale.x * 3, 0, 0);
                // round the offset up with no decimal
            }
        }
    }
}