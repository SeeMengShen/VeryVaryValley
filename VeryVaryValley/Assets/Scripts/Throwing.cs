using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform fpCam;
    public Transform tpCam;
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public ItemSlot selectingItemSlot;

    [Header("Settings")]
    public int quantity;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    //public float throwUpwardForce;

    private string notThrowable;
    private string throwablePrefabPath;
    private string throwingAnimBool;

    public bool readyToThrow;

    private bool charging;

    //Vector3 predictForceDirection;
    //Vector3 predictForceToAdd;

    private void Start()
    {
        readyToThrow = true;
        UpdateSelectingItem();
        InitText();
        charging = false;
    }

    private void Update()
    {
        if (GameController.Instance.stopControl)
        {
            return;
        }

        if (GameController.Instance.fpc.firstPersonCamera.activeInHierarchy)
        {
            cam = fpCam;
        }
        else
        {
            cam = tpCam;
        }

        UpdateSelectingItem();

        /*if (Input.GetKeyDown(throwKey) && readyToThrow && quantity >= 1)
        {
            StartCoroutine(ThrowDelay());
            readyToThrow = false;
        }*/

        if (Input.GetKey(throwKey))
        {
            if (!IsThrowable() && !IsEmpty())
            {
                GameController.Instance.ShowWarningText(notThrowable);
                return;
            }
            else
            {
                if (readyToThrow && quantity >= 1)
                {
                    //predictForceDirection = cam.transform.forward;
                    //predictForceToAdd = predictForceDirection * throwForce * GameController.currentPower;
                    //Debug.Log(predictForceToAdd);
                    charging = true;
                    //DrawTrajectory.Intsance.UpdateTrajectory(predictForceToAdd, objectToThrow.GetComponent<Rigidbody>(), attackPoint.position);
                }

            }
        }
        else if (Input.GetKeyUp(throwKey))
        {
            if (IsThrowable())
            {
                if (readyToThrow && quantity >= 1)
                {
                    charging = false;
                    StartCoroutine(ThrowDelay());
                    //DrawTrajectory.Intsance.HideLine();
                    readyToThrow = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (charging)
        {
            GameController.Instance.PlayerCharging();
        }
    }

    private void Throw()
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        /*RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 5f))
        {
            //forceDirection = (hit.point - attackPoint.position).normalized;
            Debug.Log(hit.collider.name);
        }*/

        Vector3 forceDirection = cam.transform.forward;

        Vector3 forceToAdd = forceDirection * throwForce * GameController.Instance.currentPower;

        // add force
        // + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        //projectileRb.velocity = transform.TransformDirection(throwingForce);

        selectingItemSlot.MinusSlotContent();

        // implement throwCooldown
        //Invoke(nameof(ResetThrow), throwCooldown);

        StartCoroutine(Cooldown());
    }

    private void ResetThrow()
    {
        readyToThrow = true;
        FirstPersonController.animator.SetBool(throwingAnimBool, false);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(throwCooldown);
        ResetThrow();
        GameController.Instance.ResetPowerBar();
    }

    IEnumerator ThrowDelay()
    {
        FirstPersonController.animator.SetBool(throwingAnimBool, true);
        yield return new WaitForSeconds(0.7f);
        Throw();
    }

    public void UpdateSelectingItem()
    {
        selectingItemSlot = ItemBar.Instance.itemSlots[ItemBar.Instance.selectIndex];
        quantity = selectingItemSlot.quantity;

        if (selectingItemSlot.item.throwable)
        {
            objectToThrow = (GameObject)Resources.Load(throwablePrefabPath + selectingItemSlot.item.name);
        }
        else
        {
            objectToThrow = null;
        }
    }

    private bool IsThrowable()
    {
        if (selectingItemSlot.item.throwable)
        {
            return true;
        }

        return false;
    }

    private bool IsEmpty()
    {
        if (selectingItemSlot.item == ItemBar.Instance.emptyItem)
        {
            return true;
        }

        return false;
    }

    private void InitText()
    {
        notThrowable = "This is not a throwable item!";
        throwablePrefabPath = "Throwabale Item Prefab/";
        throwingAnimBool = "throwing";
    }
}