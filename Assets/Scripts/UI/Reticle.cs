using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    [SerializeField] private Wire _wire;
    private RawImage reticle;
    enum Judge
    {
        Long,
        Short,
        Zero
    }

    private Judge judge = Judge.Zero;
    

    // Start is called before the first frame update
    void Start()
    {
        reticle = gameObject.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(_wire.cam.position, _wire.cam.forward, out _wire.hit);

        if (HasChanged())
        {
            switch (judge)
            {
                case Judge.Long:
                    reticle.color = Color.red;
                    break;
                case Judge.Short:
                    reticle.color = Color.green;
                    break;
                case Judge.Zero:
                    reticle.color = Color.red;
                    break;
            }
        }


        
    }
    

    bool HasChanged()
    {
        Judge temp = Judge.Zero;
        bool hasChangeJudge = false;
        if (_wire.hit.distance >= 20) temp = Judge.Long;
        if (_wire.hit.distance <= 20) temp = Judge.Short;
        if (_wire.hit.distance == 0) temp = Judge.Zero;
        if (temp != judge)
        {
            judge = temp;
            hasChangeJudge = true;
        }

        return hasChangeJudge;
    }
}
