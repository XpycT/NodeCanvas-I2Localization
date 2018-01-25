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
        
        [HideInInspector] public BBParameter<int> _choiceIndex;

        protected override string info
        {
            get { return string.Format("Set language"); }
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
            string[] languages = LocalizationManager.GetAllLanguages().ToArray();
            _choiceIndex.value = UnityEditor.EditorGUILayout.Popup("Language", _choiceIndex.value, languages);
            language.value = languages[_choiceIndex.value];
        }
        #endif
    }
}