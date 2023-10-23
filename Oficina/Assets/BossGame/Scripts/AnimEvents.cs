using System.Collections;
using System.Collections.Generic;
using BossGame.Scripts;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    public void IntroBoss1()
    {
        GetComponentInParent<BossController>().TerminouIntro();
    }

    public void GritoBoss()
    {
        GetComponentInParent<BossController>().Grito();
    }
}
