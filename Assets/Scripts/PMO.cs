using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMO : MonoBehaviour
{
    [SerializeField]
    private float fuseTimer = 5;
    void Update()
    {
        DestroyAfterTime();
    }
    /// <summary>
    /// This Function destroy current PlayerMovementObject (PMO).
    /// </summary>
    public void DestroyPMO()
    {
        Destroy(this.gameObject);
    }
    private void DestroyAfterTime(){
        fuseTimer -= 1 * Time.deltaTime;
        if (fuseTimer <= 0)
            Destroy(this.gameObject); 
        }
}
