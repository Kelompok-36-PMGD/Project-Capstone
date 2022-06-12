using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{    
    public enum InteractionType { NONE, PickUp, Examine,GrabDrop, Interact }
    public enum ItemType { Staic, Consumables}
    [Header("Attributes")]
    public string itemName;
    public InteractionType interactType;
    public ItemType type;
    [Header("Exmaine")]
    public string descriptionText;
    [Header("Custom Events")]
    public UnityEvent customEvent;
    public UnityEvent consumeEvent;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Interact()
    {
        switch(interactType)
        {
            case InteractionType.PickUp:
                //Add the object to the PickedUpItems list
                //Check whether the inventory is full
                if(FindObjectOfType<InventorySystem>().items.Count == FindObjectOfType<InventorySystem>().itemMax)
                {
                    //Shows inventory full text
                    Debug.Log("Inventory full!!");
                }
                else
                {
                    FindObjectOfType<InventorySystem>().PickUp(gameObject);
                    //Disable
                    gameObject.SetActive(false);
                    DontDestroyOnLoad(gameObject);
                }
                break;
            case InteractionType.Examine:
                //Call the Examine item in the interaction system
                FindObjectOfType<InteractionSystem>().ExamineItem(this);                
                break;
            case InteractionType.GrabDrop:
                //Grab interaction
                FindObjectOfType<InteractionSystem>().GrabDrop();
                break;
            case InteractionType.Interact:
                //Straight to customEvent.Invoke()
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }

        //Invoke (call) the custom event(s)
        customEvent.Invoke();
    }

}
