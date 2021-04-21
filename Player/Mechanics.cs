using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mechanics : MonoBehaviour
{
    [Header("Game Info")]
    public bool isPaused = false;

    [Header("Walk")]
    public float speed;
    public float horizontal;
    public float vertical;

    [Header("Run")]
    private float stamina;
    private float speedRun = 1;
    public float rest = 5;
    public bool canRun = true;
    public Animator _animator;

    [Header("Hit")]
    private bool canHit = true;
    private float recharge;
    public Weapon weaponEquipped;
    public SpriteRenderer weaponSprite;

    private Image weaponImage;
    private Atack atack;
    private PlayerStats _stats;

    private void Start()
    {
        Time.timeScale = 1.0f;
        SetWeapon();
        weaponImage = GameObject.Find("WeaponSprite").GetComponent<Image>();
        atack = GetComponentInChildren<Atack>();
        _stats = GetComponent<PlayerStats>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isPaused)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            if (horizontal != 0 || vertical != 0) Walk();
            else _animator.SetBool("running", false);
            if (canHit) Hit();
        }
    }

    void Walk()
    {
        float decrement = (horizontal != 0 && vertical != 0) ? Mathf.Cos(0.78f) : 1;
        transform.position += new Vector3(horizontal, vertical) * Time.deltaTime * speed * decrement * speedRun;
        if (horizontal > 0) transform.eulerAngles = new Vector3(0, 0);
        if (horizontal < 0) transform.eulerAngles = new Vector3(0, 180);
        _animator.SetBool("running", true);
        Run();
    }

    void Hit()
    {
        if (Input.GetMouseButtonDown(0) && canHit)
        {
            canHit = false;
            _animator.SetTrigger("hit");
            atack.PlayAnimation();
            StartCoroutine(HitRecharge());
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (canRun) StartCoroutine(RunOver());
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedRun = 1f;
        }
    }

    IEnumerator HitRecharge()
    {
        recharge = weaponEquipped.recharge;
        weaponImage.fillAmount = 0.0f;
        yield return new WaitForSeconds(recharge / 4);
        weaponImage.fillAmount = 0.25f;
        yield return new WaitForSeconds(recharge / 4);
        weaponImage.fillAmount = 0.50f;
        yield return new WaitForSeconds(recharge / 4);
        weaponImage.fillAmount = 0.75f;
        yield return new WaitForSeconds(recharge / 4);
        weaponImage.fillAmount = 1f;
        canHit = true;
    }
    IEnumerator RunOver()
    {
        canRun = false;
        speedRun = 1.5f;
        stamina = _stats.stamina;
        yield return new WaitForSeconds(stamina);
        speedRun = 1f;
        yield return new WaitForSeconds(rest);
        canRun = true;
    }

    public void AddWeapon(Weapon weapon)
    {
        weaponEquipped = weapon;
        atack.SetWeapon(weaponEquipped.damage);
        weaponImage.sprite = weaponEquipped.image;
        SetWeapon();
    }
    public void SetWeapon()
    {
        if (weaponEquipped.image != null) weaponSprite.sprite = weaponEquipped.image;
    }
}
