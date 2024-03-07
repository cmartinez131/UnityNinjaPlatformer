using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Settings")]
    [SerializeField, Range(1f, 3f)] private float maxIdleSpeed = 2;
    [SerializeField] private float maxTilt = 5;
    [SerializeField] private float tiltSpeed = 20;

    [Header("Particles")]
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private ParticleSystem landParticles;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] footsteps;

    private AudioSource source;
    private PlayerController playerController;
    private bool grounded;

    private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    private static readonly int JumpKey = Animator.StringToHash("Jump");

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        playerController.Jumped += OnJumped;
        playerController.GroundedChanged += OnGroundedChanged;
    }

    private void OnDisable()
    {
        playerController.Jumped -= OnJumped;
        playerController.GroundedChanged -= OnGroundedChanged;
    }

    private void Update()
    {
        // This presumes that you have a float parameter named "IdleSpeed" in your Animator to control the idle animation speed.
        anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, maxIdleSpeed, Mathf.Abs(playerController.CurrentFrameInput.x)));

        // This presumes you have a bool parameter named "Grounded" in your Animator to indicate if the player is grounded or not.
        anim.SetBool(GroundedKey, grounded);

        // Flip the sprite based on the direction of movement.
        if (playerController.CurrentFrameInput.x != 0)
        {
            sprite.flipX = playerController.CurrentFrameInput.x < 0;
        }
    }

    private void OnJumped()
    {
        anim.SetTrigger(JumpKey);
        jumpParticles.Play();
        PlayRandomFootstep();
    }

    private void OnGroundedChanged(bool isGrounded, float impact)
    {
        grounded = isGrounded;
        if (grounded)
        {
            landParticles.Play();
            PlayRandomFootstep();
        }
    }

    private void PlayRandomFootstep()
    {
        if (footsteps.Length > 0)
        {
            AudioClip clip = footsteps[Random.Range(0, footsteps.Length)];
            source.PlayOneShot(clip);
        }
    }
}
