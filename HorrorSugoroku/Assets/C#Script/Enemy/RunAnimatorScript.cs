using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class RunAnimationScript : MonoBehaviour
    {
        private Animator Anim;
        private CharacterController Ctrl;
        private Vector3 MoveDirection = Vector3.zero;
        // Cache hash values
        private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
        private static readonly int RunState = Animator.StringToHash("Base Layer.ghost_run");
        private static readonly int SurprisedState = Animator.StringToHash("Base Layer.ghost_suprised");

        // moving speed
        [SerializeField] private float Speed = 4;

        // Random surprise animation
        [SerializeField] private float minSurpriseInterval = 5f;
        [SerializeField] private float maxSurpriseInterval = 15f;
        private float nextSurpriseTime;

        void Start()
        {
            Anim = this.GetComponent<Animator>();
            Ctrl = this.GetComponent<CharacterController>();
            SetNextSurpriseTime();
        }

        void Update()
        {
            MOVE();
            GRAVITY();
            CheckSurprise();
        }

        //---------------------------------------------------------------------
        // for character moving
        //---------------------------------------------------------------------
        private void MOVE()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MOVE_Velocity(new Vector3(0, 0, Speed), new Vector3(0, 0, 0));
                Anim.CrossFade(RunState, 0.1f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MOVE_Velocity(new Vector3(0, 0, -Speed), new Vector3(0, 180, 0));
                Anim.CrossFade(RunState, 0.1f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MOVE_Velocity(new Vector3(-Speed, 0, 0), new Vector3(0, -90, 0));
                Anim.CrossFade(RunState, 0.1f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MOVE_Velocity(new Vector3(Speed, 0, 0), new Vector3(0, 90, 0));
                Anim.CrossFade(RunState, 0.1f, 0, 0);
            }
            else
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }

        //---------------------------------------------------------------------
        // value for moving
        //---------------------------------------------------------------------
        private void MOVE_Velocity(Vector3 velocity, Vector3 rot)
        {
            MoveDirection = new Vector3(velocity.x, MoveDirection.y, velocity.z);
            if (Ctrl.enabled)
            {
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
            MoveDirection.x = 0;
            MoveDirection.z = 0;
            this.transform.rotation = Quaternion.Euler(rot);
        }

        //---------------------------------------------------------------------
        // gravity for fall of this character
        //---------------------------------------------------------------------
        private void GRAVITY()
        {
            if (Ctrl.enabled)
            {
                if (CheckGrounded())
                {
                    if (MoveDirection.y < -0.1f)
                    {
                        MoveDirection.y = -0.1f;
                    }
                }
                MoveDirection.y -= 9.81f * Time.deltaTime;
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
        }

        //---------------------------------------------------------------------
        // whether it is grounded
        //---------------------------------------------------------------------
        private bool CheckGrounded()
        {
            if (Ctrl.isGrounded && Ctrl.enabled)
            {
                return true;
            }
            Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
            float range = 0.2f;
            return Physics.Raycast(ray, range);
        }

        //---------------------------------------------------------------------
        // check for surprise animation
        //---------------------------------------------------------------------
        private void CheckSurprise()
        {
            if (Time.time >= nextSurpriseTime)
            {
                Anim.CrossFade(SurprisedState, 0.1f, 0, 0);
                SetNextSurpriseTime();
            }
        }

        //---------------------------------------------------------------------
        // set next surprise time
        //---------------------------------------------------------------------
        private void SetNextSurpriseTime()
        {
            nextSurpriseTime = Time.time + Random.Range(minSurpriseInterval, maxSurpriseInterval);
        }
    }
}