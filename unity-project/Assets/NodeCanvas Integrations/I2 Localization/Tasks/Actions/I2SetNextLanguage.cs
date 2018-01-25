using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Set Next Language")]
    [Category("I2 Localization")]
    [Description(
        "Set localization CurrentLanguage with the next available language and send a graph event.")]
    public class I2SetNextLanguage : ActionTask<GraphOwner>
    {
        [RequiredField] public BBParameter<string> successEvent;
        [RequiredField] public BBParameter<string> failureEvent;

        public BBParameter<float> delay;

        public bool sendGlobal;


        protected override string info
        {
            get
            {
                return string.Format("Set localization CurrentLanguage with the next available language");
            }
        }

        protected override void OnUpdate()
        {
            if (elapsedTime >= delay.value)
            {
                bool success = false;
                var languages = LocalizationManager.GetAllLanguages();
                var idx = languages.IndexOf(LocalizationManager.CurrentLanguage);

                if (idx >= 0 && languages.Count > 1)
                {
                    // select next language
                    LocalizationManager.CurrentLanguage = languages[(idx + 1) % languages.Count];
                    success = true;
                }

                EndAction(success);
            }
        }
    }
}