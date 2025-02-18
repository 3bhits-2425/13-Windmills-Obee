using UnityEngine;
using UnityEngine.UI;

public class WindmillManager : MonoBehaviour
{
    [SerializeField] private WindmillDynamicSpeed[] windmills; // Array of windmills to control
    [SerializeField] private Button nextWindmillButton; // Button to proceed to the next windmill

    private int currentWindmillIndex = 0;

    private void Start()
    {
        if (nextWindmillButton != null)
        {
            nextWindmillButton.onClick.AddListener(ActivateNextWindmill);
        }

        InitializeWindmills();
    }

    private void InitializeWindmills()
    {
        // Ensure all windmills are inactive except the first one
        for (int i = 0; i < windmills.Length; i++)
        {
            windmills[i].enabled = (i == 0); // Only enable the first windmill
        }
    }

    private void ActivateNextWindmill()
    {
        if (currentWindmillIndex < windmills.Length)
        {
            // Lock the current windmill
            windmills[currentWindmillIndex].SendMessage("LockInput");

            // Move to the next windmill
            currentWindmillIndex++;

            if (currentWindmillIndex < windmills.Length)
            {
                // Enable the next windmill
                windmills[currentWindmillIndex].enabled = true;
            }
            else
            {
                // Disable the button if all windmills are locked
                nextWindmillButton.interactable = false;
            }
        }
    }
}
