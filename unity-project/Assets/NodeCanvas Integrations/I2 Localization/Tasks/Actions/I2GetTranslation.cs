using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Get Translation")]
    [Category("I2 Localization")]
    [Description("Get the translations from global source or from LanguageSource prefab")]
    public class I2GetTranslation : ActionTask
    {
        public BBParameter<LanguageSource> languageSource;
        public BBParameter<bool> selectionMode;
        [RequiredField] public BBParameter<string> term;
        public BBParameter<string> translation;


        protected override string info
        {
            get { return string.Format("Get the translation of term"); }
        }

        protected override void OnUpdate()
        {
            
            var termTranslation = languageSource.isNull ? LocalizationManager.GetTranslation(term.value) : languageSource.value.GetTranslation(term.value);
            if (!translation.isNone)
                translation.value = termTranslation;
            
            EndAction();
        }
        
        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR
        protected override void OnTaskInspectorGUI()
        {
            languageSource = EditorUtils.BBParameterField("Language Source", languageSource) as BBParameter<LanguageSource>;
            
            selectionMode = EditorUtils.BBParameterField("Selection Mode", selectionMode) as BBParameter<bool>;

            if (selectionMode.value)
            {
                EditTermSelection();
            }else{
                term = EditorUtils.BBParameterField("Term", term) as BBParameter<string>;
            }
            
            translation = EditorUtils.BBParameterField("Translation", translation) as BBParameter<string>;
        }
        
        bool EditTermSelection()
        {
            var terms = (!languageSource.isNull) ? languageSource.value.GetTermsList() : LocalizationManager.GetTermsList();
            terms.Sort(System.StringComparer.OrdinalIgnoreCase);
            terms.Add("");
            terms.Add("<inferred from text>");
            terms.Add("<none>");
            var aTerms = terms.ToArray();


            var index = (term.value == "-" || term.value == "" ? aTerms.Length - 1: 
                (term.value == " " ? aTerms.Length - 2 : 
                    System.Array.IndexOf( aTerms, term.value)));

            var newIndex = UnityEditor.EditorGUILayout.Popup("Term", index, aTerms);

            if (index == newIndex) return false;

            term.value = (newIndex < 0 || newIndex == aTerms.Length - 1) ? string.Empty : aTerms[newIndex];
            if (newIndex == aTerms.Length - 1)
                term.value = "-";
            else if (newIndex < 0 || newIndex == aTerms.Length - 2)
                term.value = string.Empty;
            else
                term.value = aTerms[newIndex];

            return true;
        }
#endif
    }
}