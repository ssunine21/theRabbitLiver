using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 스킬 : jump attack,
// 맞거나 죽을 때 : Shoulder Hit And Fall, Head Hit

public class Player : MonoBehaviour {
	private readonly int hashMoveRight = Animator.StringToHash("moveRight");
	private readonly int hashMoveLeft = Animator.StringToHash("moveLeft");
	private readonly int hashHitTheTrap = Animator.StringToHash("hitTheTrap");
	private readonly int hashDead = Animator.StringToHash("dead");
	private readonly int hashSkill = Animator.StringToHash("skill");
	private readonly int hashReverse = Animator.StringToHash("reverse");

	private readonly float CHAR_DIRECTION = 0f;
	private readonly float TRAP_DAMAGE = 0.1f;
	private readonly float AMOUNT_RECOVERY_HP_ON_KILL = 0.06f;
	private readonly float AMOUNT_RECOVERY_MP_ON_KILL = 0.2f;

	[Range(0, 100)]
	public float hitDelay;

	public Stamina stamina;
	private ParticleSystem protectionParticle;
	private ParticleSystem healParticle;
	private ParticleSystem hitParticle;

	public bool isGroggy = false;
	public bool isSuperCharge = false;
	public bool isDead = false;

	private bool _isProtection;
	public bool IsProtection {
		get => _isProtection;
		set {
			if (protectionParticle == null) return;

			if (value) protectionParticle.Play();
			else protectionParticle.Stop();
			_isProtection = value;
        }
    }

	public bool isHeal {
		set {
			if(value) {
				if (healParticle != null) {
					healParticle.Play();
				}
            }
        }
    }

	private bool _hittingByTrap = false;
	public bool hittingByTrap {
		get { return _hittingByTrap; }
		set {
			_hittingByTrap = value;
			isGroggy = value;
		}
	}
	private float crashErrorRange = 0.05f;
	private Vector3 posAfterHit;

	public Animator animator;

	public ICharacter iCharacter;

    private void Awake() {
		healParticle = Instantiate(Resources.Load<ParticleSystem>("Particle/Healing"), transform);
		hitParticle = Resources.Load<ParticleSystem>("Particle/Hit_Smoke");
		protectionParticle = Instantiate(Resources.Load<ParticleSystem>("Particle/Shield"), transform);
	}

    private void Start() {
		animator = GetComponent<Animator>();
		iCharacter = GetComponent<ICharacter>();

		iCharacter.GameSetting();
		stamina.skillUseCount = iCharacter.SkillCount();

	}

