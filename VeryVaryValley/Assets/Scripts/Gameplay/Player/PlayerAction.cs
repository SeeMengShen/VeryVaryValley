using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerAction : MonoBehaviour
{
    [Header("References")]
    public static Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public ItemSlot selectingItemSlot;

    [Header("Settings")]
    public int quantity;
    public float throwCooldown;

    [Header("Action")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public KeyCode useKey = KeyCode.Mouse1;
    public float throwForce;
    
    public bool readyToThrow;
    public bool holdingUsable;
    private bool charging;

    private const string NOT_THROWABLE = "This is not a throwable item!";
    private const string THROWABLE_PREFAB_PATH = "Throwabale Item Prefab/";
    private const string THROWING_ANIM_BOOL = "throwing";

    private const string NOT_USABLE = "This is not an usable item!";
    private const string ITEM_TAG = "Item";
    private const string POINTING_ITEM_TEXT = "Item: ";    

    public RawImage crosshair;

    public SceneUIManager sceneUIManager;

    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
        holdingUsable = false;
        charging = false;

        UpdateSelectingItem();
    }

    // Update is called once per frame
    void Update()
    {
        // If stop control is true, do not update
        if (LevelController.Instance.GetStopControl())
        {
            return;
        }

        // Check current selecting item
        UpdateSelectingItem();

        // Charging throw
        if (Input.GetKey(throwKey))
        {
            if (!ItemBar.Instance.IsHoldingThrowable() && !ItemBar.Instance.IsHoldingEmpty())
            {
                LevelController.Instance.ShowWarningText(NOT_THROWABLE);
                return;
            }
            else
            {
                if (readyToThrow && quantity >= 1)
                {
                    charging = true;
                }
            }
        }
        else
        {
            // Release key frame
            if (charging)
            {
                charging = false;
                StartCoroutine(ThrowDelay());
                readyToThrow = false;
            }
        }        

        //  Refresh the UI of the power bar
        sceneUIManager.ShowReady(readyToThrow);

        //  If player is selecting usable item in inventory
        if (holdingUsable)
        {
            if (Input.GetKeyDown(useKey))   // Shooting invoked
            {
                Use(CheckPointing());       // Get reference of pointing objects to collect
            }
            else
            {
                CheckPointing();            // Just check pointing objects
            }
        }
        else if (Input.GetKeyDown(useKey))
        {
            if (!ItemBar.Instance.IsHoldingEmpty())
            {
                LevelController.Instance.ShowWarningText(NOT_USABLE);
            }
        }
    }

    void FixedUpdate()
    {
        if (charging)
        {
            LevelController.Instance.PlayerCharging();
        }
    }

    private void Throw()
    {
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        Vector3 forceToAdd = forceDirection * throwForce * LevelController.Instance.currentPower;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        selectingItemSlot.MinusSlotContent();

        StartCoroutine(Cooldown());
    }

    private void ResetThrow()
    {
        readyToThrow = true;
        FirstPersonController.animator.SetBool(THROWING_ANIM_BOOL, false);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(throwCooldown);
        ResetThrow();
        LevelController.Instance.ResetPowerBar();
    }

    //  To cope with the animation delay
    IEnumerator ThrowDelay()
    {
        FirstPersonController.animator.SetBool(THROWING_ANIM_BOOL, true);
        yield return new WaitForSeconds(0.7f);
        Throw();

        //play throw sound
        AudioManager.Instance.PlayOneShotSoundEffect(AudioManager.Instance.throwingEffect);
    }

    //  Check holding item type to update objectToThrow for throwing or holdingUsable boolean
    public void UpdateSelectingItem()
    {
        selectingItemSlot = ItemBar.Instance.GetSelectingItemSlot();
        quantity = selectingItemSlot.quantity;

        if (selectingItemSlot.item.throwable)
        {
            objectToThrow = (GameObject)Resources.Load(THROWABLE_PREFAB_PATH + ItemBar.Instance.GetHoldingItem().name);
        }
        else
        {
            objectToThrow = null;
        }

        if (ItemBar.Instance.IsHoldingUsable())
        {
            holdingUsable = true;
        }
        else
        {
            holdingUsable = false;
        }

        sceneUIManager.ShowKeyUI();
    }

    //  Raycast out to check pointing object
    private GameObject CheckPointing()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 150.0f, 7))
        {
            if (hit.collider.tag == ITEM_TAG)
            {
                LevelController.Instance.ShowGrabText(POINTING_ITEM_TEXT + hit.collider.name);
                crosshair.color = Color.yellow;
                return hit.collider.gameObject;
            }
            else
            {
                LevelController.Instance.ShowGrabText(string.Empty);
            }
        }
        return gameObject;
    }

    // Collect object that was pointed by player
    private void Use(GameObject pointingObject)
    {
        if (pointingObject.tag == ITEM_TAG)
        {
            cam.GetComponent<PlayerSight>().Collect(pointingObject);
            pointingObject.GetComponent<Collectable>().Collected();
        }

        //play gun shot
        AudioManager.Instance.PlayGunSound();
    }
}
