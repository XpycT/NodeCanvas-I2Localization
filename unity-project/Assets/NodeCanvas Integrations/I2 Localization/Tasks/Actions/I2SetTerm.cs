using System.Collections.Generic;
using I2.Loc;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.Analytics;

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

        private int _choiceIndex = 0;


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
                    _choiceIndex = UnityEditor.EditorGUILayout.Popup("Term", _choiceIndex, terms);
                    if (_choiceIndex < 0)
                        _choiceIndex = 0;
                    primaryTerm.value = terms[_choiceIndex];
                }
        #endif
    }
}