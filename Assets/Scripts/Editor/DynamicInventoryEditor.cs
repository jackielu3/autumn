using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DynamicInventory))]
public class DynamicInventoryEditor : Editor
{
    private SerializedProperty predefinedItemsProperty;
    private DynamicInventory inventory;

    private void OnEnable()
    {
        predefinedItemsProperty = serializedObject.FindProperty("predefinedItems");
        inventory = (DynamicInventory)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Predefined Items (Initialization)", EditorStyles.boldLabel);

        // Predefined Items Section
        for (int i = 0; i < predefinedItemsProperty.arraySize; i++)
        {
            SerializedProperty itemProperty = predefinedItemsProperty.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(itemProperty.FindPropertyRelative("itemType"), GUIContent.none);
            EditorGUILayout.PropertyField(itemProperty.FindPropertyRelative("count"), GUIContent.none);

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                predefinedItemsProperty.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Item"))
        {
            predefinedItemsProperty.InsertArrayElementAtIndex(predefinedItemsProperty.arraySize);
            var newItem = predefinedItemsProperty.GetArrayElementAtIndex(predefinedItemsProperty.arraySize - 1);
            newItem.FindPropertyRelative("itemType").objectReferenceValue = null;
            newItem.FindPropertyRelative("count").intValue = 1;
        }

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        // Runtime Inventory Management
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.LabelField("Runtime Inventory Management", EditorStyles.boldLabel);

            foreach (var category in inventory.GetCategories())
            {
                EditorGUILayout.LabelField($"Category: {category.Key.Name}", EditorStyles.boldLabel);

                foreach (var item in category.Value.items)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(item.itemType.itemName, GUILayout.Width(150));
                    int newCount = EditorGUILayout.IntField("Count", item.count);

                    if (newCount != item.count)
                    {
                        item.count = Mathf.Max(0, newCount); // Ensure no negative counts
                    }

                    if (GUILayout.Button("Remove", GUILayout.Width(70)))
                    {
                        inventory.RemoveItem(item);
                        break;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }

            // Add Runtime Item
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Add New Item", EditorStyles.boldLabel);

            ItemData newItemType = (ItemData)EditorGUILayout.ObjectField("Item Type", null, typeof(ItemData), false);
            int newItemCount = EditorGUILayout.IntField("Count", 1);

            if (GUILayout.Button("Add Item") && newItemType != null)
            {
                inventory.AddItem(new ItemInstance(newItemType) { count = newItemCount }, newItemCount);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Runtime inventory management is only available while the game is running.", MessageType.Info);
        }
    }
}
