using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Set Current Language")]
    [Category("I2 Localization")]
    [Description("Set the localization CurrentLanguage")]
    public class I2SetCurrentLanguage : ActionTask
    {
        public BBParameter<string> language;

        protected override string info
        {
            get { return string.Format("Set language to {0}", language); }
        }

        protected override void OnUpdate()
        {
            bool success = false;
            if (LocalizationManager.HasLanguage(language.value))
            {
                success = true;
                LocalizationManager.CurrentLanguage = language.value;
            }

            EndAction(success);
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
        #if UNITY_EDITOR
        protected override void OnTaskInspectorGUI()
        {
            LocalizationManager.UpdateSources();
            string[] Languages = LocalizationManager.GetAllLanguages().ToArray();
            System.Array.Sort(Languages);
            
            int index = System.Array.IndexOf(Languages, language.value);
            int newIndex = UnityEditor.EditorGUILayout.Popup("Language", index, Languages);
            
            if (newIndex != index)
            {
                index = newIndex;
                if (index < 0 || index >= Languages.Length)
                    language.value = string.Empty;
                else
                    language.value = Languages[index];
            }
            
            //_choiceIndex.value = UnityEditor.EditorGUILayout.Popup("Language", _choiceIndex.value, languages);
            //language.value = languages[_choiceIndex.value];

            
           

        }
        #endif
    }
}