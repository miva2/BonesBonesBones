using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorLogic : MonoBehaviour
{
    public GameObject[] attackPoints;

    public float greenZone = .5f;
    public float yellowZone = 2f;

    public Sprite greenTexture;
    public Sprite yellowTexture;
    public Sprite redTexture;

    public CombatScript combatScript;


    /// <summary>BattleUI GameObject;</summary>
    private GameObject battleCanvas;
    private Image Indicator;
    [SerializeField]
    private float indicatorMoveSpeed = 0.3f;

    [SerializeField]
    private int attackPointIndex = 0;
    private Image[] attackPointImage;

    [SerializeField]
    private Vector3 nextDestination;
    private float markerDistanceAllowance = 0.01f;

    [SerializeField]
    private Sprite HitTexture, AimTexture;  


    private void Start()
    {
        battleCanvas = this.GetComponentInParent<Canvas>().gameObject;
        nextDestination = attackPoints[attackPointIndex].transform.position;

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

        var pos = transform.position;
        var target = point.transform.position;
        var distSq = Vector3.SqrMagnitude(pos - target);

        var img = attackPointImage[index];

        if (distSq < greenZone * greenZone)
        {
            img.sprite = greenTexture;
            combatScript.SetHitZone(CombatScript.HitZone.GREEN);
        }
        else if (distSq < yellowZone * yellowZone)
        {
            img.sprite = yellowTexture;
            combatScript.SetHitZone(CombatScript.HitZone.YELLOW);
        }
        else
        {
            img.sprite = redTexture;
            combatScript.SetHitZone(CombatScript.HitZone.RED);
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
            transform.position = Vector3.MoveTowards(transform.position, nextDestination, indicatorMoveSpeed * Time.deltaTime);
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

        return attackPoints[attackPointIndex].transform.position;
    }

    private bool IsDestinationReached(Vector3 targetDestination)
    {
        var dist = Vector3.Distance(transform.position, targetDestination);

        return dist <= markerDistanceAllowance;
    }


    public void ChangeIndicatorImage(string Image){
        if(Image == "Aim"){
            Indicator.sprite = AimTexture;
        } else if (Image == "Hit!"){
            Indicator.sprite = HitTexture;
        }
    }
}
