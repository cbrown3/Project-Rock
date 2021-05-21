/*using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CharSelection), true)]
public class CharSelectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CharSelection targetCharButton = (CharSelection)target;

        targetCharButton.p1PortraitAnimator = EditorGUILayout.ObjectField("Player 1 Portrait Animator:", targetCharButton.p1PortraitAnimator, typeof(Animator), true) as Animator;

        targetCharButton.p2PortraitAnimator = EditorGUILayout.ObjectField("Player 2 Portrait Animator:", targetCharButton.p2PortraitAnimator, typeof(Animator), true) as Animator;

        targetCharButton.fightButton = EditorGUILayout.ObjectField("Fight Button:", targetCharButton.fightButton, typeof(Button), true) as Button;

        targetCharButton.charNum = EditorGUILayout.IntField("Character Number:", targetCharButton.charNum);

        // Show default inspector property editor
        DrawDefaultInspector();
    }
}
*/