using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
        /// <summary>
        /// Where can we attack.
        /// </summary>

    public enum HitZoneColor { RED, YELLOW, GREEN }

    public struct HitZone {
        public string hitzoneType;
        public HitZoneColor color;

        public HitZone(string hitzoneType, HitZoneColor color)
        {
            this.hitzoneType = hitzoneType;
            this.color = color;
        }
    }
    private IndicatorLogic indicatorLogic;
        /// <summary>
        /// Current Hit Zone.
        /// </summary>
    private HitZone cHZ;

    private void Start() 
    {
            // Indicator script init; \\
        indicatorLogic = GetComponentInChildren<IndicatorLogic>(true);
            // Nearest target (Point), nearests point color \\
            /// ummary>
            /// 
            /// </summary>
            /// <returns></returns>

        cHZ = indicatorLogic.GetCurrentHitzone();
    }

    /// <summary>
    /// Attacking: Player to AI
    /// </summary>
    private void Attacking()
    {
        switch(cHZ.hitzoneType){}
    }
}
