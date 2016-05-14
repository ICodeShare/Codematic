using System;
namespace LTP.TextEditor.Gui.InsightWindow
{
	public interface IInsightDataProvider
	{
		int InsightDataCount
		{
			get;
		}
		void SetupDataProvider(string fileName, TextArea textArea);
		bool CaretOffsetChanged();
		bool CharTyped();
		string GetInsightData(int number);
	}
}
