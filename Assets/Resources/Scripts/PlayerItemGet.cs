using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGet : MonoBehaviour {

    PlayerStats playerStats;

    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// 前回取ったアイテムのuuid
    /// </summary>
    string preItemUuid = "";

    [SerializeField]
    GameObject getLifeItemEffect;
    [SerializeField]
    GameObject getScoreItemEffect;
    private void Awake()
    {
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        playerStats = this.GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // アイテムに当たったら体力とスコアを加算する
        if (collision.transform.tag == "Item")
        {
            Item getItem = collision.gameObject.GetComponent<Item>();

            // 前回取得したアイテムと同じUUIDの場合処理しない
            if (preItemUuid == getItem.Uuid)
            {
                return;
            }
            // 体力回復アイテムの場合は回復系サウンドを鳴らす
            if (getItem.AddHitPoint > 0)
            {
                soundController.Item2();

                getLifeItemEffect.SetActive(false);
                getLifeItemEffect.transform.position = this.transform.position - transform.up * 0.5f;
                getLifeItemEffect.SetActive(true);
            }
            else
            {
                soundController.Item();
                getScoreItemEffect.SetActive(false);
                getScoreItemEffect.transform.position = this.transform.position - transform.up * 0.5f;
                getScoreItemEffect.SetActive(true);
            }
            playerStats.PlayerHitPoint += getItem.AddHitPoint;
            playerStats.Score += getItem.AddScore;
            preItemUuid = getItem.Uuid;

        }
    }
}
