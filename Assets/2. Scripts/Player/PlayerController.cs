using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerAttack attack;
    private PlayerDash dash;
    private PlayerParry parry;
    private PlayerHealth health;
    private PlayerInteract interact;
    private PlayerInventory inventory;
    private PlayerMap map;

    private Animator anim;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        dash = GetComponent<PlayerDash>();
        parry = GetComponent<PlayerParry>();
        health = GetComponent<PlayerHealth>();
        interact = GetComponent<PlayerInteract>();
        inventory = GetComponent<PlayerInventory>();
        map = GetComponent<PlayerMap>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movement.UpdateGroundCheck();

        if (Input.GetKeyDown(KeyCode.X))
            attack.DoAttack();

        if (Input.GetKeyDown(KeyCode.Z))
            parry.DoParry();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            dash.TryDash();

        if (Input.GetKeyDown(KeyCode.D))
            health.DoHealth();

        if (Input.GetKeyDown(KeyCode.F))
            interact.DoInteract();

        if (Input.GetKeyDown(KeyCode.I))
            inventory.ToggleInventory();

        if (Input.GetKeyDown(KeyCode.M))
            map.ToggleMap();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        movement.Move(horizontal);

        if (Input.GetButton("Jump"))
            movement.Jump();
    }
}
