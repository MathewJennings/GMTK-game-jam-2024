using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunPickup : MonoBehaviour
{

    [SerializeField]
    private GunShoot.Mode modeToEnable;
    [SerializeField]
    private Transform instructionsCanvas;

    [SerializeField]
    private Sprite scientistWithoutArms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            int numPlayerChildren = collision.transform.childCount;
            SpriteRenderer playerSpriteRenderer = null;
            for (int i = 0; i < numPlayerChildren; i++)
            {
                GameObject playerChild = collision.transform.GetChild(i).gameObject;
                if (playerChild.tag.Equals("Gun"))
                {
                    playerChild.SetActive(true);
                    GunShoot gunShoot = playerChild.GetComponentInChildren<GunShoot>();
                    gunShoot.EnableGunMode(modeToEnable);
                    gunShoot.SetGunMode(modeToEnable);
                    this.gameObject.SetActive(false);
                }
                if (playerChild.name.Equals("PlayerSprite"))
                {
                    playerChild.GetComponent<SpriteRenderer>().sprite = scientistWithoutArms;
                }
            }

            if (instructionsCanvas)
            {
                instructionsCanvas.gameObject.SetActive(true);
            }
        }
    }
}
