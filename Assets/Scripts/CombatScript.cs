using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatScript : MonoBehaviour
{  

    [SerializeField, Header("Combat camera settings"),Tooltip("Main Camera")]
    private Camera CombatCamera;
    [SerializeField]
    private float zoomedOrthographicSize = 15;
    [SerializeField]
    private float maxZoomSpeed = 8;
    [Range(0f,120f), SerializeField]
    private float zoomAccel = 80;
    private float targetZoom;
    private float zoomSpeed;
    private float defaultOrthographicSize;
    [Space]
    // -----------------------------------------------------------------
    
     [Header("Rest")]
    
    // -------------------------------------------------------------------


        /// <summary> Is attack enabled?</summary>
    public bool attackEnabled = false;
        
        /// <summary>
        /// Curent enemy object.
        /// </summary>
    private GameObject currentEnemy;

        /// <summary>
        /// BattleUI canvas.
        /// </summary>
    private Canvas BattleUi;
    private int activeTriggers = 0;

        /// <summary>
        /// Checking, if battle was initializate alredy.
        /// </summary>
    private bool wasBattleInit = false;



    //--------------------------------------------------------------------------------------

    /// <summary>
    /// Possibilities, where you can hit.
    /// </summary>
    public enum HitZone { RED, YELLOW, GREEN }

    
        /// <summary>
        /// Currently hitted point.
        /// </summary>
    private HitZone currentHitZone;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hitZone">Enum HitZone</param>
    public void SetHitZone(HitZone hitZone)
    {
        currentHitZone = hitZone;
    }

    private void Start()
    {
            // Set current camera position. \\
        defaultOrthographicSize = CombatCamera.orthographicSize;
        targetZoom = defaultOrthographicSize;
    }

    private void Update()
    {
        UpdateZoom();
    }

    /// <summary>Zoom-in and zoom-out the camera.</summary>
    private void UpdateZoom()
    {
        var currentZoom = CombatCamera.orthographicSize;
        if (currentZoom == targetZoom) return;

        var sign = Mathf.Sign(targetZoom - currentZoom);

        var dt = Time.deltaTime;
        zoomSpeed = Mathf.Clamp(zoomSpeed + zoomAccel * dt * sign, -maxZoomSpeed, maxZoomSpeed);
        currentZoom += zoomSpeed * dt;

        CombatCamera.orthographicSize = Mathf.Clamp(currentZoom, zoomedOrthographicSize, defaultOrthographicSize);

        if (currentZoom == targetZoom)
        {
            zoomSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
            // [Enemy init] \\
        if (other.tag == "Enemy")
        {
                // Reference for currnet enemy init \\
            currentEnemy = other.gameObject;
                // 
            activeTriggers++;
            targetZoom = zoomedOrthographicSize;
        }
            // You spotted the enemy.  \\
        if (other.tag == "Enemy" && activeTriggers >= 1)
        {

            ShowBattleUI();
        }
            // Player can attack the enemy. \\
        if(other.tag == "Enemy" && activeTriggers == 2)
        {
                // Was battle init before?
            if(!wasBattleInit) BattleInit();
        }    
    }

    private void OnTriggerExit(Collider other) {
        attackEnabled = false;

        if (other.tag == "Enemy") activeTriggers--;
        if(other.tag == "Enemy" && activeTriggers == 1){
             attackEnabled = false;
             BattleUi.SendMessage("AttackFinish");
        }
        if(other.tag == "Enemy" && activeTriggers <= 0)
        {
            BattleUi.gameObject.SetActive(false);
            targetZoom = 20;
            wasBattleInit = false;
        }
    }

    /// <summary>First phase of battle.</summary>
    private void ShowBattleUI(){
        BattleUi = currentEnemy.GetComponentInChildren<Canvas>(true);
        BattleUi.gameObject.SetActive(true);
        //shadeOut.GetComponent<GameObject>().SetActive(true);
    }
        /// <summary>Secend phase of battle. </summary>
    private void BattleInit(){
        print("You can attack bro!");
            // Inform that battle started;
        BattleUi.SendMessage("AttackStarts");
        attackEnabled = true;
    }

}
