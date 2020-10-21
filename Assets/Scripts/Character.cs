using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        BeginPunch,
        Punch,
        BeginDying,
        Dead,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Fist,
    }


    public Weapon weapon;
    public float runSpeed;
    public float distanceFromEnemy;
    public Transform target;
    public TargetIndicator targetIndicator;
    private AudioPlay audioPlay;
    public string ShootAudioName;
    public string HitFistAudioName;
    public string HitBatAudioName;
    public string HitAudioName;
    public string DeathAudioName;
    State state;
    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Health health;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        targetIndicator = GetComponentInChildren<TargetIndicator>(true);
        state = State.Idle;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        audioPlay = GetComponentInChildren<AudioPlay>();
    }

    public bool IsIdle()
    {
        return state == State.Idle;
    }

    public bool IsDead()
    {
        return state == State.BeginDying || state == State.Dead;
    }

    public void SetState(State newState)
    {
        if (IsDead())
            return;

        state = newState;
    }


    public void DoDamageToTarget()
    {    
        switch (weapon)
        {
            case Weapon.Bat:
                audioPlay.PlayAudio(HitBatAudioName);
                break;

            case Weapon.Fist:
                audioPlay.PlayAudio(HitFistAudioName);
                break;

            case Weapon.Pistol:
                audioPlay.PlayAudio(ShootAudioName);
                break;
        }
        Health healthT = target.GetComponentInChildren<Health>();
        Character targetcharacter = target.GetComponent<Character>();
        if (targetcharacter.IsDead())
            return;
        
        healthT.ApplyDamage(1.0f); // FIXME: захардкожено
        if(healthT.current >= 1.0f)
        targetcharacter.audioPlay.PlayAudio(HitAudioName);
        if (healthT.current <= 0.0f)
        {
            targetcharacter.state = State.BeginDying;
            targetcharacter.audioPlay.PlayAudio(DeathAudioName);
        }

        
                   
    }

    [ContextMenu("Attack")]
    public void AttackEnemy()
    {
        if (IsDead())
            return;

        Character targetCharacter = target.GetComponent<Character>();
        if (targetCharacter.IsDead())
            return;

        switch (weapon) {
            case Weapon.Bat:
                state = State.RunningToEnemy;
                break;

            case Weapon.Fist:
                state = State.RunningToEnemy;
                break;

            case Weapon.Pistol:
                state = State.BeginShoot;
                break;
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.00001f) {
            transform.position = targetPosition;
            return true;
        }

        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 step = direction * runSpeed;
        if (step.magnitude < distance.magnitude) {
            transform.position += step;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

    void FixedUpdate()
    {
        
        switch (state) {
            case State.Idle:
                transform.rotation = originalRotation;
                animator.SetFloat("Speed", 0.0f);
                break;

            case State.RunningToEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(target.position, distanceFromEnemy)) {
                    switch (weapon) {
                        case Weapon.Bat:
                            state = State.BeginAttack;
                            break;

                        case Weapon.Fist:
                            state = State.BeginPunch;
                            break;
                    }
                }
                break;

            case State.BeginAttack:
                animator.SetTrigger("MeleeAttack");
                state = State.Attack;
                var meleeeffect = target.GetComponentInChildren<BloodEffect>();
                meleeeffect.PlayEffect();

                break;

            case State.Attack:
                break;

            case State.BeginShoot:
                
                animator.SetTrigger("Shoot");
                StartCoroutine(nameof(OnCompliteAttackAnimation));
                var shootEffect = target.GetComponentInChildren<BloodEffect>();
                shootEffect.PlayEffect();
                state = State.Shoot;
                break;

            case State.Shoot:
                break;

            case State.BeginPunch:
                animator.SetTrigger("Punch");
                state = State.Punch;
                meleeeffect = target.GetComponentInChildren<BloodEffect>();
                meleeeffect.PlayEffect();
                break;

            case State.Punch:
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginDying:
                animator.SetTrigger("Death");
                state = State.Dead;
                break;

            case State.Dead:
                break;
        }
    }

    /*public IEnumerator OnCompliteAttackAnimation()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >  0.7f);
        var fireEffect = GetComponentInChildren<FireEffect>();
        fireEffect.PlayEffect();
    }*/
    public IEnumerator OnCompliteAttackAnimation()
    {
        System.Func<bool> x = () => animator.GetCurrentAnimatorStateInfo(0).IsName("m_pistol_shoot") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.1F;
        Debug.Log("1");
        yield return new WaitUntil(x);
        var fireEffect = GetComponentInChildren<FireEffect>();
        fireEffect.PlayEffect();
        Debug.Log("2");
    }
}
