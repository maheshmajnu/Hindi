using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAfterSeconds : MonoBehaviour
{
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        StartCoroutine(TurnOnRigidbodyAfterSeconds());
    }

    IEnumerator TurnOnRigidbodyAfterSeconds()
    {
        yield return new WaitForSeconds(14f);
        _rigidbody.isKinematic = false;
    }
    
}