	private void Update() {
		if (!isGroggy && !isDead) {
			if (Input.touchCount > 0) {
				if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {

					if (Input.GetTouch(0).phase == TouchPhase.Began) {
						var touchPoint = Input.GetTouch(0).position;
						Move(Camera.main.ScreenToViewportPoint(touchPoint).x < 0.5);
					}
				}
			} else if (Input.GetMouseButtonDown(0)) {
				if (!EventSystem.current.IsPointerOverGameObject()) {
					var touchPoint = Input.mousePosition;
					Move(Camera.main.ScreenToViewportPoint(touchPoint).x < 0.5);
				}
			} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				Move(true);
			} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
				Move(false);
			}
		}

		if (stamina.hpBar <= 0 && !isDead) {
			StartCoroutine(nameof(Dead));
		}
	}

	private IEnumerator Dead() {
		isDead = true;
		isSuperCharge = true;
		animator.SetTrigger(hashDead);

		yield return new WaitForSeconds(2f);
		if (GameManager.init.Chance) {
			UIManager.init.OpenRestartUI();
			GameManager.init.Chance = false;
		} else {
			GameManager.init.FinishGame();
		}
	}

	public void StartReverse() {
		StartCoroutine(nameof(Reverse));
    }

	private IEnumerator Reverse() {
		animator.SetTrigger(hashReverse);
		stamina.SetStamina(0.7f, 0);
		UIManager.init.CloseRestartUI();
		yield return new WaitForSeconds(2f);
		UIManager.init.RestartCount();
    }

	public void reverseWhenEndDeathAnimation() {
		isDead = false;
		isSuperCharge = false;
    }

	private void FixedUpdate() {
		if (hittingByTrap) {
			this.transform.position = Vector3.Lerp(this.transform.position, posAfterHit, Time.deltaTime * hitDelay);

			if (transform.position.z <= (posAfterHit.z + crashErrorRange)) {
				transform.position = posAfterHit;
				if (this.transform.position.z % 3 != 0) {
					Vector3 vector3 = this.transform.position;
					vector3.z -= (this.transform.transform.position.z % 3);
				}

				if (!isDead) {
					hittingByTrap = false;
					isSuperCharge = false;
				}
			}
		}

		if (!isDead) {
			float hpDecreaseSpeed = SpawnManager.init.levelDesign.hpDecreasingSpeed;
			stamina.hpBar -= SpawnManager.init.levelDesign.hpDecreasingSpeed * iCharacter.hpDecreasing * Time.deltaTime;
		}
	}

	private void Move(bool isLeft) {
		Vector3 offset = new Vector3(0, this.transform.position.y, 0);
		Vector3 quaternion = new Vector3();

		offset.z = this.transform.position.z + Definition.TILE_SPACING;
		GameManager.init.recordData.runCount++;

		if (isLeft) {
			animator.SetTrigger(hashMoveLeft);

			quaternion.y = -CHAR_DIRECTION;
			if (this.transform.position.x == 0)
				offset.x = Definition.TILE_SPACING * 2;
			else
				offset.x = this.transform.position.x - Definition.TILE_SPACING;
		} else {
			animator.SetTrigger(hashMoveRight);

			quaternion.y = CHAR_DIRECTION;
			if (this.transform.position.x == Definition.TILE_SPACING * 2)
				offset.x = 0;
			else
				offset.x = this.transform.position.x + Definition.TILE_SPACING;
		}
		SoundManager.init.PlaySFXSound(Definition.SoundType.Move);

		this.transform.position = offset;
		this.transform.rotation = Quaternion.Euler(quaternion);

		DataManager.init.score.currScore += 50;


		if (GameManager.init.recordData.runCount > 1350)
			GameManager.init.Goal();
	}

	public void Move() {
		Vector3 offset = new Vector3(this.transform.position.x, this.transform.position.y, 0);
		offset.z = this.transform.position.z + Definition.TILE_SPACING;
		GameManager.init.recordData.runCount++;

		animator.SetTrigger(hashMoveLeft);
		this.transform.position = offset;
		DataManager.init.score.currScore += 50;
	}

	public void OnProtection() {
		IsProtection = true;
    }

	private void OnTriggerEnter(Collider collision) {

		if (collision.gameObject.CompareTag(Definition.TAG_ENEMY)) {
			CollisionWithEnemy(collision);
		}
		else if (collision.gameObject.CompareTag(Definition.TAG_ITEM)) {
			IItem item = collision.GetComponent<IItem>();
			item.Use();
			Destroy(collision.gameObject);
        }
		
		else if (collision.gameObject.CompareTag(Definition.TAG_ATTACK_COLLIDER)) {
			CollisionWithAttack(collision);
		}
	}

	private void CollisionWithEnemy(Collider collision) {
		if (collision.GetComponent<Wolf>() != null) return;

		SoundManager.init.PlaySFXSound(Definition.SoundType.Hit);
		stamina.hpBar += AMOUNT_RECOVERY_HP_ON_KILL;
		stamina.mpBar += (AMOUNT_RECOVERY_MP_ON_KILL + iCharacter.mpIncreasing);
		GameManager.init.recordData.enemyKill++;

		if (hitParticle != null) {
			Instantiate(hitParticle, new Vector3(transform.position.x, .8f, transform.position.z), Quaternion.identity);
		}
		Destroy(collision.gameObject);
	}

	private void CollisionWithAttack(Collider collider) {
		if (!isSuperCharge) {
			if (!IsProtection) {
				Hitting(collider);
				GameManager.init.recordData.hitCount++;
				stamina.hpBar -= 0.1f;
				SoundManager.init.PlaySFXSound(Definition.SoundType.Hitted);
			} else {
				IsProtection = false;
				SoundManager.init.PlaySFXSound(Definition.SoundType.Hitted_Shield);
			}
		}
	}

	private void Hitting(Collider collider) {
		posAfterHit = this.transform.position;
		try {
			posAfterHit.z -= (Definition.TILE_SPACING * collider.GetComponent<AttackColliderInfo>().force_KnockBack);
		} catch (Exception e) {
#if(DEBUG)
			UnityEngine.Debug.LogError(e.StackTrace);
#endif
		}
		animator.SetTrigger(hashHitTheTrap);

		hittingByTrap = true;
		isSuperCharge = true;
	}

	public bool Skill() {
		if(iCharacter.Skill()) {
			animator.SetTrigger(hashSkill);
			return true;
		}
		return false;
	}
}