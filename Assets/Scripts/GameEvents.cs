using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvents", menuName = "Project ESCAPE/Create GameEvents")]
public class GameEvents : ScriptableObject
{
    public Action<CollisionInteractable> onItemPickedUp;
}
