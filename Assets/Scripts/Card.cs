using UnityEngine;
using System.Collections.Generic;

namespace Project
{
    
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<ElementType> cardType;
        public Sprite cardSpirte;
        public string description;


        public enum ElementType
        {
            Fire,
            Earth,
            Water,
            Dark,
            Light,
            Air
        }



        public enum AttackPattern
        {
            Single,
            Multitarget,
            Cross,
            Column,
            Row,
            TwoByTwo,
            FourByFour

        }

        public enum PriorityTarget
        {
            Close,
            Far,
            LeastCurrentHealth,
            MostCurrentHealth,
            MostMaxHealth,
            MostDamage
        }

        public enum SpellType
        {
            Buff,
            Debuff
        }
        
        public enum AttributeTarget
        {
            health,
            damage,
            range,
            attackPattern,
            damangeType,
            cardType,
            priorityTarget
        }

    }
}
