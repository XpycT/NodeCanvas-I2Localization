using I2.Loc;
using UnityEngine;
using ParadoxNotion.Design;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;

namespace NodeCanvas.Tasks.I2Loc{

	[Category("I2 Localization/Dialogue")]
	[Name("Localized Say")]
	[Description("You can use a variable inline with the text by using brackets likeso: [myVarName] or [Global/myVarName].\nThe bracket will be replaced with the variable value ToString")]
	public class I2Say : ActionTask<IDialogueActor> {

		public BBParameter<LanguageSource> languageSource;
		[RequiredField] public BBParameter<string> term;
		public BBParameter<string> translation;
		public Statement statement = new Statement("-");

		protected override string info{
			get { return string.Format("Say localized <i>{0}</i> term", term ); }
		}
		
		

		protected override void OnExecute(){
			var termTranslation = languageSource.isNull ? LocalizationManager.GetTranslation(term.value) : languageSource.value.GetTranslation(term.value);
			if (!translation.isNone){
				translation.value = termTranslation;
				statement.text = translation.value;
			}
			
			var tempStatement = statement.BlackboardReplace(blackboard);
			DialogueTree.RequestSubtitles( new SubtitlesRequestInfo( agent, tempStatement, EndAction ) );
		}


		////////////////////////////////////////
		///////////GUI AND EDITOR STUFF/////////
		////////////////////////////////////////
		#if UNITY_EDITOR

		protected override void OnTaskInspectorGUI(){
			languageSource = EditorUtils.BBParameterField("Language Source", languageSource) as BBParameter<LanguageSource>;
			
			EditTermSelection();
			//statement.text = UnityEditor.EditorGUILayout.TextArea(statement.text, (GUIStyle)"textField", GUILayout.Height(100));
			statement.audio = (AudioClip)UnityEditor.EditorGUILayout.ObjectField("Audio Clip", statement.audio, typeof(AudioClip), false);
			statement.meta = UnityEditor.EditorGUILayout.TextField("Meta", statement.meta);
		}
		
		bool EditTermSelection()
		{
			var terms = (!languageSource.isNull) ? languageSource.value.GetTermsList() : LocalizationManager.GetTermsList();
			terms.Sort(System.StringComparer.OrdinalIgnoreCase);
			var aTerms = terms.ToArray();


			var index = (term.value == "-" || term.value == "" ? 0: 
				(term.value == " " ? 0 : 
					System.Array.IndexOf( aTerms, term.value)));

			var newIndex = UnityEditor.EditorGUILayout.Popup("Term", index, aTerms);

			if (index == newIndex) return false;

			term.value = (newIndex < 0) ? string.Empty : aTerms[newIndex];

			return true;
		}

		#endif
	}
}