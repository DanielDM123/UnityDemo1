using Project;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Card", menuName = "Card/Character")]
public class Character : Card
{
    public int health;
    public int damageMin;
    public int damageMax;

    public List<ElementType> damageType;
    public int range;
    public AttackPattern attackPattern;
    public PriorityTarget priorityTarget;
    public GameObject prefab;


 
}
