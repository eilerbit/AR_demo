using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPlace : MonoBehaviour, IDraggable
{
    [SerializeField] float speedModifier = 0.001f;
    [SerializeField] private float minX = -1.15f, maxX = 1.15f;
    [SerializeField] private float minZ = -8.5f, maxZ = -6.5f;

    public void Drag(float inputX, float inputZ)
    {
        float xDrag = Mathf.Clamp(transform.position.x + inputX * speedModifier, minX, maxX);
        float zDrag = Mathf.Clamp(transform.position.z + inputZ * speedModifier, minZ, maxZ);

        Vector3 dragPosition = new Vector3(xDrag, transform.position.y, zDrag);

        transform.position = dragPosition;        
    }

    public void Rotate(float inputX, float inputZ)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x + inputZ * speedModifier, transform.rotation.y, transform.rotation.z + inputX * speedModifier);
    }
    
}
