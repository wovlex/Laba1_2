using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1_2
{
    internal class Class1
    {
    }
}
public class CEnemyTemplate
{
    //Название противника
    string name;
    //Название иконки
    string iconName;
    //Атрибуты здоровья
    int baseLife;
    double lifeModifier;

    //Атрибуты золота за победу над противником
    int baseGold;
    double goldModifier;
    //Шанс на появление
    double spawnChance;
}
