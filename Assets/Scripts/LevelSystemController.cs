using System;
using UnityEngine;

public class LevelSystemController : MonoBehaviour
{
    [SerializeField] private Ui_level uiLevel;
    private readonly LevelSystem levelSystem = new LevelSystem();

    private SpaceshipControls spaceshipControls;
    private InventoryController inventoryController;
    private WeaponController weaponController;

    /// <summary> 
    ///   Sets inventory system, ensure that this happens before UI access the 
    ///   level system which would result in NullReferenceException 
    /// </summary>
    private void Awake()
    {
        spaceshipControls = GetComponent<SpaceshipControls>();
        inventoryController = GetComponent<InventoryController>();
        weaponController = GetComponentInChildren<WeaponController>();
        levelSystem.SetInventory(inventoryController.GetInventory());
        uiLevel.SetLevelSystem(levelSystem);
    }
    
    private void Start()
    {
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        Debug.Log("Get Children Component: " + weaponController);

    }

    /// <summary> 
    ///   Updates Player's Properties based on the current level
    /// </summary>
    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        PlayerRankEntry entry = levelSystem.GetCurrentPlayerLevelRank();
        spaceshipControls.setMaximumVelocity(entry.MaxVelocity);
        spaceshipControls.setMaxBoostDuration(entry.BoostDuration);
        weaponController.DamageMultiplier = entry.DamageFactor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            uiLevel.gameObject.SetActive(!uiLevel.gameObject.activeSelf);
        }
    }
}
