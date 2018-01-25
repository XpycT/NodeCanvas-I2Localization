using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Get Term")]
    [Category("I2 Localization")]
    [Description("Get the localization Terms")]
    public class I2GetTerm : ActionTask
    {
        [RequiredField] public BBParameter<Localize> localizeObject;
        [BlackboardOnly] public BBParameter<string> term;


        protected override string info
        {
            get { return string.Format("Get the localization Term"); }
        }

        protected override void OnUpdate()
        {
				
            if (localizeObject.value != null)
            {
                term.value = localizeObject.value.Term;
            }
            EndAction();
        }
    }
}