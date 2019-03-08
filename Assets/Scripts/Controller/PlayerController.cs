using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Inputs")] public float horizontal;
        public float vertical;
        public bool jump;

        [Header("Stats")] public float moveSpeed = 2.0f;
        public float heightOffset = 0.2f;

        [Header("States")] public bool onGround;

        public LayerMask groundMask;

        private Animator _anim;
        private Rigidbody2D _rigid;
        private SpriteRenderer _sprite;

        #region Animator Properties

        private static readonly int HashSpeed = Animator.StringToHash("Speed");
        private static readonly int HashFallSpeed = Animator.StringToHash("FallSpeed");
        private static readonly int HashGroundDistance = Animator.StringToHash("GroundDistance");

        #endregion

        public void Init()
        {
            _anim = GetComponent<Animator>();
            _anim.applyRootMotion = false;

            _rigid = GetComponent<Rigidbody2D>();
            _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

            _sprite = GetComponent<SpriteRenderer>();
        }

        public void Tick()
        {
            var jumpForce = _rigid.velocity.y;
            if (jump && onGround)
                jumpForce = 5f;
            
            _rigid.velocity = new Vector2(horizontal * moveSpeed, jumpForce);

            var groundDist = GroundCheck();
            
            _anim.SetFloat(HashGroundDistance, groundDist);
            _anim.SetFloat(HashFallSpeed, _rigid.velocity.y);
            _anim.SetFloat(HashSpeed, Mathf.Abs(horizontal));

            if (!Mathf.Approximately(horizontal, 0f))
                _sprite.flipX = horizontal < 0;
        }

        private float GroundCheck()
        {
            var distanceFromGround = Physics2D.Raycast(transform.position, Vector2.down, 1, groundMask);

            var noHit = Mathf.Approximately(distanceFromGround.distance, 0f);
            onGround = !noHit && distanceFromGround.distance <= 0.3f;
            
            return noHit ? 99f : distanceFromGround.distance - heightOffset;
        }
    }
}
