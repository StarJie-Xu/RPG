using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private Player player; 
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colerLoosingSpeed;

    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;
    private int facingDir = 1;




    private bool canDuplicateClone;
    private float chanceToDuplicate;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colerLoosingSpeed));

            if (sr.color.a <= 0)
              Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform,float _cloneDuration,bool _canAttack,Vector3 _offset,Transform _closestEnemy,bool _canDuplicate,float _chanceToDuplicate,Player _player)
    {
        if(_canAttack)
            anim.SetInteger("AttackNumber",Random.Range(1 , 4));

        player = _player;

        Vector3 adjustedPosition = new Vector3(
             _newTransform.position.x,
             _newTransform.position.y - 0.4f, // 根据需要调整这个值
             _newTransform.position.z
         );
        transform.position = adjustedPosition + _offset;
        cloneTimer = _cloneDuration;

        closestEnemy = _closestEnemy;
        canDuplicateClone = _canDuplicate;
        chanceToDuplicate = _chanceToDuplicate;
        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
       cloneTimer = -.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position,attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {

                player.stats.DoDamage(hit.GetComponent<CharacterStats>());

                if(canDuplicateClone)
                {
                    if(Random.Range(0,100) < chanceToDuplicate)
                    {
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(.5f * facingDir, 0.5f));
                    }
                }
            }
        }
    }

    private void FaceClosestTarget()
    {
 
            if (closestEnemy != null)
            {
               if(transform.position.x > closestEnemy.position.x)
            {


                  facingDir = -1; 
                  transform.Rotate(0, 180, 0);
            }
            }

        }
    }


