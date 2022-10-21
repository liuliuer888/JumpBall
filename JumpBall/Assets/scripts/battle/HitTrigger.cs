using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitTrigger : MonoBehaviour 
{
    public Action<Collision2D> OnHit;

    void OnColliderEnter2D(Collision2D co)
	{
        OnHit (co);
	}
}
