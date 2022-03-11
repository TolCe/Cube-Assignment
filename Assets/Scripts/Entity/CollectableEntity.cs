using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CollectableEntity : PoolObject
{
    public CollectableVO VO;

    public void GetHit()
    {
        SetColliderActivity(false);
        PoolController.Instance.PutBackIntoPool(VO.ItemName.ToString(), gameObject);
    }
}
