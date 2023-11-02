using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class LinerMoving : MonoBehaviour
{
    private Transform object_rb;
    public float moveTime = 5.0f; //Anchor�Ԃ̈ړ�����
    public float defoltStopTime = 2.0f;�@ //Anchor�ł̒�~����
    public bool isSmoothMove = true;
    public GameObject[] AnchorPoints;

    private int nowAnchorIndex; //�Ō�ɒʉ߂���Ancor��index�ԍ�
    private int nextAnchorIndex;    //���ɖڎw��Ancor��index�ԍ�
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
            //�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
            if (Vector3.Distance(transform.position, nextPosition) > 0.01f)
            {
                //���ݒn���玟��AnchorPoint�ւ̃x�N�g�����쐬
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

                //����AnchorPoint�ֈړ�
                //object_rb.MovePosition(toVector);
                object_rb.position = (toVector);
                //transform.localEulerAngles = rotateVector;
            }
            //�ڕWAnchorPoint�ɓ���
            else
            {
                object_rb.position = (nextPosition);
                //���݂�AnchorPoint���X�V
                if (!isReturning)
                {
                    ++nowAnchorIndex;
                } else
                {
                    --nowAnchorIndex;
                }
                //���݂�AnchorPoint���z��̗��[�������ꍇ�t�����ֈړ�
                if (nowAnchorIndex + 1 >= AnchorPoints.Length)
                {
                    isReturning = true;
                }
                else if (nowAnchorIndex - 1 < 0)
                {
                    isReturning = false;
                }

                timer = 0f;�@//�^�C�}�[�̃��Z�b�g
                StartCoroutine(StopMoving()); //�ꎞ��~
            }
        }
        
    }

    private IEnumerator StopMoving()
    {
        isMoving = false;
        yield return new WaitForSeconds(defoltStopTime);
        isMoving = true;
    }


    //�f�o�b�O�p�@Scene�^�u��Anchor�Ԃɐ�������
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
