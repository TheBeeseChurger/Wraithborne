using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardData))]
public class CardDataEditor : Editor
{
    bool showHeart;
    bool showEntity;
    bool showStructure;
    bool showRitual;

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
    SerializedProperty entityRange;
    SerializedProperty entitySpeed;

    SerializedProperty structurePulseCost;
    SerializedProperty structureHealth;
    SerializedProperty structureDamage;
    SerializedProperty structureRange;

    SerializedProperty ritualPulseCost;
    SerializedProperty ritualEffect;

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
        entityRange = serializedObject.FindProperty("EntityRange");
        entitySpeed = serializedObject.FindProperty("EntitySpeed");

        structurePulseCost = serializedObject.FindProperty("StructurePulseCost");
        structureHealth = serializedObject.FindProperty("StructureHealth");
        structureDamage = serializedObject.FindProperty("StructureDamage");
        structureRange = serializedObject.FindProperty("StructureRange");

        ritualPulseCost = serializedObject.FindProperty("RitualPulseCost");
        ritualEffect = serializedObject.FindProperty("RitualEffect");
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
                    EditorGUILayout.PropertyField(entityRange);
                    EditorGUILayout.PropertyField(entitySpeed);

                    EditorGUILayout.EndVertical();
                }
                break;
            case CardTypes.Structure:
                showStructure = EditorGUILayout.Foldout(showStructure, "Structure Information", true);
                if (showStructure)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorGUILayout.PropertyField(structurePulseCost);
                    EditorGUILayout.PropertyField(structureHealth);
                    EditorGUILayout.PropertyField(structureDamage);
                    EditorGUILayout.PropertyField(structureRange);

                    EditorGUILayout.EndVertical();
                }
                break;
            case CardTypes.Ritual:
                showRitual = EditorGUILayout.Foldout(showRitual, "Ritual Information", true);
                if (showRitual)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorGUILayout.PropertyField(ritualEffect);
                    EditorGUILayout.PropertyField(ritualPulseCost);

                    EditorGUILayout.EndVertical();
                }
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
