using Crypt.Core;
using Crypt.Movement;
using UnityEngine;

namespace Crypt.Combat
{
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0;
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null) {return;}
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackMethod();
            }
        }

        private void AttackMethod()
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                 GetComponent<Animator>().SetTrigger("Attack");
                 timeSinceLastAttack = 0;
               //triggers the Hit() event.
            }
        }

           //Animation Event
         void Hit()
        {
            Health healthCompenet = target.GetComponent<Health>();
            healthCompenet.TakeDamage(weaponDamage);
        }  

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
        
    }
}