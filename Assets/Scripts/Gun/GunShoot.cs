using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    Camera camera;

    public AudioSource fireAudioSource;
    public AudioSource failFireAudioSource;
    public AudioSource switchGunAudioSource;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotDuration = new WaitForSeconds(.13f);
    public enum Mode
    {
        SizeScale,
        MovementSpeedScale
    }
    private Mode currentMode;
    private List<Mode> disabledModes = new List<Mode>();
    public Mode GetMode()
    {
        return currentMode;
    }

    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Set Gun Mode
        if (Input.GetKeyDown(KeyCode.Alpha1) && !disabledModes.Contains(Mode.SizeScale))
        {
            SetGunMode(Mode.SizeScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !disabledModes.Contains(Mode.MovementSpeedScale))
        {
            SetGunMode(Mode.MovementSpeedScale);
        }

        // Shoot
        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            // Get world position of mouse.
            Vector3 mousePos = Input.mousePosition;
            // Offset the camera's z position.
            mousePos.z = camera.transform.position.z * -1;
            mousePos = camera.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position);

            // If the hit is self, we clicked between the tip of the gun and ourselves.
            // Try again with a raycast going in the other direction.
            if (hit && hit.transform.tag == "Player")
            {
                hit = Physics2D.Raycast(transform.position, transform.position - mousePos);
            }
            lineRenderer.SetPosition(0, Vector2.zero);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(mousePos));
            ProcessHit(hit);
        }
    }

    public void SetGunMode(Mode mode)
    {
        SetGunMode(mode, false);
    }

    public void SetGunMode(Mode mode, bool muteAudio)
    {
        if (!muteAudio)
        {
            PlayRandomizedPitchAudioClip(switchGunAudioSource);
        }
        currentMode = mode;
        Debug.Log("Updating gun mode to " + mode.ToString());
    }

    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        yield return shotDuration;
        lineRenderer.enabled = false;
    }

    public void DisableGunMode(Mode mode)
    {
        disabledModes.Add(mode);
    }
    public void EnableGunMode(Mode mode)
    {
        disabledModes.Remove(mode);
    }

    private bool CanShoot()
    {
        GameObject player = transform.parent.parent.gameObject;
        if (!player || player.tag != "Player") return false;

        bool scaleIsChanging = player.GetComponent<SizeScaler>().GetScaleIsChanging();
        Debug.Log(scaleIsChanging);
        if (scaleIsChanging)
        {
            return false;
        }

        return true;
    }

    private void ProcessHit(RaycastHit2D hit)
    {
        if (!hit)
        {
            StartCoroutine(ShotEffect());
            PlayRandomizedPitchAudioClip(failFireAudioSource);
            return;
        }

        lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
        StartCoroutine(ShotEffect());

        if (currentMode == Mode.SizeScale)
        {
            SizeScaler hitScaler = hit.transform.gameObject.GetComponent<SizeScaler>();
            if (hitScaler != null && hitScaler.DoesScale())
            {
                SizeScaler meScaler = gameObject.transform.parent.gameObject.GetComponentInParent<SizeScaler>();
                //if the scales are the same, don't bother swapping
                if(meScaler.GetTransformScaleMultiplier() == hitScaler.GetTransformScaleMultiplier())
                {
                    PlayRandomizedPitchAudioClip(failFireAudioSource);
                }
                else
                {
                    hitScaler.SwapTransformScaleMultiplier(meScaler);
                    PlayRandomizedPitchAudioClip(fireAudioSource);  
                }
            }
            else
            {
                PlayRandomizedPitchAudioClip(failFireAudioSource);
            }
        } else if (currentMode == Mode.MovementSpeedScale)
        {
            MovementSpeedScaler hitScaler = hit.transform.gameObject.GetComponent<MovementSpeedScaler>();
            MovementSpeedScaler meScaler = gameObject.transform.parent.gameObject.GetComponentInParent<MovementSpeedScaler>();
            //Don't swap multiplier if the target is not valid
            if(meScaler == null || hitScaler == null || !hitScaler.getIsMovable())
            {
                PlayRandomizedPitchAudioClip(failFireAudioSource);
                return;
            }
            else
            {
                PlayRandomizedPitchAudioClip(fireAudioSource);
            }
            //Don't swap multiplier if the scales are the same
            if(meScaler.getMultiplier() == hitScaler.getMultiplier())
            {
                PlayRandomizedPitchAudioClip(failFireAudioSource);
            }
            else
            {
                hitScaler.SwapMultiplier(meScaler);
            }
        } else
        {
            Debug.LogError("Current gun mode " + currentMode.ToString() + " not handled");
        }

    }
    private void PlayRandomizedPitchAudioClip(AudioSource audioSource)
    {
        audioSource.pitch = Random.Range(0.6f, 1f);
        audioSource.volume = Random.Range(0.6f, 1f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
