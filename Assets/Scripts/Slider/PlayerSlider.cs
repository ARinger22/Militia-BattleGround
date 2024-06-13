using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    public Slider fuelSlider; // Reference to the fuel slider

    private float maxFuel = 100f;
    private float currentFuel;
    void Start()
    {
        currentFuel = maxFuel;
        UpdateFuelUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentFuel > maxFuel) currentFuel -= Time.deltaTime * 10;
            UpdateFuelUI();
        }
        else {
            if(currentFuel < maxFuel) currentFuel += Time.deltaTime * 10;
            UpdateFuelUI();
        }
    }

    public void UpdateFuelUI()
    {
        fuelSlider.value = currentFuel / maxFuel;
    }
}
