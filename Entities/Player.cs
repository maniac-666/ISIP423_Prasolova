using ISIP423_Prasolova.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP423_Prasolova.Entities
{
    internal class Player : Entity
    {
        public Armor ArmorPlayer;
        public Weapon WeaponPlayer;

        public static Player Instance;

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public override void GetDamage(int damage)
        {
            Hp -= damage - ArmorPlayer.Protection;
            ArmorPlayer.Durability--;
        }

        public void AttackEnemy(Enemy enemy)
        {
            enemy.GetDamage(WeaponPlayer.Damage);
            WeaponPlayer.Durability--;
        }
    }
}
