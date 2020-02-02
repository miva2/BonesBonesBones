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
        /// <summary>
        /// Enemy Bony Character Script;
        /// </summary>
    private BonyCharacter eBChS;

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
        eBChS = GetComponent<BonyCharacter>();
    }

    /// <summary>
    /// Attacking: Player to AI
    /// </summary>
    private void Attacking()
    {
        switch(cHZ.color)
        {
                // Miss;
            case HitZoneColor.RED:
                eBChS.TakeDamage(cHZ.hitzoneType, Random.Range(0f,0.25f));
                break;
            case HitZoneColor.YELLOW:
                eBChS.TakeDamage(cHZ.hitzoneType, Random.Range(0.26f,0.55f));
                break;
                // If you'er successful.
            case HitZoneColor.GREEN:
                eBChS.TakeDamage(cHZ.hitzoneType, Random.Range(0.75f,1f));
                break;
        }
    }
}
