using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TragetDetector : MonoBehaviour
{
    private Color currentColor;
    public MeshRenderer meshRenderer;
    public Material mat;
    private Material currentMat;
    public bool canShootObject = false;
    public TargetController target;

    private void Start()
    {
        int playerRangeLayer = LayerMask.NameToLayer("DebugHover");
        Physics.IgnoreLayerCollision(gameObject.layer, playerRangeLayer);

        currentMat = meshRenderer.material;
        canShootObject=true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<TargetController>() != null)
        {
            TargetController targ = other.gameObject.GetComponent<TargetController>();
            meshRenderer.material = mat;
            //canShootObject = true;
            target = targ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TargetController>() != null)
        {
            meshRenderer.material = mat;
            //canShootObject = true;
            target = null;
        }
    }

    public void DisableTurret()
    {
        canShootObject = false;
    }
}
