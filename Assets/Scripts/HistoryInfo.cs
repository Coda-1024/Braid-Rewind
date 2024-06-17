using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HistoryInfo
{
    public Vector3 postion;
    public Quaternion rotation;
    public float inputX;
    public float speedY;
    public int jumpCount;
    public bool isJump;
    public bool isFall;
    public bool isRun;
    public Platform nowPlatform;
    public AnimatorStateInfo animStateInfo0;
    public AnimatorStateInfo animStateInfo1;
}
