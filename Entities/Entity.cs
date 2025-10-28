using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP423_Prasolova.Entities
{
    internal abstract class Entity
    {
        public int Hp;
        public int Damage;
        public abstract void GetDamage(int damage);
    }
}
