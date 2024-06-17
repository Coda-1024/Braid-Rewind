using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDataMgr : BaseManager<PlatformDataMgr>
{
    public List<Platform> platformList = new List<Platform>();

    public void AddPlatform(Platform p)
    {
        platformList.Add(p);
    }

    public void RemovePlatform(Platform p)
    {
        if(platformList.Contains(p))
        {
            platformList.Remove(p);
        }
    }

    public void ClearPlatform()
    {
        platformList.Clear();
    }
}
