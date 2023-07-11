using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairTool : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Camera _camera;
    [SerializeField] private InventoryItemSO _machinePart, _panel;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _amountToRepair;

    // Update is called once per frame
    void Update()
    {
        //if the player clicked
        if(_inputHandler.firePrimary)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            //mousePos.z = 0f;

            Ray ray = _camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            //if the player clicked on a game object
            if(Physics.Raycast(ray, out hit))
            {
                //if the game object can be repaired
                if(hit.collider.GetComponent<Repairable>() != null && hit.collider.GetComponent<Health>() != null)
                {
                    Repairable repairable = hit.collider.GetComponent<Repairable>();
                    Health health = hit.collider.GetComponent<Health>();

                    //if the game object is repaired with machine parts
                    if(repairable.itemToConsume == Repairable.ItemToConsume.MachinePart) {

                        //if the game object is damaged and the player has machine parts to repair it
                        if(health.GetHealthPercent() != 1f && _inventory.RemoveItem(_machinePart) != null)
                        {
                            health.Heal(_amountToRepair);
                            Debug.Log(health.GetHealth());
                        }
                    }

                    //same as the above if statement, but with panels instead of machine parts
                    else if(repairable.itemToConsume == Repairable.ItemToConsume.Panel)
                    {
                        if (health.GetHealthPercent() != 1f && _inventory.RemoveItem(_panel) != null)
                        {
                            health.Heal(_amountToRepair);
                            Debug.Log(health.GetHealth());
                        }
                    }
                }
            }
        }
    }
}