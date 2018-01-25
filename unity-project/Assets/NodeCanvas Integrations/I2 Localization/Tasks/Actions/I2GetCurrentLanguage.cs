using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Get Current Language")]
    [Category("I2 Localization")]
    [Description("Get the localization current Language")]
    public class I2GetCurrentLanguage : ActionTask
    {
        [BlackboardOnly] public BBParameter<string> currentLanguage;
        [BlackboardOnly] public BBParameter<string> currentLanguageCode;


        protected override string info
        {
            get { return "Get Current Language"; }
        }

        protected override void OnUpdate()
        {
            currentLanguage.value = LocalizationManager.CurrentLanguage;
            currentLanguageCode.value = LocalizationManager.CurrentLanguageCode;
            EndAction();
        }
    }
}