using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public Sprite cardSpirte;
        public DamageType damageType;


        public enum CardType
        {
            Fire,
            Earth,
            Water,
            Dark,
            Light,
            Air
        }

        public enum DamageType
        {
            Fire,
            Earth,
            Water,
            Dark,
            Light,
            Air
        }
    }
}
