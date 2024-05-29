## 基本情報

| エンジン | バージョン  |
| ---------- | ----------- |
| Unity      | [こちらを参照して下さい](ProjectSettings/ProjectVersion.txt#L1) |

導入ライブラリ
- Addressable
- Localization
- [UniTask](https://github.com/Cysharp/UniTask)
- [R3](https://github.com/Cysharp/R3)
    - [UniRxとR3の違い](https://qiita.com/toRisouP/items/4344fbcba7b7e8d8ce16#%E6%A0%B9%E6%9C%AC%E7%9A%84%E3%81%AA%E6%8C%99%E5%8B%95%E3%81%AE%E9%81%95%E3%81%84)
- [DoTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676?locale=ja-JP)
- [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)
- [uPalette](https://github.com/Haruma-K/uPalette)
    - [日本語ドキュメント](https://github.com/Haruma-K/uPalette/blob/master/README_JA.md)
- [Addler](https://github.com/Haruma-K/Addler)
    - [Addlerドキュメント](https://light11.hatenadiary.com/entry/2021/02/09/194551)
- [UnityScreenNavigator](https://github.com/Haruma-K/UnityScreenNavigator)
    - [日本語Readme](https://github.com/Haruma-K/UnityScreenNavigator/blob/master/README_JA.md)
- [UnityUIPlayables](https://github.com/Haruma-K/UnityUIPlayables)
- [Unity Debug Sheet](https://github.com/Haruma-K/UnityDebugSheet/blob/master/README_JA.md)
- [In-Game Debug Console](https://github.com/yasirkula/UnityIngameDebugConsole?tab=readme-ov-file#in-game-debug-console-for-unity-3d)
- [Graphy](https://github.com/Tayx94/graphy?tab=readme-ov-file#graphy---ultimate-fps-counter---stats-monitor--debugger-unity)
- [AudioConductor](https://github.com/CyberAgentGameEntertainment/AudioConductor)


## コーディングルール
- Riderのオートフォーマット機能に任せる
    - 意図 : オートフォーマットのファイルを最初に作ってあとはRiderに任せた方が圧倒的に効率が良い (気になる、変えたいフォーマットはもちろん相談歓迎)

ローカル変数名は[キャメルケース](https://e-words.jp/w/%E3%82%AD%E3%83%A3%E3%83%A1%E3%83%AB%E3%82%B1%E3%83%BC%E3%82%B9.html) (先頭小文字)

メンバー変数の接頭辞には「＿」(アンダースコア)を付けること

関数名　クラス名　プロパティの名前は[パスカルケース](https://wa3.i-3-i.info/word13955.html) (先頭大文字)  

残りの細かいルールはRiderの設定を参照してください

## ブランチルール

- {ブランチ種別}/{名前}/{機能名} / これ以降は機能の規模が大きくなったら後述の例のように適宜命名していく

#### ブランチ種別
- ```feature``` : 機能追加
- ```fix``` : バグ修正や諸々の修正関連
- ```add```: アセット等の追加

#### 例)
- 規模感大きくない場合
    - ```feature/seiya/timeline/```

- 規模感大きい場合
    - ```feature/seiya/timeline/main```
        - ```feature/seiya/timeline/hoge01``` (```feature/seiya/timeline/main```にマージする)
        - ```feature/seiya/timeline/hoge01``` (```feature/seiya/timeline/main```にマージする)

#### コミットメッセージ
- ```add : hoge```
- ```feat : hoge```
- ```fix : hoge```
など、ブランチ名と同じ感覚の接頭辞でよしなに
