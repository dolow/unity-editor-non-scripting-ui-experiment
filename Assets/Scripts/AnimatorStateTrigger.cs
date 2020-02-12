using UnityEngine;

/**
 * Animator のステートを遷移させるコンポーネント
 * 単一方向のトランジションのみ対応
 */
public class AnimatorStateTrigger : MonoBehaviour
{
    public const string STATE_INDEX         = "stateIndex";
    public const string STATE_COUNT         = "stateCount";
    public const string TRIGGER_SINGLE_SHOT = "singleShot";

    public GameObject target = null;

    /**
     * 次のステートに遷移させる
     */
    public void RequestTransition()
    {
        this.RequestTransition(this.target);
    }
    public void RequestTransition(GameObject gameObject)
    {
        // Animator コンポーネントの取得
        Animator animator = this.GetTargetAnimator(gameObject);

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


    /**
     * 利用する Animator を返す
     */
    private Animator GetTargetAnimator(GameObject obj)
    {
        // 存在しない場合は 自身の gameObject を target にする
        if (obj == null)
        {
            obj = this.gameObject;
        }

        // Animator コンポーネントがあるかどうか
        return obj.GetComponent<Animator>();
    }
}
