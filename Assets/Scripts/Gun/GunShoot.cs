using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    Camera camera;

    [SerializeField]
    private Transform stealOrb;

    public AudioSource fireAudioSource;
    public AudioSource failFireAudioSource;
    public AudioSource switchGunAudioSource;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotDuration = new WaitForSeconds(.13f);

    private static Color BLUE = new Color(0, 100, 255);
    private static Color ORANGE = new Color(255, 90, 0);

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
        // Don't read inputs if time is frozen (i.e. we are paused).
        if (Time.timeScale <= 0)
        {
            return;
        }

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
        if (mode == Mode.MovementSpeedScale)
        {
            GetComponent<SpriteRenderer>().color = ORANGE;
        }
        if (mode == Mode.SizeScale)
        {
            GetComponent<SpriteRenderer>().color = BLUE;
        }
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
                    CreateStealOrbs(hit);
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
            //Don't swap multiplier if the scales are the same
            if (meScaler == null || hitScaler == null || !hitScaler.getIsMovable() ||
               meScaler.getMultiplier() == hitScaler.getMultiplier())
            {
                PlayRandomizedPitchAudioClip(failFireAudioSource);
                return;
            }
            else
            {
                hitScaler.SwapMultiplier(meScaler);
                CreateStealOrbs(hit);
                PlayRandomizedPitchAudioClip(fireAudioSource);
            }
        } else
        {
            Debug.LogError("Current gun mode " + currentMode.ToString() + " not handled");
        }

    }

    private void CreateStealOrbs(RaycastHit2D hit)
    {
        // Create an empty gameobject with position that the orbs can slerp to and from.
        GameObject hitPoint = new GameObject("hitPointEnd");
        hitPoint.transform.position = hit.point;
        hitPoint.transform.SetParent(hit.transform);

        Transform toOrb = Instantiate(stealOrb, transform.position, Quaternion.identity);
        toOrb.GetComponent<SpriteRenderer>().color = ORANGE;
        toOrb.GetComponent<StealOrbChase>().SetOrigin(transform);
        toOrb.GetComponent<StealOrbChase>().SetTarget(hitPoint.transform);

        Transform fromOrb = Instantiate(stealOrb, hit.transform.position, Quaternion.identity);
        fromOrb.GetComponent<SpriteRenderer>().color = BLUE;
        fromOrb.GetComponent<StealOrbChase>().SetOrigin(hitPoint.transform);
        fromOrb.GetComponent<StealOrbChase>().SetTarget(transform);

        // Destroy empty hitPoint object after 1 second.
        Destroy(hitPoint, 1f);
    }

    private void PlayRandomizedPitchAudioClip(AudioSource audioSource)
    {
        audioSource.pitch = Random.Range(0.6f, 1f);
        audioSource.volume = Random.Range(0.6f, 1f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
