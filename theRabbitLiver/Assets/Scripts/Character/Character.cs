using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour {
    protected static readonly int MOVE_OFFSET = 3;

    protected Player player;
    abstract public void Skill();
}