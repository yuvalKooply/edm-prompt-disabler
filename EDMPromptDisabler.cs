using System;
using System.Xml;
using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;

[InitializeOnLoad]
public class EDMPromptDisabler : MonoBehaviour
{
    private const string Name = "EDM Prompt Disabler";
    private const string EDMPromptWindowTitle = "Enable Android Auto-resolution?";
    
    
    
    static EDMPromptDisabler()
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
            using var reader = new XmlTextReader("ProjectSettings/GvhProjectSettings.xml");
            var doc = new XmlDocument();
            doc.Load(reader);
            reader.Dispose();

            var promptNode =
                doc.SelectSingleNode(
                    "projectSettings/projectSetting[@name='GooglePlayServices.PromptBeforeAutoResolution']");
            if (promptNode != null)
            {
                var valueAttribute = promptNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    valueAttribute.Value = "False";
                }
                else
                {
                    if (promptNode is XmlElement promptElement)
                        promptElement.SetAttribute("value", "False");
                    else
                        throw new Exception("Failed to read GvhProjectSettings.xml file!");
                }
            }
            else
            {
                var projectSettingsNode = doc.SelectSingleNode("projectSettings");
                if (projectSettingsNode != null)
                {
                    var projectSettingNode = doc.CreateElement("projectSetting");
                    projectSettingNode.SetAttribute("name", "GooglePlayServices.PromptBeforeAutoResolution");
                    projectSettingNode.SetAttribute("value", "False");

                    projectSettingsNode.AppendChild(projectSettingNode);
                    doc.Save("ProjectSettings/GvhProjectSettings.xml");
                }
                else
                    throw new Exception("Failed to patch GvhProjectSettings.xml file!");
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"{Name}: Failed to disable prompt. {e.Message}");
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
