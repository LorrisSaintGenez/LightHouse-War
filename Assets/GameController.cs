using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GUIText barrelText;
    public int barrelCount;

    public GUIText hpText;
    public int healthPoint;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LowerHealthPoint (int damages)
    {
        healthPoint -= damages;
        UpdateHealthPointText();
    }

    public int GetHealthPoint ()
    {
        return healthPoint;
    }

    void UpdateHealthPointText ()
    {
        hpText.text = "HP: " + healthPoint;
    }

    public void RemoveBarrel ()
    {
        barrelCount--;
        UpdateBarrelCount();
    }

    public int GetBarrelLeft ()
    {
        return barrelCount;
    }

    void UpdateBarrelCount () {
        barrelText.text = "Barrels: " + barrelCount;
    }
}
