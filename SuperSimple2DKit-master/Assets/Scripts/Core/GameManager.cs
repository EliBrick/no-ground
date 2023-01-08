using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Manages inventory, keeps several component references, and any other future control of the game itself you may need*/

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource; //A primary audioSource a large portion of game sounds are passed through
    public DialogueBoxController dialogueBoxController;
    public HUD hud; //A reference to the HUD holding your health UI, coins, dialogue, etc.
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    private static GameManager instance;
    [SerializeField] public AudioTrigger gameMusic;
    [SerializeField] public AudioTrigger gameAmbience;
    public List<Pylon> pylons = new List<Pylon>();

    // Singleton instantiation
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    public void GetInventoryItem(string name, Sprite image)
    {
        inventory.Add(name, image);

        if (image != null)
        {
            hud.SetInventoryImage(inventory[name]);
        }
    }

    public void RemoveInventoryItem(string name)
    {
        inventory.Remove(name);
        hud.SetInventoryImage(hud.blankUI);
    }

    public void ClearInventory()
    {   
        inventory.Clear();
        hud.SetInventoryImage(hud.blankUI);
    }

    public void checkPuzzle()
    {
        if(pylons.Count == 0)
        {
            pylons = new List<Pylon>(FindObjectsOfType<Pylon>());
        }
        bool levelClear = true;
        foreach (var p in pylons)
        {
            if (!p.electrified)
            {
                levelClear = false;
            }
        }
        if (levelClear)
        {
            GameObject door = GameObject.Find("Door");
            door.GetComponent<Collider2D>().enabled = true;
            door.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GameObject door = GameObject.Find("Door");
            door.GetComponent<Collider2D>().enabled = false;
            door.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

}
