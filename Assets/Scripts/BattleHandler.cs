using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
        /// <summary>
        /// Where can we attack.
        /// </summary>
    private enum HitZone { RED, YELLOW, GREEN }

    private IndicatorLogic indicatorLogic;
    private HitZone currentHitZone;

    private void Start() 
    {
            // Indicator script init; \\
        indicatorLogic.GetComponentInChildren<IndicatorLogic>(true);
    }

    /// <summary>
    /// Attacking: Player to AI
    /// </summary>
    private void Attacking()
    {

    }
}
