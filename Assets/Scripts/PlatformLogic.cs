using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic
{
    private Movement moveObj;

    public List<Platform> platformList;

    public Platform nowPlatform;

    public PlatformLogic(Movement moveObj)
    {
        this.moveObj = moveObj;
        platformList = PlatformDataMgr.Instance.platformList;
        nowPlatform = null;
    }

    public void UpdateCheck()
    {
        if(moveObj.isJump || moveObj.isFall)
        {
            nowPlatform = null;
            foreach (Platform p in platformList)
            {
                if(p.IsOnMe(moveObj.transform.position) && (nowPlatform == null || nowPlatform.y <= p.y))
                {
                    nowPlatform = p;
                    moveObj.ChangeNowPlatform(p);
                }
            }
        }
        else
        {
            if (nowPlatform != null && !nowPlatform.IsOnMe(moveObj.transform.position))
            {
                moveObj.Fall();
            }

        }

    }
}
