using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{

    private Health healthScript;

    // Start is called before the first frame update
    void Awake()
    {
        healthScript = GetComponent<Health>();
    }
public void ActivateShield(bool shieldActive)
   
    {
        healthScript.shieldActivated= shieldActive;
    }
}
