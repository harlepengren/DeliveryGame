using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackupLight : MonoBehaviour
{
    [SerializeField] Material backupLights;
 
    private PlayerInput controls;
    private Color basicColor;
    private float backupIntensity = 2.0f;

    void Awake()
    {
        controls = new PlayerInput();

        basicColor = backupLights.GetColor("_EmissionColor");
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        Vector2 move_direction = controls.Player.Move.ReadValue<Vector2>();

        if(move_direction.y < 0){
            Color nextColor = backupLights.GetColor("_EmissionColor");
            nextColor = nextColor * backupIntensity;
            backupLights.SetColor("_EmissionColor",nextColor);
        } else {
            backupLights.SetColor("_EmissionColor",basicColor);
        }
    }
}
