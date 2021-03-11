using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup settings")]
    [Tooltip(("How fast ship moves up and down based upon player input"))]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float yRange = 9f;
    
    [Header("Laser gun list")]
    [Tooltip(("Add all player lasers here"))]
    [SerializeField] private List<ParticleSystem> lasers; 
    
    [Header("Screen position based tuning")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -15f;

    [Header("Player input based tuning")]
    [SerializeField] private float controlRollFactor = -30f;
    [SerializeField] private float positionYawFactor = 2.5f;

    private float yThrow;
    private float xThrow;

    private void Update()
    {
        UpdateThrow();
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        var lasersActiveState = Input.GetButton("Fire1");
        
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var go in lasers)
        {
            var emissionModule = go.emission;
            emissionModule.enabled = lasersActiveState;
        }
    }

    private void UpdateThrow()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
    }

    private void ProcessRotation()
    {
        var localPosition = transform.localPosition;
        
        var pitchDuwToPosition = localPosition.y * positionPitchFactor;
        var pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        var pitch = pitchDuwToPosition + pitchDueToControlThrow;
        var yaw = localPosition.x * positionYawFactor;
        var roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        var transformL = transform;
        var localPos = transformL.localPosition;

        var xOffset = xThrow * movementSpeed * Time.deltaTime;
        var xPos = Mathf.Clamp(localPos.x + xOffset, -xRange, xRange);

        var yOffset = yThrow * movementSpeed * Time.deltaTime;
        var yPos = Mathf.Clamp(localPos.y + yOffset, -yRange, yRange);

        transformL.localPosition = new Vector3(xPos, yPos, localPos.z);
    }
}
