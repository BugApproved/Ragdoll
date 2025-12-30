using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkPlayer : MonoBehaviour
{
  [SerializeField]
  Rigidbody rigidbody3D;
  [SerializeField]
ConfigurableJoint mainJoint;

 Vector2 moveInputVector = Vector2.zero;
 bool isJumpButtonPressed = false;

 float maxSpeed = 3;

 bool isGrounded = false;

 RaycastHit[] raycastHits = new RaycastHit[10];

    void Start()
    {
        
    }

    void Update()
    {
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            isJumpButtonPressed = true;

        
    }

    void FixedUpdate()
    {
        isGrounded = false;
        int numberofHits = Physics.SphereCastNonAlloc(rigidbody3D.position, 0.1f, transform.up * -1, raycastHits, 0.5f);

        for (int i = 0; i < numberofHits; i++)
        {
            if (raycastHits[i].transform.root == transform)
            continue;
                isGrounded = true;
            break;
        }

        if (!isGrounded)
            rigidbody3D.AddForce(Vector3.down * 10);


            float inputMagnitued = moveInputVector.magnitude;

            if (inputMagnitued != 0)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(new Vector3(moveInputVector.x, 0, moveInputVector.y * -1), transform.up); 

                mainJoint.targetRotation = Quaternion.RotateTowards(mainJoint.targetRotation, desiredRotation, Time.fixedDeltaTime * 300);

                Vector3 localVelocifyVsForward = transform.forward * Vector3.Dot(transform.forward, rigidbody3D.linearVelocity);

                float localForwardVelocity = localVelocifyVsForward.magnitude;

                if (localForwardVelocity < maxSpeed)
            {
                rigidbody3D.AddForce(transform.forward * inputMagnitued * 30);
            }
            }
            if (isGrounded && isJumpButtonPressed)
            {
                rigidbody3D.AddForce(Vector3.up * 20, ForceMode.Impulse);
                isJumpButtonPressed = false;
            }
    }
}