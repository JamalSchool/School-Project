using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Reference to the collider of the player's weapon.
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float swordAttackCD = 0.5f;

    // References to various components and input controls needed for the sword mechanics.
    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private bool attackButtonDown, isAttacking = false;

    

    // Awake is called when the script instance is being loaded.
    // It initializes component references and sets up input controls.
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    // OnEnable is called when the object becomes enabled and active.
    // It enables input controls.
    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Start is called before the first frame update.
    // It sets up an event listener for the attack action triggered by the player's input.
    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    // Update is called once per frame.
    // It ensures that the sword follows the mouse cursor with a specific offset.
    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown= false;
    }

    // Initiates the attack action.
    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking=true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            StartCoroutine(AttackCDRoutine());

        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    // Deactivates the weapon collider after the attack animation is finished.
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    // Makes the weapon follow the mouse cursor with a specific offset.
    private void MouseFollowWithOffset()
    {
        // Retrieve the current position of the mouse cursor in screen coordinates.
        Vector3 mousePos = Input.mousePosition;

        // Convert the player's position from world space to screen space.
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        // Calculate the angle between the player's position and the mouse cursor position.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // Check if the mouse cursor is to the left of the player's position on the screen.
        if (mousePos.x < playerScreenPoint.x)
        {
            // If the mouse cursor is on the left side, adjust the rotation of the active weapon and weapon collider to face towards the cursor with an offset.
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            // If the mouse cursor is on the right side, adjust the rotation of the active weapon and weapon collider to face towards the cursor with an offset.
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }
}
