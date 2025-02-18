using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WindmillManager : MonoBehaviour
{
    [SerializeField] private List<WindmillDynamicSpeed> windmills; // List of windmills
    [SerializeField] private Button lockButton; // Button to lock the current windmill and switch to the next one

    private int currentIndex = 0; // Index of the currently active windmill
    private bool[] locked; // Keeps track of locked windmills

    private void Start()
    {
        // Initialize the locked array based on the number of windmills
        locked = new bool[windmills.Count];

        // Set up the lock button listener
        if (lockButton != null)
        {
            lockButton.onClick.AddListener(LockAndSwitchWindmill);
        }

        // Enable the first windmill and disable others
        SetActiveWindmill(currentIndex);
    }

    private void Update()
    {
        // Update the speed of all windmills, but only the active windmill can change speed
        for (int i = 0; i < windmills.Count; i++)
        {
            if (i != currentIndex)
            {
                // Lock the windmill if it's not the active one
                if (!locked[i])
                {
                    windmills[i].LockInput();  // Lock the input on non-active windmills
                }
            }
            else
            {
                // Enable the active windmill's speed control
                if (locked[i])
                {
                    windmills[i].LockInput();  // Keep it locked after being set
                }
                else
                {
                    windmills[i].UnlockInput(); // Allow input for the active windmill
                }
            }
        }
    }

    private void LockAndSwitchWindmill()
    {
        // Lock the current windmill and switch to the next one
        if (!locked[currentIndex])
        {
            locked[currentIndex] = true;
            windmills[currentIndex].currentSpeed = windmills[currentIndex].currentSpeed; // Keep the current speed when locked
            windmills[currentIndex].LockInput(); // Lock input to stop changing speed

            // Move to the next windmill
            currentIndex = (currentIndex + 1) % windmills.Count;
            SetActiveWindmill(currentIndex);
        }
    }

    private void SetActiveWindmill(int index)
    {
        // No need to enable/disable the windmill GameObjects because they are always visible.
        // Just update the input behavior.
        for (int i = 0; i < windmills.Count; i++)
        {
            if (i == index)
            {
                // Make sure the active windmill accepts input for speed adjustment
                windmills[i].UnlockInput();
            }
            else
            {
                // Lock the input for the inactive windmills
                windmills[i].LockInput();
            }
        }
    }
}