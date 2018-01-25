using UnityEngine;
using System.Collections.Generic;
using I2.Loc;
using ParadoxNotion.Design;
using NodeCanvas.Framework;
using NodeCanvas.DialogueTrees;

namespace NodeCanvas.Tasks.I2Loc{

	[Category("I2 Localization/Dialogue")]
	[Name("Localized Say Random")]
	[Description("A random localized statement will be chosen each time for the actor to say")]
	public class I2SayRandom : ActionTask<IDialogueActor> {

		public BBParameter<LanguageSource> languageSource;
		public List<string> terms = new List<string>();
		public List<Statement> statements = new List<Statement>();

		protected override void OnExecute(){
			var index = Random.Range(0,statements.Count);
			var statement = statements[index];
			var term = terms[index];
			var termTranslation = languageSource.isNull ? LocalizationManager.GetTranslation(term) : languageSource.value.GetTranslation(term);
			statement.text = termTranslation;
			
			var tempStatement = statement.BlackboardReplace(blackboard);
			var info = new SubtitlesRequestInfo( agent, tempStatement, EndAction );
			DialogueTree.RequestSubtitles(info);
		}


		////////////////////////////////////////
		///////////GUI AND EDITOR STUFF/////////
		////////////////////////////////////////
		#if UNITY_EDITOR

		protected override void OnTaskInspectorGUI()
		{
			if (GUILayout.Button("Add Statement")){
				statements.Add(new Statement(""));
				terms.Add("-");
			}

			var statementsArray = statements.ToArray();
			for (var index = 0; index < statementsArray.Length; index++)
			{
				var statement = statementsArray[index];
				
				GUILayout.BeginHorizontal();
				GUILayout.BeginVertical();
				EditorUtils.Separator();
				terms[index] = EditTermSelection(statement, terms[index]);
				statement.audio =
					(AudioClip) UnityEditor.EditorGUILayout.ObjectField("Audio Clip", statement.audio, typeof(AudioClip), false);
				statement.meta = UnityEditor.EditorGUILayout.TextField("Meta", statement.meta);
				GUILayout.EndVertical();
				GUILayout.BeginVertical();
				if (GUILayout.Button("X"))
				{
					statements.Remove(statement);
				}

				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		}
		
		string EditTermSelection(Statement statement, string term)
		{
			var terms = (!languageSource.isNull) ? languageSource.value.GetTermsList() : LocalizationManager.GetTermsList();
			terms.Sort(System.StringComparer.OrdinalIgnoreCase);
			var aTerms = terms.ToArray();


			var index = (term == "-" || term == "" ? 0: 
				(term == " " ? 0 : 
					System.Array.IndexOf( aTerms, term)));

			var newIndex = UnityEditor.EditorGUILayout.Popup("Term", index, aTerms);
			term = aTerms[newIndex];

			return term;
		}

		#endif
	}
}