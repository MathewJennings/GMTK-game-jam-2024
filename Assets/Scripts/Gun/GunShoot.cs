using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    Camera camera;

    public AudioSource fireAudioSource;
    public AudioSource switchGunAudioSource;
    public enum Mode
    {
        SizeScale,
        MovementSpeedScale
    }
    private Mode currentMode;

    public Mode GetMode()
    {
        return currentMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Set Gun Mode
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayRandomizedPitchAudioClip(switchGunAudioSource);
            SetGunMode(Mode.SizeScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayRandomizedPitchAudioClip(switchGunAudioSource);
            SetGunMode(Mode.MovementSpeedScale);
        }

        // handle rotation
        Vector3 mousePos = Input.mousePosition;
        // Offset the camera's z position.
        mousePos.z = camera.transform.position.z * -1;
        mousePos = camera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            PlayRandomizedPitchAudioClip(fireAudioSource);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position);
            ProcessHit(hit);
        }
    }

    private void PlayRandomizedPitchAudioClip(AudioSource audioSource)
    {
        audioSource.pitch = Random.Range(0.6f, 1f);
        audioSource.volume = Random.Range(0.6f, 1f);
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void SetGunMode(Mode mode)
    {
        currentMode = mode;
        Debug.Log("Updating gun mode to " + mode.ToString());
    }

    private void ProcessHit(RaycastHit2D hit)
    {
        if (!hit)
        {
            return;
        }

        
        if (currentMode == Mode.SizeScale)
        {
            SizeScaler hitScaler = hit.transform.gameObject.GetComponent<SizeScaler>();
            if (hitScaler != null)
            {
                SizeScaler meScaler = gameObject.transform.parent.gameObject.GetComponentInParent<SizeScaler>();
                hitScaler.SwapTransformScaleMultiplier(meScaler);
            }
        } else if (currentMode == Mode.MovementSpeedScale)
        {
            MovementSpeedScaler hitScaler = hit.transform.gameObject.GetComponent<MovementSpeedScaler>();
            MovementSpeedScaler meScaler = gameObject.transform.parent.gameObject.GetComponentInParent<MovementSpeedScaler>();
            //Don't swap multiplier if the target is not valid
            if(meScaler == null || hitScaler == null)
            {
                return;
            }
            hitScaler.SwapMultiplier(meScaler);
        } else
        {
            Debug.LogError("Current gun mode " + currentMode.ToString() + " not handled");
        }
        
    }
}
