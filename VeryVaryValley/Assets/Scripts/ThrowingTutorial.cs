using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class ThrowingTutorial : MonoBehaviour
{
    private FirstPersonController fpc;

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
    public float throwUpwardForce;

    bool readyToThrow;

    private string notThrowable;
    private string throwablePrefabPath;

    private void Start()
    {
        readyToThrow = true;
        fpc = GetComponent<FirstPersonController>();
        UpdateSelectingItem();
        InitText();
    }

    private void Update()
    {
        UpdateSelectingItem();

        if (Input.GetKeyDown(throwKey) && readyToThrow && quantity > 0)
        {
            Throw();
        }

        if(fpc.firstPersonCamera.activeInHierarchy)
        {
            cam = fpCam;
        }
        else
        {
            cam = tpCam;
        }
    }

    private void Throw()
    {
        if (!IsThrowable())
        {
            GameController.ShowThreeSecondText(notThrowable);
            return;
        }

        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 5f))
        {
            //forceDirection = (hit.point - attackPoint.position).normalized;
            Debug.Log(hit.collider.name);
        }        

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

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
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3.0f);
        ResetThrow();
    }

    public void UpdateSelectingItem()
    {
        selectingItemSlot = ItemBar.itemSlots[ItemBar.selectIndex];
        quantity = selectingItemSlot.quantity;
        objectToThrow = (GameObject)Resources.Load(throwablePrefabPath + selectingItemSlot.item.name);
    }

    private bool IsThrowable()
    {
        if(selectingItemSlot.item.throwable)
        {
            return true;
        }

        return false;
    }
    
    private void InitText()
    {
        notThrowable = "This is not a throwable item!";
        throwablePrefabPath = "Throwabale Item Prefab/";
    }
}