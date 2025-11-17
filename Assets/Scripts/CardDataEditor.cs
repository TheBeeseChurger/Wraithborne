using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardData))]
public class CardDataEditor : Editor
{
    bool showHeart;
    bool showEntity;

    SerializedProperty cardName;
    SerializedProperty cardDescription;
    SerializedProperty artwork;

    SerializedProperty cardType;
    SerializedProperty cardPulseType;

    SerializedProperty heartPassive;
    SerializedProperty heartHealth;
    SerializedProperty heartPulseGen;

    SerializedProperty entityPulseCost;
    SerializedProperty entityHealth;
    SerializedProperty entityDamage;

    private void OnEnable()
    {
        cardName = serializedObject.FindProperty("CardName");
        cardDescription = serializedObject.FindProperty("CardDescription");
        artwork = serializedObject.FindProperty("Artwork");

        cardType = serializedObject.FindProperty("CardType");
        cardPulseType = serializedObject.FindProperty("CardPulseType");

        heartPassive = serializedObject.FindProperty("HeartPassive");
        heartHealth = serializedObject.FindProperty("HeartHealth");
        heartPulseGen = serializedObject.FindProperty("HeartPulseGen");

        entityPulseCost = serializedObject.FindProperty("EntityPulseCost");
        entityHealth = serializedObject.FindProperty("EntityHealth");
        entityDamage = serializedObject.FindProperty("EntityDamage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(cardDescription);
        EditorGUILayout.PropertyField(artwork);

        EditorGUILayout.PropertyField(cardType);
        EditorGUILayout.PropertyField(cardPulseType);

        EditorGUILayout.Space(10);

        CardTypes t = (CardTypes)cardType.enumValueIndex;

        switch (t)
        {
            case CardTypes.Heart:
                showHeart = EditorGUILayout.Foldout(showHeart, "Heart Information", true);
                if (showHeart)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorGUILayout.PropertyField(heartPassive);
                    EditorGUILayout.PropertyField(heartHealth);
                    EditorGUILayout.PropertyField(heartPulseGen);

                    EditorGUILayout.EndVertical();
                }
                break;
            case CardTypes.Entity:
                showEntity = EditorGUILayout.Foldout(showEntity, "Entity Information", true);
                if (showEntity)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorGUILayout.PropertyField(entityPulseCost);
                    EditorGUILayout.PropertyField(entityHealth);
                    EditorGUILayout.PropertyField(entityDamage);

                    EditorGUILayout.EndVertical();
                }
                break;
            case CardTypes.Structure:
            case CardTypes.Ritual:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
