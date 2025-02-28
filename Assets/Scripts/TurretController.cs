using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public bool turretActive = false;
    public bool canUseTurret = false;
    public bool isUsingTurret = false;
    public Transform usePosition;

    public GameObject turretHead;
    public GameObject defaultBullet;
    public GameObject currentBullet;

    private void Start()
    {
        currentBullet = defaultBullet;
        turretActive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(turretActive)
        {
            if (other.gameObject.tag == "Player")
            {
                TragetDetector fillChecker = currentBullet.GetComponentInChildren<TragetDetector>();
                InventoryManager.Instance.player.targetDetector = fillChecker;
                InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().canSwitchFPP = true;
                InventoryManager.Instance.player.animator.SetBool("TurretAiming", true);
                InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().ShowPickUpPopUp(null);
                canUseTurret = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isUsingTurret)
        {
            InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().canSwitchFPP = false;
            InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().HidePickUpPopUp();
            canUseTurret = false;
        }
    }

    public void EnableTurret()
    {
        turretActive = true;
    }

    public void DisableTurret()
    {
        turretActive = false;
    }

    

    private void Update()
    {
        if(canUseTurret)
        {
            if((Input.GetKeyDown(KeyCode.E) || InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed) && !isUsingTurret)
            {
                InventoryManager.Instance.player.DebugTransform = currentBullet.transform;
                TragetDetector fillChecker = currentBullet.GetComponentInChildren<TragetDetector>();
                InventoryManager.Instance.player.targetDetector = fillChecker;
                InventoryManager.Instance.player.FPPSwitch(usePosition, turretHead,true);
                currentBullet.SetActive(true);
                //InventoryManager.Instance.player.gameObject.SetActive(false);
                isUsingTurret = true;
            }
            else if((Input.GetKeyDown(KeyCode.E) || InventoryManager.Instance.player.InteractIspressed) && isUsingTurret)
            {
              GetDownOfTurret();
            }
        }

        if(isUsingTurret)
        {
            if(Input.GetMouseButton(0) || InventoryManager.Instance.player.ShootState)
            {

            }
        }
    }

    public void GetDownOfTurret()
    {
        //InventoryManager.Instance.player.FPPSwitch(usePosition, turretHead, false);
        //InventoryManager.Instance.player.gameObject.SetActive(true);
        isUsingTurret = false;
        currentBullet.SetActive(false);
    }

    public void ChangeBullet(GameObject bullet)
    {
        currentBullet.SetActive(false);
        currentBullet = bullet;
        InventoryManager.Instance.player.DebugTransform = currentBullet.transform;
        TragetDetector fillChecker = currentBullet.GetComponentInChildren<TragetDetector>();
        InventoryManager.Instance.player.targetDetector = fillChecker;
    }
}
