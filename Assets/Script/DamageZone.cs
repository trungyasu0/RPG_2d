using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage = 1;
    public void OnTriggerStay2D(Collider2D other)
    {
        RubyController charController = other.GetComponent<RubyController>();

        if (charController)
        {
            charController.TakeDamage(damage);
        }

    }
}
