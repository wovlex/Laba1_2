using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2
{
    public class CEnemyTemplate
    {
        // Название противникаa
        public string Name { get; set; }
        // Название иконки
        public string IconName { get; set; }
        // Атрибуты здоровья
        public int BaseLife { get; set; }
        public double LifeModifier { get; set; }
        // Атрибуты золота за победу над противником
        public int BaseGold { get; set; }
        public double GoldModifier { get; set; }
        // Шанс на появление
        public double SpawnChance { get; set; }

        // Можно добавить конструктор
        public CEnemyTemplate(string name, string iconName, int baseLife, double lifeModifier,int baseGold, double goldModifier, double spawnChance)
        {
            Name = name;
            IconName = iconName;
            BaseLife = baseLife;
            LifeModifier = lifeModifier;
            BaseGold = baseGold;
            GoldModifier = goldModifier;
            SpawnChance = spawnChance;
        }
    }
}

