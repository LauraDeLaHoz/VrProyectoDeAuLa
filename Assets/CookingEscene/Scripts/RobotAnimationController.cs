using UnityEngine;

public class RobotAnimationController : MonoBehaviour
{
    [Header("Robot Animator")]
    public Animator animator;

    [Header("Animation States")]
    public string[] animationStates =
    {
        "StaticIdle",
        "Run",
        "Jump_Start",
        "Death"
    };

    private int currentAnimation = 0;

    public void ActivateRobot()
    {
        gameObject.SetActive(true);
    }

    public void NextAnimation()
    {
        if (animator == null)
        {
            Debug.LogWarning("No hay Animator asignado al RobotAnimationController.");
            return;
        }

        currentAnimation++;

        if (currentAnimation >= animationStates.Length)
        {
            currentAnimation = 0;
        }

        animator.Play(animationStates[currentAnimation], 0, 0f);

        Debug.Log("Animación actual: " + animationStates[currentAnimation]);
    }
}
