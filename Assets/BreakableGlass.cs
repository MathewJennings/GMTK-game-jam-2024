using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGlass : MonoBehaviour
{
    [SerializeField]
    private AudioSource glassBreakAudioSource;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.GetComponent<SizeScaler>().GetTransformScaleMultiplier() >= 10)
            {
                glassBreakAudioSource.PlayOneShot(glassBreakAudioSource.clip);
                Destroy(this.gameObject);
            }
        }
    }

}
