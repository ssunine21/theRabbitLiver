using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectInfo))]
public class Item : MonoBehaviour {

    private Player _player;
    public Player player {
        get {
            if (_player == null) 
                _player = GameManager.init.player;
            return _player;
        }
        set {
            _player = value;
        }
    }

}