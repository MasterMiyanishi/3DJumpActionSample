using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスではプレイヤーの体力と生存を管理しています
/// </summary>
public class PlayerStats : MonoBehaviour
{
    /// <summary>
    /// 体力
    /// </summary>
    [SerializeField]
    private float playerHitPoint = 3f;

    /// <summary>
    /// MAX体力
    /// </summary>
    [SerializeField]
    private float playerMaxHitPoint = 3f;

    /// <summary>
    /// 生存フラグ
    /// </summary>
    [SerializeField]
    private bool playerAlive = true;

    /// <summary>
    /// 無敵時間
    /// </summary>
    [SerializeField]
    float godTime = 3f;

    /// <summary>
    /// スコア
    /// </summary>
    [SerializeField]
    int score = 0;

    /// <summary>
    /// ハイスコア
    /// </summary>
    [SerializeField]
    int highScore = 0;

    private string scoreKey = "SCORE"; //スコアの保存先キー
    private string highScoreKey = "HIGH_SCORE"; //ハイスコアの保存先キー

    /// <summary>
    /// 空中浮遊する（デバッグモード）
    /// </summary>
    [SerializeField]
    bool ghostMode = false;

    /// <summary>
    /// ゲームオーバー画面が表示されるまでの時間
    /// </summary>
    float gameOverScreenDisplayTime = 2f;

    private void Awake()
    {
        score = PlayerPrefs.GetInt(scoreKey, 0);
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);

    }
    /// <summary>
    /// スコアの保存
    /// </summary>
    public void SaveScore()
    {
        PlayerPrefs.SetInt(scoreKey, score);
    }
    /// <summary>
    /// ハイスコアの保存
    /// </summary>
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt(highScoreKey, HighScore);
    }
    /// <summary>
    /// スコアの削除
    /// </summary>
    public void DelScore()
    {
        score = 0;
        PlayerPrefs.DeleteKey(scoreKey);
    }
    /// <summary>
    /// ハイスコアの削除
    /// </summary>
    public void DelHighScore()
    {
        DelScore();
        highScore = 0;
        PlayerPrefs.DeleteKey(highScoreKey);
    }
    public float PlayerHitPoint
    {
        get
        {
            return playerHitPoint;
        }

        set
        {
            playerHitPoint = value;
            // 体力が0以下になったら生存フラグをOFFにする
            if (playerHitPoint <= 0)
            {
                PlayerAlive = false;
            }



            // 体力の最大値をこえないようにする
            if (playerHitPoint > PlayerMaxHitPoint)
            {
                playerHitPoint = PlayerMaxHitPoint;
            }
        }
    }
    public bool PlayerAlive
    {
        get
        {
            return playerAlive;
        }

        set
        {
            playerAlive = value;

            if (!playerAlive)
            {
                StartCoroutine(GameOverScreenDisplay());
            }
        }
    }
    IEnumerator GameOverScreenDisplay()
    {
        yield return new WaitForSeconds(gameOverScreenDisplayTime);

        GameObject.Find("GameOverCanvas").GetComponent<Animator>().SetTrigger("Display"); ;

        yield break;
    }

    public float GodTime
    {
        get
        {
            return godTime;
        }

        set
        {
            godTime = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            // ハイスコアを超えたらハイスコアにもスコアを格納する
            if (HighScore < score)
            {
                highScore = score;
            }
        }
    }

    public float PlayerMaxHitPoint
    {
        get
        {
            return playerMaxHitPoint;
        }

        set
        {
            playerMaxHitPoint = value;
        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }
    }

    public bool GhostMode
    {
        get
        {
            return ghostMode;
        }

        set
        {
            ghostMode = value;
        }
    }
}
