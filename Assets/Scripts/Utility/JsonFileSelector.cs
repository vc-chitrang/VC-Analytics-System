using UnityEngine;
using SimpleFileBrowser;
using System; // Namespace for SimpleFileBrowser

public class JsonFileSelector:MonoBehaviour {
	public static Action<string> onJsonContentLoaded = null;

	public void BrowseJsonFile() {
		// Show the file browser
		FileBrowser.SetFilters(true,new FileBrowser.Filter("JSON Files",".json"));
		FileBrowser.SetDefaultFilter(".json");
		FileBrowser.SetExcludedExtensions(".lnk",".tmp",".exe");

		FileBrowser.ShowLoadDialog((paths) => {
			// This callback executes when the user selects a file
			string selectedFile = paths[0];
			Debug.Log("Selected File: " + selectedFile);
			ProcessJsonFile(selectedFile);
		},
		() => {
			// This callback executes when the user cancels the dialog
			Debug.Log("File selection canceled.");
		},
		FileBrowser.PickMode.Files);
	}

	private void ProcessJsonFile(string filePath) {
		// Example: Read and print the content of the selected JSON file
		string jsonContent = System.IO.File.ReadAllText(filePath);
		Debug.Log("JSON Content: " + jsonContent);
		onJsonContentLoaded?.Invoke(jsonContent);
	}
}//JsonFileSelector class end.
