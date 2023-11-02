using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class LinerMoving : MonoBehaviour
{
    private Transform object_rb;
    public float moveTime = 5.0f; //Anchor間の移動時間
    public float defoltStopTime = 2.0f;　 //Anchorでの停止時間
    public bool isSmoothMove = true;
    public GameObject[] AnchorPoints;

    private int nowAnchorIndex; //最後に通過したAncorのindex番号
    private int nextAnchorIndex;    //次に目指すAncorのindex番号
    private bool isReturning = false;
    private bool isMoving = true;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        object_rb = GetComponent<Transform>();
        nowAnchorIndex = 0;
        object_rb.position = AnchorPoints[0].transform.position;;
        //Debug.Log(object_rb.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReturning)
        {
            nextAnchorIndex = nowAnchorIndex + 1;
        } else
        {
            nextAnchorIndex = nowAnchorIndex - 1;
        }

        Vector3 nowPosition = AnchorPoints[nowAnchorIndex].transform.position;
        Vector3 nextPosition = AnchorPoints[nextAnchorIndex].transform.position;
        //Vector3 nextRotation = AnchorPoints[nextAnchorIndex].transform.localEulerAngles;
        if (isMoving)
        { 
            //目標ポイントとの誤差がわずかになるまで移動
            if (Vector3.Distance(transform.position, nextPosition) > 0.01f)
            {
                //現在地から次のAnchorPointへのベクトルを作成
                if (isSmoothMove)
                {
                    timer += Time.deltaTime;
                }
                else if (!isSmoothMove)
                {
                    timer += Time.deltaTime;
                }
                Vector3 toVector = Vector3.Lerp(nowPosition, nextPosition, Mathf.SmoothStep(0, 1, timer / moveTime));
                //Vector3 rotateVector = Vector3.Lerp(AnchorPoints[nowAnchorIndex].transform.localEulerAngles, nextRotation, Mathf.SmoothStep(0, 1, timer / moveTime));

                //次のAnchorPointへ移動
                //object_rb.MovePosition(toVector);
                object_rb.position = (toVector);
                //transform.localEulerAngles = rotateVector;
            }
            //目標AnchorPointに到着
            else
            {
                object_rb.position = (nextPosition);
                //現在のAnchorPointを更新
                if (!isReturning)
                {
                    ++nowAnchorIndex;
                } else
                {
                    --nowAnchorIndex;
                }
                //現在のAnchorPointが配列の両端だった場合逆方向へ移動
                if (nowAnchorIndex + 1 >= AnchorPoints.Length)
                {
                    isReturning = true;
                }
                else if (nowAnchorIndex - 1 < 0)
                {
                    isReturning = false;
                }

                timer = 0f;　//タイマーのリセット
                StartCoroutine(StopMoving()); //一時停止
            }
        }
        
    }

    private IEnumerator StopMoving()
    {
        isMoving = false;
        yield return new WaitForSeconds(defoltStopTime);
        isMoving = true;
    }


    //デバッグ用　SceneタブでAnchor間に線を引く
    void OnDrawGizmos()
    {
        if (AnchorPoints.Length >= 2)
        {
            for (int i = 0; i < (AnchorPoints.Length - 1); i++)
            {
                Gizmos.color = Color.red;
                Vector3 startPoint = AnchorPoints[i].transform.position;
                Vector3 endPoint = AnchorPoints[i + 1].transform.position;
                Gizmos.DrawLine(startPoint, endPoint);
            }
        }
    }
}
