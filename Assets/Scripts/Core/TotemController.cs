using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TotemController : MonoBehaviour
{
    public TotemCompletion totem;
    public Transform[] recipients;
    PlayerInput playerInput;
    Vector3 orbScale;
    private bool isEventSent;
    
    [SerializeField]
    public AudioSource totemTrack; 

    public AudioClip[] audioClipArray;

    [SerializeField]
    public AudioSource orbEnter; 
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();

    }

    void Start()
    {
        recipients = transform.Find("Recipients").GetComponentsInChildren<Transform>();
        orbScale = new Vector3(4f, 4f, 4f);
        // totemTrack.clip = audioClipArray[0];
        totemTrack.Play();
    }

    // Update is called once per frame
    void Update()
    {


        if (!totem.isPickup && totem.orb && Vector3.Distance(transform.position, totem.orb.transform.position) <= 5f)
        {
                for (int i = 0; i < 2; i++)
                {
                    if (recipients[i].childCount == 0 && totem.orb.CompareTag(totem.name))
                    {
                        totem.orb.transform.parent = recipients[i];
                        totem.orb.GetComponent<Rigidbody>().isKinematic = true;
                        orbEnter.Play();
                        totem.orb.transform.DOLocalMove(new Vector3(0, 0, 0), 2);
                        totem.orb.transform.DOScale(orbScale, 1);
                        // ici arreter clignote 
                        // ici sop pulse son 
                        // totem.orb.pulseSource.Stop();

                        // totemTrack = orbTrack
                        // totemTrack = totem.orb.orbTrack;
                        // Debug.Log(totem.orb.orbEnter);

                        if (recipients[0].childCount == 1 && recipients[1].childCount == 0) {
                            totemTrack.clip = audioClipArray[1];
                            Debug.Log("CHANGE LE SON == HALF");
                        } else if (recipients[0].childCount == 1 && recipients[1].childCount == 1) {
                            totemTrack.clip = audioClipArray[2];

                        }

                        totemTrack.Play();

                    }
                    else if (!totem.orb.CompareTag(totem.name))
                    {
                        totem.orb.transform.DOShakeRotation(0.5f, 0.5f, 10, fadeOut: true);
                    }
                    if (checkTotem() && !isEventSent)
                    {
                        totem.completeTotem(true,totem.name);
                        isEventSent = true;
                    }
                }
        }
    }


    public bool checkTotem()
    {
        for (int i = 0; i < recipients.Length; ++i)
        {
            if (recipients[i].childCount<1)
            {
                return false;
            }
        }

        return true;
    }
    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();

    }
}

