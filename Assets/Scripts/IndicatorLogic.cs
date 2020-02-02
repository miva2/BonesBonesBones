using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorLogic : MonoBehaviour
{

    [Header("Pointers vars")]
    public GameObject[] attackPoints;
    public Sprite greenTexture;
    public Sprite yellowTexture;
    public Sprite redTexture;
    [Space]
    
    [SerializeField, Header("Hit chance radius")]
    private float greenZone = .4f;
    [SerializeField]
    private float yellowZone = 2f;
    [Space]


    public BattleHandler BattleHandler;


    /// <summary>BattleUI GameObject;</summary>
    private GameObject battleCanvas;
    private Image Indicator;
    [SerializeField, Header("Indicator"),Range(20f,150f)]
    private float indicatorMoveSpeed = 20f;

    private int attackPointIndex = 0;
    private Image[] attackPointImage;

    private Vector3 nextDestination;
    private float markerDistanceAllowance = 0.01f;

    [Header("Identicator sprites")]
    [SerializeField]
    private Sprite AimSprite;  
    [SerializeField]
    private Sprite HitSprite;
    [SerializeField]
    private Sprite MissSprite;


    private void Start()
    {
        battleCanvas = this.GetComponentInParent<Canvas>().gameObject;
        nextDestination = attackPoints[attackPointIndex].transform.localPosition;

        Indicator = GetComponent<Image>();

        attackPointImage = new Image[3];
        for (var i = 0; i < 3; i++)
            attackPointImage[i] = attackPoints[i].GetComponent<Image>();
    }
    private void Update()
    {
        Indicator_AutoMove();
        SetColors();       
    }

    public void Attack()
    {
        print("HIT!");
    }

    void SetColors()
    {
        SetColor(0);
        SetColor(1);
        SetColor(2);
    }

    void SetColor(int index)
    {
        var point = attackPoints[index];

        var pos = transform.localPosition;
        var target = point.transform.localPosition;
        var distSq = Vector3.SqrMagnitude(pos - target);

        var img = attackPointImage[index];

        if (distSq < greenZone * greenZone)
        {
            img.sprite = greenTexture;
        }
        else if (distSq < yellowZone * yellowZone)
        {
            img.sprite = yellowTexture;
        }
        else
        {
            img.sprite = redTexture;
        }
    }

    /*private void determineEffectiveness()
    {

        // TODO: dont calculate every frame
        // TODO: make code less ugly
        float targetMiddleWidth = currentTargetRect.width / 2f;
        float targetMiddleHeight = currentTargetRect.height / 2f;
        float indicatorMiddleWidth = indicatorRect.width / 2f;
        float indicatorMiddleHeight = indicatorRect.height / 2f;
        // Debug.Log("middleshit: " + targetMiddleWidth + ", " + targetMiddleHeight + ", " + indicatorMiddleWidth + ", " + indicatorMiddleHeight);

        // ##########################################
        // # ----------------------------------------------------------- #
        // # I comment it out cause I need console. Roman ;) #
        // # ----------------------------------------------------------- #
        // ###########################################

        float indicatorX = transform.position.x + indicatorMiddleWidth;
        float indicatorY = transform.position.y + indicatorMiddleHeight;
        float targetX = currentTargetTransform.position.x + targetMiddleWidth;
        float targetY = currentTargetTransform.position.y + targetMiddleHeight;

        // Debug.Log("transform.position.x " + transform.position.x);
        // Debug.Log("indicator: " + indicatorX + ", " + indicatorY);
        // Debug.Log("target: " + targetX + ", " + targetY);


        //Debug.Log("Mathf.Abs(indicatorX - targetX)" + Mathf.Abs(indicatorX - targetX));
        if (Mathf.Abs(indicatorX - targetX) > yellowZone && Mathf.Abs(indicatorY - targetY) > yellowZone)
        {
            //red
            // Debug.Log("RED");
            //changing the image of other component is ugly. Should send an event.
            currentTargetImage.sprite = redTexture; //------------- TODO HERE
        } else if (Mathf.Abs(indicatorX - targetX) <= yellowZone && Mathf.Abs(indicatorY - targetY) <= yellowZone
        && !(Mathf.Abs(indicatorX - targetX) <= greenZone && Mathf.Abs(indicatorY - targetY) <= greenZone)) 
        {
            //yellow
            // Debug.Log("YELLOW");
            currentTargetImage.sprite = yellowTexture; //------------- TODO HERE
        } else if (Mathf.Abs(indicatorX - targetX) <= greenZone && Mathf.Abs(indicatorY - targetY) <= greenZone)
        {
            //green
            // Debug.Log("GREEN");
            currentTargetImage.sprite = greenTexture; //------------- TODO HERE
        }
    }*/

    private void Indicator_AutoMove(){
        if (battleCanvas.activeInHierarchy == true)
        {
            if (IsDestinationReached(nextDestination)) {
                nextDestination = GetNextDestination();
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextDestination, indicatorMoveSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetNextDestination()
    {
        int amount = attackPoints.Length;

        if (attackPointIndex < amount - 1)
        {
            attackPointIndex++;
        }
        else
        {
            attackPointIndex = 0;
        }

        return attackPoints[attackPointIndex].transform.localPosition;
    }

    private bool IsDestinationReached(Vector3 targetDestination)
    {
        var dist = Vector3.Distance(transform.localPosition, targetDestination);

        return dist <= markerDistanceAllowance;
    }

    /// <summary>
    /// Change sprite image of indicator as you need.
    /// </summary>
    /// <param name="Image">String {Aim, Hit, Miss}</param>
    public void ChangeIndicatorImage(string Image){
        if(Image == "Aim"){
            Indicator.sprite = AimSprite;
        } else if (Image == "Hit"){
            Indicator.sprite = HitSprite;
        } else if(Image == "Miss"){
            Indicator.sprite = MissSprite;
        }
    }

    private GameObject GetClosestTarget()
    {
        Vector3 indicatorPosition = transform.localPosition;
        GameObject closestTarget = attackPoints[0];
        float currentClosestDistance = float.MaxValue;

        foreach(GameObject target in attackPoints){
            float distance = Vector3.Distance(target.transform.localPosition, indicatorPosition);
            if(distance < currentClosestDistance)
            {
                currentClosestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    /// <returns>Nearest target(HeadPoit, LeftPoint, RightPoint), Color of nearest target (RED, YELLOW, GREEN)</returns>
    public BattleHandler.HitZone GetCurrentHitzone()
    {
        GameObject target = GetClosestTarget();

        string hitzoneType = target.name;

        Sprite sprite = target.GetComponent<Image>().sprite;
        BattleHandler.HitZoneColor color;
        if (sprite == greenTexture)
        {
            color = BattleHandler.HitZoneColor.GREEN;
        }
        else if (sprite == yellowTexture)
        {
            color = BattleHandler.HitZoneColor.YELLOW;
        }
        else
        {
            color = BattleHandler.HitZoneColor.RED;
        }

        BattleHandler.HitZone hitZone = new BattleHandler.HitZone(hitzoneType, color);

        return hitZone;
    }
}
