using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/**
 * 他のコンポーネントの処理終了時に、このコンポーネントの
 * OnAnimationFinished を呼ぶ処理をシーケンシャルに実行するコンポーネント
 */
public class AnimationSequence : MonoBehaviour
{
    // 順番に実行した処理を設定する
    public UnityEvent[] animations = { };
    // 現在どこまで処理しているかのインデックス
    private int currentIndex  = 0;

    /**
     * 先頭から実行する
     */
    public void Run()
    {
        this.currentIndex = 0;

        this.TryRun();
    }

    /**
     * GameObject を有効にする
     */
    public void ActivateGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    /**
     * GameObject を無効にする
     */
    public void InactivateGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    /**
     * 指定秒数待って次の処理を実行する
     */
    public void RunNextAfter(float seconds)
    {
        StartCoroutine(this.RunNext(seconds));
    }

    /**
     * インデックスが正当かどうかを評価して実行する
     */
    private void TryRun()
    {
        if (this.currentIndex >= this.animations.Length)
        {
            return;
        }

        UnityEvent next = this.animations[this.currentIndex];
        next.Invoke();
    }

    private IEnumerator RunNext(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        this.currentIndex++;
        this.TryRun();
    }
}
