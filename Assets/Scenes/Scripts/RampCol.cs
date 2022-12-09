using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampCol : MonoBehaviour
{
    [SerializeField]private float jumpForce = 100;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Force);
            
        }
    }
}
