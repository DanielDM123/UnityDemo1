using UnityEngine;
using Project;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Card/Spell")]
public class Spell : Card
{
    public SpellType spellType;
    public List<AttributeTarget> attributeTarget;
    public List<int> attributeChangeAmount;
}
