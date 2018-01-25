using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.I2Loc
{
    [Name("Set Term")]
    [Category("I2 Localization")]
    [Description("Set the localization Terms")]
    public class I2SetTerm : ActionTask
    {
        [RequiredField] public BBParameter<Localize> localizeObject;
        public BBParameter<string> primaryTerm;
        public BBParameter<string> secondaryTerm;

        protected override string info
        {
            get { return string.Format("Set the localization Term"); }
        }

        protected override string OnInit()
        {
            if (LocalizationManager.GetTermsList().Count == 0)
                return "LocalizationManager do not have any terms!";
            return null;
        }

        protected override void OnUpdate()
        {
            bool status = false;
            if (localizeObject.value != null)
            {
                if (string.IsNullOrEmpty(secondaryTerm.value))
                    localizeObject.value.SetTerm(primaryTerm.value);
                else
                    localizeObject.value.SetTerm(primaryTerm.value, secondaryTerm.value);

                status = true;
            }

            EndAction(status);
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR
        protected override void OnTaskInspectorGUI()
        {
            localizeObject = EditorUtils.BBParameterField("Localize Object", localizeObject) as BBParameter<Localize>;

            var terms = (!localizeObject.isNull && localizeObject.value.Source != null)
                ? localizeObject.value.Source.GetTermsList()
                : LocalizationManager.GetTermsList();
            terms.Sort(System.StringComparer.OrdinalIgnoreCase);
            terms.Add("");
            terms.Add("<inferred from text>");
            terms.Add("<none>");
            var aTerms = terms.ToArray();

            DoTermPopup("Primary Term", primaryTerm, aTerms);
            DoTermPopup("Secondary Term", secondaryTerm, aTerms);
        }

        bool DoTermPopup(string label, BBParameter<string> sTerm, string[] aTerms)
        {
            var index = (sTerm.value == "-" || sTerm.value == ""
                ? aTerms.Length - 1
                : (sTerm.value == " " ? aTerms.Length - 2 : System.Array.IndexOf(aTerms, sTerm.value)));

            var newIndex = UnityEditor.EditorGUILayout.Popup(label, index, aTerms);

            if (index == newIndex) return false;

            sTerm.value = (newIndex < 0 || newIndex == aTerms.Length - 1) ? string.Empty : aTerms[newIndex];
            if (newIndex == aTerms.Length - 1)
                sTerm.value = "-";
            else if (newIndex < 0 || newIndex == aTerms.Length - 2)
                sTerm.value = string.Empty;
            else
                sTerm.value = aTerms[newIndex];

            return true;
        }
#endif
    }
}