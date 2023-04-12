using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    /// <summary>
    /// サウンドデバッグ用
    /// </summary>
    [SerializeField]
    bool DEBUG = false;

    private AudioSource bgm;

    /// <summary>
    /// BGMループ開始時点
    /// </summary>
    [SerializeField]
    private float bgmLoopStartTime;

    /// <summary>
    /// BGMループ終了時点
    /// </summary>
    [SerializeField]
    private float bgmLoopEndTime;

    /// <summary>
    /// BGM開始時点
    /// </summary>
    [SerializeField]
    public float bgmStartTime;

    private AudioSource monster;
    private AudioSource jump;
    private AudioSource jump2;
    private AudioSource stomp;
    private AudioSource block;
    private AudioSource damage;
    private AudioSource impact;
    private AudioSource dead;
    private AudioSource item;
    private AudioSource item2;
    private AudioSource door;
    private AudioSource select;
    private AudioSource cancel;
    
    void Start()
    {
        bgm      = transform.Find("BGM").gameObject.GetComponent<AudioSource>();
        monster  = transform.Find("Monster").gameObject.GetComponent<AudioSource>();
        jump     = transform.Find("Jump").gameObject.GetComponent<AudioSource>();
        jump2    = transform.Find("Jump2").gameObject.GetComponent<AudioSource>();
        stomp    = transform.Find("Stomp").gameObject.GetComponent<AudioSource>();
        block    = transform.Find("Block").gameObject.GetComponent<AudioSource>();
        damage   = transform.Find("Damage").gameObject.GetComponent<AudioSource>();
        impact   = transform.Find("Impact").gameObject.GetComponent<AudioSource>();
        item     = transform.Find("Item").gameObject.GetComponent<AudioSource>();
        item2    = transform.Find("Item2").gameObject.GetComponent<AudioSource>();
        door     = transform.Find("Door").gameObject.GetComponent<AudioSource>();
        select   = transform.Find("Select").gameObject.GetComponent<AudioSource>();
        cancel   = transform.Find("Cancel").gameObject.GetComponent<AudioSource>();
        dead     = transform.Find("Dead").gameObject.GetComponent<AudioSource>();

        bgm.time = bgmStartTime;
        bgm.Play();
    }

    void Update()
    {
        // テスト用
        if (DEBUG)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                bgm.time = bgmStartTime;
                bgm.Play();
            }
            print(bgm.time);
        }

        // 現在の再生時間がbgmLoopEndTimeを超えたらbgmLoopStartTimeを現在の再生時間に設定
        if (bgm.time >= bgmLoopEndTime)
        {
            // BGMループ
            bgm.time = bgmLoopStartTime;
        }
    }

    /// <summary>
    /// Monsterを再生
    /// </summary>
    public void Monster()
    {
        monster.PlayOneShot(monster.clip);
    }

    /// <summary>
    /// Jumpを再生
    /// </summary>
    public void Jump()
    {
        jump.PlayOneShot(jump.clip);
    }

    /// <summary>
    /// Jump2を再生
    /// </summary>
    public void Jump2()
    {
        jump2.PlayOneShot(jump2.clip);
    }

    /// <summary>
    /// Stompを再生
    /// </summary>
    public void Stomp()
    {
        stomp.PlayOneShot(stomp.clip);
    }

    /// <summary>
    /// Blockを再生
    /// </summary>
    public void Block()
    {
        block.PlayOneShot(block.clip);
    }

    /// <summary>
    /// Damageを再生
    /// </summary>
    public void Damage()
    {
        damage.PlayOneShot(damage.clip);
    }

    /// <summary>
    /// Impactを再生
    /// </summary>
    public void Impact()
    {
        impact.PlayOneShot(impact.clip);
    }

    /// <summary>
    /// Itemを再生
    /// </summary>
    public void Item()
    {
        item.PlayOneShot(item.clip);
    }

    /// <summary>
    /// Item2を再生
    /// </summary>
    public void Item2()
    {
        item2.PlayOneShot(item2.clip);
    }

    /// <summary>
    /// Doorを再生
    /// </summary>
    public void Door()
    {
        door.PlayOneShot(door.clip);
    }

    /// <summary>
    /// Selectを再生
    /// </summary>
    public void Select()
    {
        select.PlayOneShot(select.clip);
    }
    /// <summary>
    /// Cancelを再生
    /// </summary>
    public void Cancel()
    {
        cancel.PlayOneShot(cancel.clip);
    }
    /// <summary>
    /// Deadを再生
    /// </summary>
    public void Dead()
    {
        dead.PlayOneShot(dead.clip);
    }
}
