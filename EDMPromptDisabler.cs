using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Editor._private.edm_prompt_disabler
{
    [InitializeOnLoad]
    public class EdmPromptDisabler : MonoBehaviour
    {
        private const string Name = "EDM Prompt Disabler";
        private const string EDMPromptWindowTitle = "Enable Android Auto-resolution?";
    
    
    
        static EdmPromptDisabler()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            CloseAndDisableEDMPrompt();
        }
    
        [MenuItem("Kooply/Editor/Disable EDM Prompt")]
        public static void CloseAndDisableEDMPrompt()
        {
            CloseEDMPrompt();
            DisableEDMPrompt();
        }
    
        private static void CloseEDMPrompt()
        {
            var editorWindow = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (var window in editorWindow)
            {
                if (window.titleContent.text.StartsWith(EDMPromptWindowTitle))
                {
                    window.Close();
                    break;
                }
            }
        }
    
        private static void DisableEDMPrompt()
        {
            try
            {
                var workingDirectory = Directory.GetParent(Application.dataPath);
                if (workingDirectory != null)
                {
                    Process.Start(new ProcessStartInfo("git",
                            "checkout HEAD~1 -- ProjectSettings/GvhProjectSettings.xml")
                        {
                            WorkingDirectory = workingDirectory.ToString()
                        })?.WaitForExit();
                }
                else
                    throw new Exception("Can't detect working directory.");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning($"{Name}: Failed to disable prompt. {e.Message}");
            }
        }

        private static async void DisableEDMPromptAfterDelay()
        {
            await Task.Delay(3000);
            CloseAndDisableEDMPrompt();
        }
    
        // ******************************************************************************
        // Events
        // ******************************************************************************

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            DisableEDMPromptAfterDelay();
        }
    
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            DisableEDMPromptAfterDelay();
        }
    }
}
