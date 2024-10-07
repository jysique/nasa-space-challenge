using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAnimator : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset jump;
    public AnimationReferenceAsset activate;
    public AnimationReferenceAsset deactivate;

    public string currentState;
    public string currentAnimation;

    private PlayerMovement pm;
    private Vector2 moveInput;

    public bool air;


    //sfx
    public AudioSource aSource;
    public AudioSource carLoop;
    public AudioClip transformerSound;

    [SerializeField] private bool changeShape;
    // Start is called before the first frame update
    void Start()
    {
        //changeShape = true;

        pm = transform.parent.GetComponent<PlayerMovement>();
        currentState = "Idle";
        SetCharacterState(currentState);

        var firstTrackEntry = skeletonAnimation.state.SetAnimation(0, activate, false); 


        // Registrar un callback para que al terminar la primera animación se reproduzca la segunda
        firstTrackEntry.Complete += (trackEntry) =>
        {
            changeShape = false;
            SetCharacterState(currentState);
        };

    }

    private void OnEnable()
    {
        aSource.PlayOneShot(transformerSound);
        skeletonAnimation.state.SetAnimation(0, activate, false).Complete += (trackEntry) =>
        {
            
            changeShape = false;
            SetCharacterState(currentState);
        };
    }
    private void OnDisable()
    {
        changeShape = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (changeShape)
            {
                print("close menu");

                var firstTrackEntry = skeletonAnimation.state.SetAnimation(0, activate, false);


                // Registrar un callback para que al terminar la primera animación se reproduzca la segunda
                firstTrackEntry.Complete += (trackEntry) =>
                {
                    changeShape = false;
                    SetCharacterState("Run");
                };
            }
            else
            {
                changeShape = true;
                skeletonAnimation.state.SetAnimation(0, deactivate, false);
            }
            
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    changeShape = false;
        //    //SetCharacterState(currentState);
        //}
        moveInput.x = Input.GetAxisRaw("Horizontal");
        if (!air)
        {
            var maxVolume = .2f;
            carLoop.volume = maxVolume * Mathf.Abs(moveInput.x);
        }
        if (!changeShape)
        {
            if (!pm.Grounded && !air)
            {
                SetCharacterState("Jump");
            }
            else
            {
                if (Mathf.Abs(moveInput.x) > 0.01f)
                {

                    SetCharacterState("Run");
                }
                else
                {
                    SetCharacterState("Idle");
                }
            }
        }
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }


    public void SetCharacterState(string state)
    {
        if (state.Equals("Jump"))
        {
            SetAnimation(jump, false, 1f);
        }
        if (state.Equals("Idle")){
            SetAnimation(idle, true, 1f);

        }
        if (state.Equals("Run"))
        {
            SetAnimation(run, true, 1f);
        }
        
        currentState = state;
    }
}
