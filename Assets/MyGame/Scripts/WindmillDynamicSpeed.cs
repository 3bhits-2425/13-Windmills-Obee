using UnityEngine;
using UnityEngine.UI;

public class WindmillDynamicSpeed : MonoBehaviour
{
    [SerializeField] private Light lampLight; // Assign in Inspector
    [SerializeField] private float maxLightIntensity = 1f; // Maximum lamp brightness
    [SerializeField] private Slider speedSlider; // Assign in Inspector
    [SerializeField] private float maxRotationSpeed = 300f; // Maximum speed
    [SerializeField] private float acceleration = 50f; // Speed increase per second
    [SerializeField] private float deceleration = 30f; // Speed decrease per second
    [SerializeField] private Button toggleSpeedButton; // Button to toggle speed

    private float currentSpeed = 0f; // Current rotation speed
    private bool isSpeedConstant = false; // Toggle state for constant speed
    private bool isLocked = false; // State to lock input permanently

    private void Start()
    {
        if (toggleSpeedButton != null)
        {
            toggleSpeedButton.onClick.AddListener(ToggleSpeed); // Add button click listener
        }
    }

    private void Update()
    {
        // If input is locked, keep rotating at the current speed
        if (isLocked)
        {
            RotateWindmill();
            return;
        }

        // If speed is not constant, allow speed adjustment with Space key
        if (!isSpeedConstant)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deceleration * Time.deltaTime;
            }

            // Clamp speed between 0 and maxRotationSpeed
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxRotationSpeed);
        }

        RotateWindmill();

        // Update the slider value
        if (speedSlider != null)
        {
            speedSlider.value = Mathf.Round(currentSpeed);
        }

        // Control light intensity based on speed
        if (lampLight != null)
        {
            lampLight.intensity = Mathf.Lerp(0f, maxLightIntensity, currentSpeed / maxRotationSpeed);
        }
    }

    private void RotateWindmill()
    {
        // Rotate the rotor hub
        transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void ToggleSpeed()
    {
        if (isLocked)
        {
            return; // Do nothing if locked
        }

        isSpeedConstant = !isSpeedConstant;

        // If speed becomes constant, fix it at its current value
        if (isSpeedConstant)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxRotationSpeed);
            LockInput(); // Permanently lock input after toggling
        }
    }

    private void LockInput()
    {
        isLocked = true;
    }
}
