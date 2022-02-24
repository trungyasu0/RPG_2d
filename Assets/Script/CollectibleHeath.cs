using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHeath : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        RubyController charController = other.GetComponent<RubyController>();

        if (charController && charController.CheckNeedHeath())
        {
            charController.ChangeHeath(1);
            Destroy(gameObject);
        }

        Debug.Log("object enter", other);
    }
}
