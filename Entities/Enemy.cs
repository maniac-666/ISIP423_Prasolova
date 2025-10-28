using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP423_Prasolova.Entities
{
    internal abstract class Enemy : Entity
    {
        public string Name;
        public int Protection;
        public int ArmDamage;

        public virtual void AttackPlayer()
        {
            Player.Instance.GetDamage(Damage);
        }

        public override void GetDamage(int damage)
        {
            Hp -= damage - Protection;
        }


    }
}
