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
        Health target;
        float timeSinceLastAttack = 0;
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null) {return;}
            if(target.IsDead()) {return;}
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
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
            target.TakeDamage(weaponDamage);
        }  

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("StopAttack");
            target = null;
        }
        
    }
}