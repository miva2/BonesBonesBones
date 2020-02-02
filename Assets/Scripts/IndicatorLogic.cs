using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorLogic : MonoBehaviour
{
    /// <summary>BattleUI GameObject;</summary>
    private GameObject battleCanvas;
    [SerializeField]
    private Image Indicator;
    [SerializeField]
    private float indicatorMoveSpeed = 0.3f;
    public GameObject[] attackPoints;

    [SerializeField]
    private int attackPointIndex = 0;
    [SerializeField]
    private Vector3 nextDestination;
    private float markerDistanceAllowance = 0.01f;

    
    public float greenZone = 3f;
    public float yellowZone = 8f;
    //public float redZone = ;
    private Rect currentTargetRect;
    private Transform currentTargetTransform;
    private Image currentTargetImage;
    private Rect indicatorRect;

    public Texture greenTexture;
    public Texture yellowTexture;
    public Texture redTexture;

    [SerializeField]
    private Sprite HitTexture, AimTexture;  


    private void Start()
    {
        battleCanvas = this.GetComponentInParent<Canvas>().gameObject;
        indicatorRect = this.GetComponent<RectTransform>().rect;
        nextDestination = attackPoints[attackPointIndex].transform.position;
        currentTargetRect = attackPoints[attackPointIndex].GetComponent<RectTransform>().rect;
        currentTargetTransform = attackPoints[attackPointIndex].GetComponent<Transform>();
        //currentTargetImage = attackPoints[attackPointIndex].GetComponent<Image>(); //------------- TODO HERE
    }
    private void Update()
    {
        Indicator_AutoMove();
        determineEffectiveness();
    }

    public void Attack()
    {
        print("HIT!");
    }

    private void determineEffectiveness()
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
            //currentTargetImage.image = redTexture; //------------- TODO HERE
        } else if (Mathf.Abs(indicatorX - targetX) <= yellowZone && Mathf.Abs(indicatorY - targetY) <= yellowZone
        && !(Mathf.Abs(indicatorX - targetX) <= greenZone && Mathf.Abs(indicatorY - targetY) <= greenZone)) 
        {
            //yellow
            // Debug.Log("YELLOW");
            //currentTargetImage.image = yellowTexture; //------------- TODO HERE
        } else if (Mathf.Abs(indicatorX - targetX) <= greenZone && Mathf.Abs(indicatorY - targetY) <= greenZone)
        {
            //green
            // Debug.Log("GREEN");
            //currentTargetImage.image = greenTexture; //------------- TODO HERE
        }
    }

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

        updateTarget();

        return attackPoints[attackPointIndex].transform.position;
    }

    private bool IsDestinationReached(Vector3 targetDestination)
    {
        var dist = Vector3.Distance(transform.position, targetDestination);

        return dist <= markerDistanceAllowance;
    }

    private void updateTarget()
    {
        currentTargetRect = attackPoints[attackPointIndex].GetComponent<RectTransform>().rect;
        currentTargetTransform = attackPoints[attackPointIndex].GetComponent<Transform>();
        //currentTargetImage = attackPoints[attackPointIndex].GetComponent<Image>(); //------------- TODO HERE
    }

    public void ChangeIndicatorImage(string Image){
        if(Image == "Aim"){
            Indicator.sprite = AimTexture;
        } else if (Image == "Hit!"){
            Indicator.sprite = HitTexture;
        }
    }
}
