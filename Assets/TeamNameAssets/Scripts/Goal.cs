using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {
    public string goalKey = "default";
    public bool hasMessage = true;
    public string nextMessage = "horse";
    public bool hasGroup = true;
    public GameObject group;
    public bool giveFuel = false;
    public float fuelAmount = 100000f;
    private bool hasTriggered = false;
    private Text messageDisplayText;
    private GameObject messageDisplay;
    private Animator messageAnimator;
    private Text fuelText;
    private GameObject fuelDisplay;
    private Text foodText;
    private GameObject foodDisplay;
    private Text horsesText;
    private GameObject horsesDisplay;
    private Text mutText;
    private GameObject mutDisplay;
    public bool updateFuelRate = false;
    public int fuelRate = 100;
    public bool updateFoodRate = false;
    public int foodRate = 100;
    public bool updateHorsesRate = false;
    public int horsesRate = 100;
    public bool updateMutRate = false;
    public int mutRate = 100;
    

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<KeyCode>() != null)
        {
            if ((other.gameObject.GetComponent<KeyCode>().key == goalKey) && (!hasTriggered))
            {
                Debug.Log("Goal!");
                hasTriggered = true;
                if (hasMessage)
                {
                    UpdateMessage();
                }
                if (hasGroup)
                {
                    ShowGroup();
                }
                if (giveFuel)
                {
                    if (other.gameObject.GetComponent<PlayerScript>() != null)
                    {
                        other.gameObject.GetComponent<PlayerScript>().GiveFuel(fuelAmount);
                    }
                }

                if (updateFuelRate) { UpdateFuelRate(); }
                if (updateFoodRate) { UpdateFoodRate(); }
                if (updateHorsesRate) { UpdateHorsesRate(); }
                if (updateMutRate) { UpdateMutRate(); }

                gameObject.SetActive(false);
            }
        }
    }
    
    void UpdateMessage()
    {
        messageDisplay = GameObject.FindWithTag("MessageHudText");
        messageDisplayText = messageDisplay.GetComponent(typeof(Text)) as Text;
        messageDisplayText.text = nextMessage;
        messageAnimator = messageDisplay.GetComponentInParent<Animator>();
        messageAnimator.SetTrigger("NewMessage");
    }

    void UpdateFuelRate()
    {
        fuelDisplay = GameObject.FindWithTag("FuelProdHudText");
        fuelText = fuelDisplay.GetComponent(typeof(Text)) as Text;
        fuelText.text = "" + fuelRate;
    }

    void UpdateFoodRate()
    {
        foodDisplay = GameObject.FindWithTag("FoodProdHudText");
        foodText = foodDisplay.GetComponent(typeof(Text)) as Text;
        foodText.text = "" + foodRate;
    }

    void UpdateHorsesRate()
    {
        horsesDisplay = GameObject.FindWithTag("HorsesProdHudText");
        horsesText = horsesDisplay.GetComponent(typeof(Text)) as Text;
        horsesText.text = "" + horsesRate;
    }

    void UpdateMutRate()
    {
        mutDisplay = GameObject.FindWithTag("MutProdHudText");
        mutText = mutDisplay.GetComponent(typeof(Text)) as Text;
        mutText.text = "" + mutRate;
    }

    void ShowGroup()
    {
        group.SetActive(true);
    }
       
}
