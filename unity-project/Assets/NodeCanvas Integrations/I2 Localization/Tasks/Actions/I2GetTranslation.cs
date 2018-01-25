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
        public BBParameter<LanguageSource> languageObject;
        [RequiredField] public BBParameter<string> term;
        public BBParameter<string> translation;


        protected override string info
        {
            get { return string.Format("Get the translation of term"); }
        }

        protected override void OnUpdate()
        {
            
            var termTranslation = languageObject.isNull ? LocalizationManager.GetTranslation(term.value) : languageObject.value.GetTranslation(term.value);
            if (!translation.isNone)
                translation.value = termTranslation;
            
            EndAction();
        }
    }
}