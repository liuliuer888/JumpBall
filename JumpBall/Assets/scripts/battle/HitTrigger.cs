using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitTrigger : MonoBehaviour 
{
    public Action<Collision2D> OnHit;

    void OnCollisionEnter2D(Collision2D co)
	{
        OnHit (co);
	}
}
