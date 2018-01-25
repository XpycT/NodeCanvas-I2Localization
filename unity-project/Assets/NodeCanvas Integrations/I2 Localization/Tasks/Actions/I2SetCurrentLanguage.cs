using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Set Current Language")]
    [Category("I2 Localization")]
    [Description("Set the localization CurrentLanguage")]
    public class I2SetCurrentLanguage : ActionTask
    {
        public BBParameter<string> language;
        
        private int _choiceIndex = 0;

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
            _choiceIndex = UnityEditor.EditorGUILayout.Popup("Language", _choiceIndex, languages);
            if (_choiceIndex < 0)
                _choiceIndex = 0;
            language.value = languages[_choiceIndex];
        }
        #endif
    }
}