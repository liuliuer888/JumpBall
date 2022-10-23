using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitTrigger : MonoBehaviour 
{
    public Action<Collision2D> OnHit;
    public Action<Collision2D> OnExitHit;

    void OnCollisionEnter2D(Collision2D co)
	{
        if (OnHit != null)
        {
            OnHit(co);
        }
	}

    void OnCollisionExit2D(Collision2D co)
    {
        if(OnExitHit != null)
        {
            OnExitHit(co);
        }
    }
}
