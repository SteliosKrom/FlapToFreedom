using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsBase : MonoBehaviour
{
    [SerializeField] private float lowerYRange;
    [SerializeField] private float upperYRange;
    [SerializeField] private float xBounds;
    [SerializeField] private PlayerController playerController;

    public float XBounds { get => xBounds; set => xBounds = value; }
    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public float LowerYRange { get => lowerYRange; set => lowerYRange = value; }
    public float UpperYRange { get => upperYRange; set => upperYRange = value; }
}
