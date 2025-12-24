using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    [System.Serializable]
    public class BonePair
    {
        public Transform bone;
        public Transform targetBone;
        public ConfigurableJoint joint;
        public float strength = 1000f;
        public float damping = 100f;
    }

    public BonePair[] bones;
    public Animator animator;
    public Transform centerOfMass;
    
    void Start()
    {
        // Disable the animator's root motion
        if (animator)
            animator.applyRootMotion = false;
    }

    void FixedUpdate()
    {
        // Apply forces to match the animated pose
        foreach (var bonePair in bones)
        {
            if (bonePair.bone && bonePair.targetBone)
            {
                MatchPose(bonePair);
            }
        }
    }

    void MatchPose(BonePair bonePair)
    {
        Rigidbody rb = bonePair.bone.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Calculate the rotation difference
        Quaternion targetRotation = bonePair.targetBone.rotation;
        Quaternion currentRotation = bonePair.bone.rotation;
        Quaternion rotationDiff = targetRotation * Quaternion.Inverse(currentRotation);

        // Convert to angle-axis
        rotationDiff.ToAngleAxis(out float angle, out Vector3 axis);
        
        // Normalize angle
        if (angle > 180) angle -= 360;

        // Apply torque
        Vector3 torque = axis * (angle * Mathf.Deg2Rad) * bonePair.strength;
        rb.AddTorque(torque - rb.angularVelocity * bonePair.damping, ForceMode.Force);
    }
}