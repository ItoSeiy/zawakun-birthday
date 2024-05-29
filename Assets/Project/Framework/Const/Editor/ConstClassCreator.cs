using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Project.Framework.Extensions;
using Project.Framework.Utils;
using UnityEditor;

namespace Project.Framework.Const.Editor
{
    public static class ConstClassCreator
    {
        //拡張子
        private const string ScriptExtension = ".cs";

        //定数の区切り文字
        private const char Delimiter = '_';

        //無効な文字を管理する配列
        private static readonly string[] InvalidChars =
        {
            " ", "!", "\"", "#", "$",
            "%", "&", "\'", "(", ")",
            "-", "=", "^", "~", "\\",
            "|", "[", "{", "@", "`",
            "]", "}", ":", "*", ";",
            "+", "/", "?", ".", ">",
            ",", "<"
        };

        /// <summary>
        /// 定数を管理するクラスを自動生成する
        /// </summary>
        /// <param name="className">クラス名</param>
        /// <param name="summary">記載したいサマリ</param>
        /// <param name="variableDict">記載したい変数 Key:変数名 Value:値</param>
        /// <param name="exportDirectoryPath">Assets/ から始まるパス</param>
        /// <param name="nameSpace">名前空間</param>
        /// <param name="isVariableTypeElias">記載したい変数の型をエイリアスで記載するか</param>
        /// <typeparam name="T">記載したい変数の型</typeparam>
        public static void Create<T>(string className, string summary, Dictionary<string, T> variableDict,
            string exportDirectoryPath, string nameSpace, bool isVariableTypeElias = true)
        {
            //ディクショナリーをソートしたものに
            var sortDict = new SortedDictionary<string, T>(variableDict);

            //入力された辞書のkeyから無効な文字列を削除して、大文字に_を設定した定数名と同じものに変更し新たな辞書に登録
            //次の定数の最大長求めるところで、_を含めたものを取得したいので、先に実行
            var newValueDict = new Dictionary<string, T>();

            foreach (var valuePair in sortDict)
            {
                var newKey = RemoveInvalidChars(valuePair.Key);
                newValueDict[newKey] = valuePair.Value;
            }

            //コード全文
            var builder = new StringBuilder();

            //ネームスペースがあれば設定
            if (!string.IsNullOrEmpty(nameSpace))
            {
                builder.AppendLine("namespace " + nameSpace);
                builder.AppendLine("{");
            }

            //コメント文とクラス名を入力
            builder.AppendLine("\t/// <summary>");
            builder.AppendLine($"\t/// {summary}");
            builder.AppendLine("\t/// </summary>");
            builder.AppendLine($"\tpublic static class {className}");
            builder.AppendLine("\t{");

            //入力された定数とその値のペアを書き出していく
            var keyArray = newValueDict.Keys.ToArray();
            foreach (var key in keyArray)
            {
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                //数字だけのkeyだったらスルー
                if (Regex.IsMatch(key, @"^[0-9]+$"))
                {
                    continue;
                }

                var variableName = typeof(T).Name;
                variableName = isVariableTypeElias ? variableName.ToLower() : variableName;
                variableName = typeof(T) == typeof(float) ? "float" : variableName;
                variableName = typeof(T) == typeof(long) ? "long" : variableName;
                //型と定数名を入力
                builder.Append($"\t\tpublic static {variableName} {key} => ");

                //Tがstringの場合は値の前後に"を付ける
                if (typeof(T) == typeof(string))
                {
                    builder.Append($@"""{newValueDict[key]}"";").AppendLine();
                }
                //Tがfloatの場合は値の後にfを付ける
                else if (typeof(T) == typeof(float))
                {
                    builder.Append($"{newValueDict[key]}f;").AppendLine();
                }
                else
                {
                    builder.Append($"{newValueDict[key]};").AppendLine();
                }
            }

            builder.AppendLine("\t}");

            //ネームスペースがあれば最後にカッコ追加
            if (!string.IsNullOrEmpty(nameSpace))
            {
                builder.AppendLine("}");
            }

            //書き出し、ファイル名はクラス名.cs
            var exportPath = Path.Combine(exportDirectoryPath, className + ScriptExtension);
            var exportText = builder.ToString();

            //書き出し先のディレクトリが無ければ作成
            var directoryName = Path.GetDirectoryName(exportPath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            //書き出し先のファイルがあるかチェック
            if (File.Exists(exportPath))
            {
                //同名ファイルの中身をチェック、全く同じだったら書き出さない
                var sr = new StreamReader(exportPath, Encoding.UTF8);
                var isSame = sr.ReadToEnd() == exportText;
                sr.Close();

                if (isSame)
                {
                    return;
                }
            }

            //書き出し
            File.WriteAllText(exportPath, exportText, Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

            CustomDebug.Log(className + ScriptExtension + "の作成が完了しました");
        }

        /// <summary>
        /// 無効な文字を削除します
        /// </summary>
        private static string RemoveInvalidChars(string str)
        {
            InvalidChars.ForEach(c => str = str.Replace(c, string.Empty));
            return str;
        }
    }
}
