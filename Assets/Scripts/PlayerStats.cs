using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Player Stats", menuName = "Create Player Attributes", order = 0)]
public class PlayerStats : ScriptableObject {
    [SerializeField] float xMovementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float slapForce;
}