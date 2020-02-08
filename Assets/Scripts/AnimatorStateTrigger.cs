using UnityEngine;

/**
 * Animator のステートを遷移させるコンポーネント
 * 単一方向のトランジションのみ対応
 */
public class AnimatorStateTrigger : MonoBehaviour
{
    public const string STATE_INDEX = "stateIndex"; // TODO: 遊ばせるか後で考える
    public const string STATE_COUNT = "stateCount"; // TODO: 遊ばせるか後で考える

    /**
     * ボタンのクリックなど、任意の契機で実行できる
     */
    public void RequestTransition()
    {
        this.RequestTransition(this.gameObject);
    }
    public void RequestTransition(GameObject gameObject)
    {
        // Animator コンポーネントがあるかどうか
        Animator animator = gameObject.GetComponent<Animator>();

        // なかったらエラーになるので処理終了
        if (!animator) return;

        // 現在のステートのインデックス
        int index = animator.GetInteger(STATE_INDEX);

        // 現在のインデックスを進める
        index++;

        // 全ステートの数
        int count = animator.GetInteger(STATE_COUNT);

        // 存在しないインデックスを指定していたら、インデックスを 0 に戻す
        if (index >= count)
        {
            index = 0;
        }

        // 新しいインデックスをセットする
        animator.SetInteger(STATE_INDEX, index);
    }
}
