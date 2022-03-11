using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObstacleEntity : PoolObject
{
    public ObstacleVO VO;

    public void GetHit()
    {
        SetColliderActivity(false);
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (transform.position.y > -2f)
        {
            transform.Translate(2 * Vector3.down * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        PoolController.Instance.PutBackIntoPool(VO.ItemName.ToString(), gameObject);
    }
}
