using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPhysicsObjects : MonoBehaviour
{
    Rigidbody rigidbody3D;
    ConfigurableJoint joint;

    [SerializeField]
    bool syncAnimation = false;
    [SerializeField]
    bool syncJoint = true;
}
