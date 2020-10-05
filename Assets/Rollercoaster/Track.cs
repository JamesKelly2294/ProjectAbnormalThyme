using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackType
{
    Loop = 0,
    Straight,
    Start,
    End,
    Photo,
    Bank
}

public class Track : MonoBehaviour, Buyable
{
    private TrackManager trackManager;
    public TrackType type;
    public bool isBlueprint;

    static Vector3[] localLoopWaypoints = new[] { new Vector3(-0.45f, -0.3367424f, 0f), new Vector3(-0.3128065f, -0.340954f, 0f), new Vector3(-0.2208815f, -0.3195973f, 0f), new Vector3(-0.05060089f, -0.2290567f, 0f), new Vector3(0.1157227f, -0.09050867f, 0f), new Vector3(0.2168044f, 0.05065752f, 0f), new Vector3(0.2486197f, 0.2162478f, 0f), new Vector3(0.1872462f, 0.3406396f, 0f), new Vector3(0.07577837f, 0.429786f, 0f), new Vector3(-0.0949018f, 0.4404377f, 0f), new Vector3(-0.2326232f, 0.3434279f, 0f), new Vector3(-0.2872765f, 0.249081f, 0f), new Vector3(-0.2900648f, 0.1055469f, 0f), new Vector3(-0.1779549f, -0.0652881f, 0f), new Vector3(0.00666678f, -0.240903f, 0f), new Vector3(0.229753f, -0.3267424f, 0f), new Vector3(0.45f, -0.3367424f, 0f) };
    static Vector3[] localStraightWaypoints = new[] { new Vector3(-0.45f, -0.3367424f, 0f), new Vector3(0.0f, -0.3367424f, 0f) };
    static Vector3[] localStartWaypoints = new[] { new Vector3(-0.9204822f, -0.7832438f, 0f), new Vector3(-0.2055083f, -0.487436f, 0f), new Vector3(0.02655159f, -0.400973f, 0f), new Vector3(0.154705f, -0.3587843f, 0f), new Vector3(0.2481711f, -0.3392277f, 0f), new Vector3(0.3811634f, -0.3367424f, 0f), new Vector3(0.435487f, -0.3367424f, 0f), new Vector3(0.4833426f, -0.3367424f, 0f) };
    static Vector3[] localEndWaypoints = new[] { new Vector3(-0.4812305f, -0.3421845f, 0f), new Vector3(-0.2873616f, -0.3393334f, 0f), new Vector3(-0.1562149f, -0.3678436f, 0f), new Vector3(0.482412f, -0.6016266f, 0f), new Vector3(1.172357f, -0.8639196f, 0f) };
    static Vector3[] localPhotowaypoints = new[] { new Vector3(-0.4955589f, -0.3367424f, 0f), new Vector3(-0.4592333f, -0.3367424f, 0f), new Vector3(-0.4180574f, -0.3148686f, 0f), new Vector3(-0.3752416f, -0.2752753f, 0f), new Vector3(-0.3368182f, -0.2106752f, 0f), new Vector3(-0.3228085f, -0.1600355f, 0f), new Vector3(-0.3096237f, -0.1123323f, 0f), new Vector3(-0.2707767f, -0.06280746f, 0f), new Vector3(-0.2014357f, -0.03104913f, 0f), new Vector3(-0.1044894f, -0.05188359f, 0f), new Vector3(0.08525414f, -0.226547f, 0f), new Vector3(0.2575977f, -0.3188751f, 0f), new Vector3(0.4051979f, -0.3367424f, 0f), new Vector3(0.493552f, -0.3367424f, 0f) };

    private void Awake()
    {
        trackManager = FindObjectOfType<TrackManager>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!isBlueprint)
        {
            trackManager.RegisterTrack(this);
        } else
        {
            var srs = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < srs.Length; i++)
            {
                srs[i].sortingLayerName = "Building Placement";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Height
    {
        get
        {
            switch (type)
            {
                default:
                    return 1;
            }
        }
    }

    public int Width
    {
        get
        {
            switch (type)
            {
                default:
                    return 1;
            }
        }
    }

    public Vector3[] LocalWaypoints()
    {
        switch (type)
        {
            case TrackType.Loop:
                return localLoopWaypoints;
            case TrackType.Straight:
                return localStraightWaypoints;
            case TrackType.Start:
                return localStartWaypoints;
            case TrackType.End:
                return localEndWaypoints;
            case TrackType.Photo:
                return localPhotowaypoints;
            default:
                return localStraightWaypoints;
        }
    }

    public int PurchaseCost()
    {
        return PurchaseCostForNthTrack(trackManager.CountForTrackType(type) + 1);
    }

    public bool IsPurchasable()
    {
        return trackManager.CanPurchaseTrackOfType(type);
    }

    public int PurchaseCostForNthTrack(int n)
    {
        switch (type)
        {
            case TrackType.Straight:
                return 0;
            case TrackType.Loop:
                return Mathf.RoundToInt(100 * (Mathf.Pow(5, n)));
            case TrackType.Photo:
                return 100;
            case TrackType.Bank:
                return 1000;
            default:
                return 0;
        }
    }

    // This can be exploited if we have cost modifiers - should probably cache the old purchase prices?
    public int RefundAmount()
    {
        return PurchaseCostForNthTrack(trackManager.CountForTrackType(type));
    }

    private void OnDestroy()
    {
        trackManager.UnregisterTrack(this);
    }
}
