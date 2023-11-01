using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class StormLighter : MonoBehaviour
{

    #region Fields
    private Animator _animator;
    //The target particel system
    private ParticleSystem _particleSystem;

    public Light light;
    //The audio source to play the sound effects
    private AudioSource _audioSource;
    //Contains all sound effects
    private AudioClip[] _audioClips;
    //Determines if the lighter is open or not
    private bool _isOpen;

    public bool isActive = true;

    UnderWay inputActions;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        inputActions = new UnderWay();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    //Start
    void Start()
    {
        inputActions.Player.FlashLight.performed += FlashLight_performed;
        _isOpen = false;

        try
        {
            _animator = GetComponent<Animator>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _particleSystem.gameObject.SetActive(false);

            _audioClips = new AudioClip[3];
            _audioClips[0] = (AudioClip)Resources.Load("Sounds/Clap_Open", typeof(AudioClip));
            _audioClips[1] = (AudioClip)Resources.Load("Sounds/Clap_Close", typeof(AudioClip));
            _audioClips[2] = (AudioClip)Resources.Load("Sounds/Fire_On", typeof(AudioClip));

            _audioSource = GetComponent<AudioSource>();

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.Message);
        }


    }

    public void FlashLight_performed(InputAction.CallbackContext obj)
    {
        if (isActive)
        {
            LighterOpenOrClose();
        }
    }

    public void LighterOpenOrClose()
    {
        if (_isOpen)
        {
            _animator.SetTrigger("Close");
            _particleSystem.gameObject.SetActive(false);
            _isOpen = false;
            light.gameObject.SetActive(false);
        }
        else
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                _animator.SetTrigger("Open");
                _isOpen = true;
                _particleSystem.gameObject.SetActive(true);
                light.gameObject.SetActive(true);
            }
        }
    }

    public void PlayOpenSound()
    {
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
    }

    public void PlayCloseSound()
    {
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();
    }

    public void PlayTurnOnSound()
    {
        _audioSource.clip = _audioClips[2];
        _audioSource.Play();
    }

    #endregion
}
