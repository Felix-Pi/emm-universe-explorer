using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceshipControls : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public float movementSpeed = 350f;

    public bool horizontalMovementInverted = false;
    public bool verticalMovementInverted = false;

    private Vector3 target_direction;
    private float angleHorizontal;
    private float angleVertical;

    [SerializeField] private Ui_inventory uiInventory;

    private Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " was triggered by " + other.gameObject.name);
        ItemWorld itemWorld = other.gameObject.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void Start()
    {
        //init spaceship position 
        //todo there is probably a better way

        transform.Rotate(5, 3.91f, 0);
        target_direction = new Vector3(0.54f, -0.094f, -0.84f);

        angleHorizontal = 0.5916f;
        angleVertical = -0.032f;

        // making the cursor invisible causes the UI Elements do not
        // react to the click events and other events as well.
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(340f, 220f, -20f), new Item { itemType = Item.ItemType.Mineral, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(350f, 220f, -20f), new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(360f, 220f, -20f), new Item { itemType = Item.ItemType.Medkit, amount = 1 });
    }


    void Update()
    {
        float rotationSpeedConverted = rotationSpeed * Time.deltaTime * 0.25f;
        float movementSpeedConverted = movementSpeed * Time.deltaTime * 0.25f;

        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");
        float movementInput = Input.GetAxis("Vertical");

        if (horizontalMovementInverted)
        {
            horizontalInput *= -1;
        }

        if (verticalMovementInverted)
        {
            verticalInput *= -1;
        }

        angleHorizontal += horizontalInput * rotationSpeedConverted;
        angleVertical += verticalInput * rotationSpeedConverted;

        target_direction =
            new Vector3(Mathf.Sin(angleHorizontal), Mathf.Sin(angleVertical), Mathf.Cos(angleHorizontal));
        transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.LookRotation(target_direction), 1);

        //only move forward
        if (movementInput > 0)
        {
            transform.Translate(0, 0, -(movementInput * movementSpeedConverted));
        }
    }
}