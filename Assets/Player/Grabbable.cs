using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    private Rigidbody rbObj;
    Transform objectGrabPoint;
    private float lerpSpeed = 13f;
    private void Awake()
    {
        rbObj= GetComponent<Rigidbody>();
        rbObj.interpolation = RigidbodyInterpolation.Interpolate;
    }
    public void Grab(Transform objectGrabPoint)
    {
        this.objectGrabPoint = objectGrabPoint;
        
        rbObj.useGravity= false;
        rbObj.isKinematic= true;
        rbObj.drag = 5f;
    }
    public void Drop()
    {
        this.objectGrabPoint = null;
        rbObj.useGravity = true;
        rbObj.isKinematic = false;
        rbObj.drag = 0;
    }
    private void Update()
    {
        if (objectGrabPoint != null)
        {
            this.transform.rotation = Quaternion.LookRotation(objectGrabPoint.forward, objectGrabPoint.up);
        }
    }
    private void FixedUpdate()
    {
        if(objectGrabPoint!= null)
        {
            
            Vector3 newPos = Vector3.Lerp(rbObj.position, objectGrabPoint.position, Time.deltaTime * lerpSpeed);
            rbObj.MovePosition(newPos);
            
        }
    }
}
