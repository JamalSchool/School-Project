using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCollector : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    public int inventorySize = 10;
    private void Start()
    {
        inventoryUI.IntializeInventoryUI(inventorySize);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }

}
