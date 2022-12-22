using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class Using : MonoBehaviour
{
    [Header("References")]
    public Transform fpCam;
    public Transform tpCam;
    public Transform cam;
    public GameObject objectToUse;
    public ItemSlot selectingItemSlot;

    [Header("Settings")]
    //public float throwCooldown;

    [Header("Throwing")]
    public KeyCode useKey = KeyCode.Mouse1;

    private string notUsable;
    private string usingAnimBool;
    private string holdingAnimBool;
    private string itemTag;
    private string pointingItemText;

    public bool readyToUse;
    public bool holdingUsable;

    public RawImage crosshair;

    //Vector3 predictForceDirection;
    //Vector3 predictForceToAdd;

    private void Start()
    {
        holdingUsable = false;
        readyToUse = true;
        InitText();
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

        if (holdingUsable)
        {
            if (Input.GetKeyUp(useKey))
            {
                if (!IsEmpty())
                {
                    if (readyToUse)
                    {
                        Use(CheckPointing());
                    }
                }
            }
            else
            {
                CheckPointing();
            }
        }
        else if (Input.GetKey(useKey))
        {
            if (!IsEmpty())
            {
                GameController.Instance.ShowWarningText(notUsable);
                return;
            }
        }
    }

    private GameObject CheckPointing()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 20f))
        {
            if (hit.collider.tag == itemTag)
            {
                Debug.Log(hit.collider.name);
                GameController.Instance.ShowGrabText(pointingItemText + hit.collider.name);
                crosshair.color = Color.yellow;
                return hit.collider.gameObject;
            }
            else
            {
                GameController.Instance.ShowGrabText(string.Empty);
            }
        }
        return gameObject;
    }

    private void Use(GameObject pointingObject)
    {
        if (pointingObject.tag == itemTag)
        {
            cam.GetComponent<PlayerSight>().Collect(pointingObject);
            pointingObject.GetComponent<Collectable>().Collected();
        }

        //StartCoroutine(Cooldown());
    }

    private void ResetThrow()
    {
        readyToUse = true;
        FirstPersonController.animator.SetBool(usingAnimBool, false);
    }

    /*IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(throwCooldown);
        ResetThrow();
        GameController.Instance.ResetPowerBar();
    }*/

    IEnumerator ThrowDelay()
    {
        FirstPersonController.animator.SetBool(usingAnimBool, true);
        yield return new WaitForSeconds(0.7f);
        //Use();
    }

    public void UpdateSelectingItem()
    {
        selectingItemSlot = ItemBar.Instance.itemSlots[ItemBar.Instance.selectIndex];

        if (IsUsable() && !IsEmpty())
        {
            holdingUsable = true;
        }
        else
        {
            holdingUsable = false;
        }

        objectToUse.SetActive(holdingUsable);
        FirstPersonController.animator.SetBool(holdingAnimBool, holdingUsable);
    }

    private bool IsUsable()
    {
        if (!selectingItemSlot.item.throwable)
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
        notUsable = "This is not an usable item!";
        itemTag = "Item";
        holdingAnimBool = "holdingGun";
        usingAnimBool = "fire";
        pointingItemText = "Item: ";
    }
}