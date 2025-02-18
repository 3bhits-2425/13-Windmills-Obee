using UnityEngine;
using UnityEngine.UI;

public class WindmillDynamicSpeed : MonoBehaviour
{
    [SerializeField] private Light lampLight; // Assign in Inspector
    [SerializeField] private Slider speedSlider; // Assign in Inspector
    [SerializeField] private float maxRotationSpeed = 300f; // Maximum speed
    [SerializeField] private float acceleration = 50f; // Speed increase per second
    [SerializeField] private float deceleration = 30f; // Speed decrease per second
    [SerializeField] private Button toggleSpeedButton; // Button to toggle speed

    public float currentSpeed = 0f; // Current rotation speed
    public bool isSpeedConstant = false; // Toggle state for constant speed
    public bool isLocked = false; // State to lock input permanently

    private void Start()
    {
        // Set up the button listener to toggle speed
        if (toggleSpeedButton != null)
        {
            toggleSpeedButton.onClick.AddListener(ToggleSpeed);
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

        // Rotate the windmill
        RotateWindmill();

        // Update the slider value
        if (speedSlider != null)
        {
            speedSlider.value = Mathf.Round(currentSpeed);
        }

        // Control light intensity based on speed
        if (lampLight != null)
        {
            lampLight.intensity = Mathf.Lerp(0f, 1f, currentSpeed / maxRotationSpeed);
        }
    }

    public void RotateWindmill()
    {
        // Rotate the windmill at the current speed
        transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void ToggleSpeed()
    {
        // Toggle speed constant mode and lock input
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

  public void LockInput()
{
    isLocked = true;
}

public void UnlockInput()
{
    isLocked = false;
}

}