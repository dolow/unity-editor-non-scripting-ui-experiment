using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * 他のコンポーネントの処理終了時に、このコンポーネントの
 * OnAnimationFinished を呼ぶ処理をシーケンシャルに実行するコンポーネント
 */
public class AnimationSequence : MonoBehaviour
{
    // 順番に実行した処理を設定するsingle
    public List<UnityEvent> animations = new List<UnityEvent>();
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
     * 指定秒数待って次の処理を実行する
     */
    public void Wait(float seconds)
    {
        StartCoroutine(this.RunNext(seconds));
    }

    /**
     * 指定秒数待って次の処理を実行する
     */
    public void Next()
    {
        this.currentIndex++;
        this.TryRun();
    }

    /**
     * インデックスが正当かどうかを評価して実行する
     */
    private void TryRun()
    {
        if (this.currentIndex >= this.animations.Count)
        {
            return;
        }

        UnityEvent next = this.animations[this.currentIndex];
        next.Invoke();
    }

    private IEnumerator RunNext(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        this.Next();
    }
}
