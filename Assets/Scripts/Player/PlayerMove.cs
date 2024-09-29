using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// ÀÌµ¿ ¼Óµµ
    /// </summary>
    public float MoveSpeed = 7.0f;

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î ÄÁÆ®·Ñ·¯
    /// </summary>
    public CharacterController cc;

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î Áß·Â
    /// </summary>
    public float gravity = -20.0f;

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î ¼öÁ÷ ¼Ó·Â
    /// </summary>
    public float yVelocity = 0;

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î Á¡ÇÁ·Â
    /// </summary>
    public float jumpPower = 5.0f;

    /// <summary>
    /// ÇöÀç Á¡ÇÁ »óÅÂ
    /// </summary>
    public bool Isjumping = false;

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î Ã¼·Â
    /// </summary>
    public int hp = 50;

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î ÃÖ´ë Ã¼·Â
    /// </summary>
    public int Maxhp = 50;

    /// <summary>
    /// hp ½½¶óÀÌ´õ
    /// </summary>
    public Slider slider;

    /// <summary>
    /// Hit È¿°ú ¿ÀºêÁ§Æ®
    /// </summary>
    public GameObject hitEffect;


    Animator animator;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        // °ÔÀÓ »óÅÂ°¡ °ÔÀÓÁßÀÏ¶§¸¸ Á¶ÀÛ °¡´É
        if (GameManager.gm.state != GameManager.State.Play)
        {
            return;
        }

        // W, A, S, D·Î ¿òÁ÷ÀÌ°Ô ¸¸µé±â
        // ÇÃ·¹ÀÌ¾îÀÇ ÀÔ·Â ¹Ş±â
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // ÀÌµ¿ ¹æÇâ ¼³Á¤
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        animator.SetFloat("MoveMotion", dir.magnitude);

        // ¸ŞÀÎ Ä«¸Ş¶ó ±âÁØÀ¸·Î ¹æÇâ º¯È¯
        dir = Camera.main.transform.TransformDirection(dir);

        // Ä³¸¯ÅÍ ¼öÁ÷ ¼Óµµ¿¡ Áß·Â°ª Àû¿ë
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // ÀÌµ¿¼Óµµ¿¡ ¸ÂÃç ÀÌµ¿
        cc.Move(dir * MoveSpeed * Time.deltaTime);

        // Á¡ÇÁ ÁßÀÌ¾ú°í, ´Ù½Ã ¹Ù´Ú¿¡ ÂøÁöÇÏ¸é ½ÇÇà
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            // Á¡ÇÁÁßÀÌ¶ó¸é ½ÇÇà
            if (Isjumping)
            {
                // Á¡ÇÁ Àü »óÅÂ·Î ÃÊ±âÈ­
                Isjumping = false;
                // ¼öÁ÷ ¼Óµµ¸¦ 0À¸·Î ÇÑ´Ù.
                yVelocity = 0;
            }
        }

        // ½ºÆäÀÌ½º Å°¸¦ ´©¸£¸é Á¡ÇÁÇÏ±â
        if (Input.GetButtonDown("Jump") && !Isjumping)
        {
            // ÇÃ·¹ÀÌ¾î ¼öÁ÷ ¼Óµµ¿¡ Á¡ÇÁ·ÂÀ» Àû¿ë
            yVelocity = jumpPower;
            Isjumping = true;
        }

        // ÇÃ·¹ÀÌ¾îÀÇ Ã¼·Â¹Ù´Â hp / maxhpÀÇ »óÅÂ·Î ³ªÅ¸³­´Ù.
        slider.value = (float)hp / (float)Maxhp;

    }

    /// <summary>
    /// ÇÃ·¹ÀÌ¾îÀÇ ÇÇ°İ ÇÔ¼ö
    /// </summary>
    /// <param name="damage">ÇÃ·¹ÀÌ¾î°¡ ¹Ş´Â µ¥¹ÌÁö</param>
    public void DamageAction(int damage)
    {
        // ¿¡³Ê¹ÌÀÇ °ø°İ·Â¸¸Å­ ÇÃ·¹ÀÌ¾îÀÇ Ã¼·Â ±ğ±â
        hp -= damage;

        // ÇÃ·¹ÀÌ¾î¯M Ã¼·ÂÀÌ 0 ÀÌ»óÀÏ ¶§
        if(hp > 0)
        {
            // µ¥¹ÌÁö¸¦ ¹ŞÀ¸¸é PlayerHit()ÇÔ¼ö ½ÇÇà
            StartCoroutine(PlayerHit());
        }
    }

    /// <summary>
    /// ÇÃ·¹ÀÌ¾î ÇÇ°İ ÀÌÆåÆ® ½ÇÇà ÇÔ¼ö
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerHit()
    {
        // 1. ÀÌÆåÆ® È°¼ºÈ­
        hitEffect.SetActive(true);

        // 2. ÀÌÆåÆ® È°¼ºÈ­ ÈÄ 0.3ÃÊ°¡ Áö³ª¸é
        yield return new WaitForSeconds(0.3f);

        // 3. ÀÌÆåÆ® ºñÈ°¼ºÈ­
        hitEffect.SetActive(false);
    }
}
