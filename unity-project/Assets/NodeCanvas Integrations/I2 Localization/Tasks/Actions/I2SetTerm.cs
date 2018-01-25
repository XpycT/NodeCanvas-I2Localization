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

        [HideInInspector] public BBParameter<int> _choiceIndex;


        protected override string info
        {
            get { return string.Format("Set the localization Term"); }
        }
        
        protected override string OnInit(){
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
                protected override void OnTaskInspectorGUI(){
                    string[] terms = LocalizationManager.GetTermsList().ToArray();
                    _choiceIndex.value = UnityEditor.EditorGUILayout.Popup("Term", _choiceIndex.value, terms);
                    primaryTerm.value = terms[_choiceIndex.value];
                }
        #endif
    }
}