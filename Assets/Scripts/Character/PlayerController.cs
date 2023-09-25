using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int moveSpeed = 1;
    public float sprintSpeed = 1;

    private bool isMoving;
    private bool isSprinting;
    private Vector2 input;
    private Vector2 movement;
    Rigidbody2D rb2d;
    Animator animator;
    public static PlayerController i { get; private set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        i = this;
    }

    private void Update()
    {




    }

    private void FixedUpdate()
    {

    }

    public void HandleUpdate()
    {

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x != 0) input.y = 0;
        //remove diagonal movement
        //Debug.Log(input.x);
        //Debug.Log(input.y);

        movement.x = Mathf.Clamp(input.x, -1f, 1f);
        movement.y = Mathf.Clamp(input.y, -1f, 1f);
        //Debug.Log(movement.x);
        //Debug.Log(movement.y);
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector2.zero)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(InteractStart(input));
        }
    }

    IEnumerator InteractStart(Vector3 vector3)
    {
        var facingDir = new Vector3(vector3.x, vector3.y);
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, GameLayers.i.InteractableLayer);
        if (collider != null)
        {
            yield return collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    static async void DelaySample()
    {
        await Task.Delay(1000);
    }

    IPlayerTriggerable currentlyInTrigger;

    //private void OnMoveOver() //colliderの範囲内に入った時
    //{
    //    var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, charactor.OffsetY), 0.2f, GameLayers.i.TriggerableLayers);

    //    IPlayerTriggerable triggerable = null;

    //    foreach (var collider in colliders)
    //    {
    //        triggerable = collider.GetComponent<IPlayerTriggerable>();
    //        if (triggerable != null)
    //        {
    //            if (triggerable == currentlyInTrigger && !triggerable.TriggerRepeatedly)
    //                break;

    //            triggerable.OnPlayerTriggered(this);
    //            currentlyInTrigger = triggerable;
    //            break;
    //        }
    //    }

    //    if (colliders.Count() == 0 || triggerable != currentlyInTrigger)
    //        currentlyInTrigger = null;
    //}
}
