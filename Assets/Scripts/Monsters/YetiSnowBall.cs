using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiSnowBall : MonoBehaviour {
    private Transform Target;
    public float firingAngle = 30.0f;
    public float gravity = 9.8f;

    public float shotSpeed;

    private void OnEnable() {
        Target = GameManager.init.player.transform;
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile() {

        float target_Distance = Vector3.Distance(this.transform.position, Target.position);
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        float flightDuration = target_Distance / Vx;

        this.transform.rotation = Quaternion.LookRotation(Target.position - this.transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration) {
            this.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}