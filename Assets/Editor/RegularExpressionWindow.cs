using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class RegularExpressionWindow : EditorWindow
{
    private enum ExecuteType
    {
        Match,
        Matches,
    }

    private ExecuteType _type = ExecuteType.Match;
    private string _input;
    private string _pattern;
    private string _output;

    [MenuItem("Tools/RegularExpressionWindow")]
    private static void Open()
    {
        var window = GetWindow<RegularExpressionWindow>();
        window.minSize = new Vector2(640, 160);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("■入力■");
        _type = (ExecuteType) EditorGUILayout.EnumPopup("Regex.〇〇", _type);
        _input = EditorGUILayout.TextField("input", _input);
        _pattern = EditorGUILayout.TextField("pattern", _pattern);

        if (string.IsNullOrEmpty(_input) || string.IsNullOrEmpty(_pattern)) EditorGUI.BeginDisabledGroup(true);
        if (GUILayout.Button("Execute"))
        {
            if (_type == ExecuteType.Match)
            {
                _output = Regex.Match(_input, _pattern).Value;
            }
            else if (_type == ExecuteType.Matches)
            {
                _output = string.Join(" , ",
                    Regex.Matches(_input, _pattern).Cast<Match>().Select(t => t.Groups[0].Value));
            }
        }

        if (string.IsNullOrEmpty(_input) || string.IsNullOrEmpty(_pattern)) EditorGUI.EndDisabledGroup();

        EditorGUILayout.LabelField("■出力■");
        EditorGUILayout.LabelField(!string.IsNullOrEmpty(_output) ? _output : "該当なし");
    }
}