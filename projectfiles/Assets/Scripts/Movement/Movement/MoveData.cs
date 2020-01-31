﻿using UnityEngine;

namespace Fragsurf.Movement {

    public enum MoveType {
        None,
        Walk,
        Noclip, // not implemented
        Ladder, // not implemented
    }

    public class MoveData {

        ///// Fields /////
        
        public Transform playerTransform;
        public Transform viewTransform;
        public Vector3 viewTransformDefaultLocalPos;
        
        public Vector3 origin;
        public Vector3 viewAngles;
        public Vector3 velocity;
        public float forwardMove;
        public float sideMove;
        public float upMove;
        public float surfaceFriction = 1f;
        public float gravityFactor = 1f;
        public float walkFactor = 1f;
        public float verticalAxis = 0f;
        public float horizontalAxis = 0f;
        public bool wishJump = false;
        public bool crouching = false;
        public bool sprinting = false;

        public float rigidbodyPushForce = 1f;

        public float defaultHeight = 2f;
        public float crouchingHeight = 1f;
        public float crouchingSpeed = 10f;
        public bool toggleCrouch = false;

        public bool slidingEnabled = false;

        public bool underwater = false;
        public bool cameraUnderwater = false;

        public bool grounded = false;
        public bool groundedTemp = false;
        public float fallingVelocity = 0f;

    }
}
