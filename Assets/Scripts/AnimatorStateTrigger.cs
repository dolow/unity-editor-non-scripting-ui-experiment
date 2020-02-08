using UnityEngine;
using UnityEngine.Events;

/**
 * Animator の特定パラメータを操作したり契機にするコンポーネント
 */
public class AnimatorStateTrigger : MonoBehaviour
{
    /**
     * inspector 上に表示するためのクラス
     * 単一のステートの Animation の再生終了後に実行する処理群を登録する
     */
    [System.Serializable]
    public class StateCallback
    {
        public int          stateIndex = 0;
        public UnityEvent[] onAnimationBegan    = { };
        public UnityEvent[] onAnimationFinished = { };
    }

    public const string STATE_INDEX = "stateIndex"; // TODO: 遊ばせるか後で考える
    public const string STATE_COUNT = "stateCount"; // TODO: 遊ばせるか後で考える

    // このコンポーネントの対象とする GameObject
    // 設定しなければ自身の GameObject を参照する
    public GameObject target = null;

    // 
    public StateCallback[] stateCallbacks;

    /**
     * シーン起動時に実行される
     */
    public void Start()
    {
        // GameObject が指定されていなければ自身の GameObject を対象とする
        if (this.target == null)
        {
            this.target = this.gameObject;
        }
    }

    /**
     * ボタンのクリックなど、任意の契機で実行できる
     */
    public void RequestTransition()
    {
        // Animator コンポーネントがあるかどうか
        Animator animator = this.target.GetComponent<Animator>();

        // なかったらエラーになるので処理終了
        if (!animator) return;

        // 現在のステートのインデックス
        int index = animator.GetInteger(STATE_INDEX);
        // 現在のインデックスを進める
        index++;
        // ステートの数
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
     * inspector 上から設定された Animation 開始時の処理を実行する
     */
    public void OnAnimationBegan()
    {
        StateCallback callback = this.GetCurrentStateCallback();
        // 存在しなければ終了
        if (callback == null) return;

        // 設定された Animation 開始時のコールバックを処理する
        for (int i = 0; i < callback.onAnimationBegan.Length; i++) 
        {
            callback.onAnimationBegan[i].Invoke();
        }
    }
    /**
     * inspector 上から設定された Animation 終了時の処理を実行する
     */
    public void OnAnimationFinished()
    {
        StateCallback callback = this.GetCurrentStateCallback();
        // 存在しなければ終了
        if (callback == null) return;

        // 設定された Animation 開始時のコールバックを処理する
        for (int i = 0; i < callback.onAnimationFinished.Length; i++)
        {
            callback.onAnimationFinished[i].Invoke();
        }
    }

    /**
     * 現在のステートのコールバックを返す
     */
    private StateCallback GetCurrentStateCallback()
    {
        StateCallback currentStateCallback = null;

        // Animator コンポーネントがあるかどうか
        Animator animator = this.GetComponent<Animator>();

        // なかったらエラーになるので処理終了
        if (!animator) return currentStateCallback;

        // 現在のステートのインデックス
        int index = animator.GetInteger(STATE_INDEX);


        // 現在のインデックスのステートのコールバックを探す
        for (int i = 0; i < stateCallbacks.Length; i++)
        {
            StateCallback stateCallback = stateCallbacks[i];
            if (stateCallback.stateIndex == index)
            {
                currentStateCallback = stateCallback;
                break;
            }
        }

        return currentStateCallback;
    }
}
