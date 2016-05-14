using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
namespace LTP.TextEditor.Document
{
	public class HighlightingManager
	{
        private ArrayList syntaxModeFileProviders = new ArrayList();
        private static HighlightingManager highlightingManager;
        private Hashtable highlightingDefs = new Hashtable();
        private Hashtable extensionsToName = new Hashtable();
        public event EventHandler ReloadSyntaxHighlighting;
        public Hashtable HighlightingDefinitions
        {
            get
            {
                return this.highlightingDefs;
            }
        }
        public static HighlightingManager Manager
        {
            get
            {
                return HighlightingManager.highlightingManager;
            }
        }
        static HighlightingManager()
        {
            HighlightingManager.highlightingManager = new HighlightingManager();
            HighlightingManager.highlightingManager.AddSyntaxModeFileProvider(new ResourceSyntaxModeProvider());
        }
        public HighlightingManager()
        {
            this.CreateDefaultHighlightingStrategy();
        }
        public void AddSyntaxModeFileProvider(ISyntaxModeFileProvider syntaxModeFileProvider)
        {
            foreach (SyntaxMode syntaxMode in syntaxModeFileProvider.SyntaxModes)
            {
                this.highlightingDefs[syntaxMode.Name] = new DictionaryEntry(syntaxMode, syntaxModeFileProvider);
                string[] extensions = syntaxMode.Extensions;
                for (int i = 0; i < extensions.Length; i++)
                {
                    string text = extensions[i];
                    this.extensionsToName[text.ToUpper()] = syntaxMode.Name;
                }
            }
            if (!this.syntaxModeFileProviders.Contains(syntaxModeFileProvider))
            {
                this.syntaxModeFileProviders.Add(syntaxModeFileProvider);
            }
        }
        public void ReloadSyntaxModes()
        {
            this.highlightingDefs.Clear();
            this.extensionsToName.Clear();
            this.CreateDefaultHighlightingStrategy();
            foreach (ISyntaxModeFileProvider syntaxModeFileProvider in this.syntaxModeFileProviders)
            {
                this.AddSyntaxModeFileProvider(syntaxModeFileProvider);
            }
            this.OnReloadSyntaxHighlighting(EventArgs.Empty);
        }
        private void CreateDefaultHighlightingStrategy()
        {
            DefaultHighlightingStrategy defaultHighlightingStrategy = new DefaultHighlightingStrategy();
            defaultHighlightingStrategy.Extensions = new string[0];
            defaultHighlightingStrategy.Rules.Add(new HighlightRuleSet());
            this.highlightingDefs["Default"] = defaultHighlightingStrategy;
        }
        private IHighlightingStrategy LoadDefinition(DictionaryEntry entry)
        {
            SyntaxMode syntaxMode = (SyntaxMode)entry.Key;
            ISyntaxModeFileProvider syntaxModeFileProvider = (ISyntaxModeFileProvider)entry.Value;
            DefaultHighlightingStrategy defaultHighlightingStrategy = HighlightingDefinitionParser.Parse(syntaxMode, syntaxModeFileProvider.GetSyntaxModeFile(syntaxMode));
            this.highlightingDefs[syntaxMode.Name] = defaultHighlightingStrategy;
            defaultHighlightingStrategy.ResolveReferences();
            return defaultHighlightingStrategy;
        }
        public IHighlightingStrategy FindHighlighter(string name)
        {
            object obj = this.highlightingDefs[name];
            if (obj is DictionaryEntry)
            {
                return this.LoadDefinition((DictionaryEntry)obj);
            }
            return (IHighlightingStrategy)((obj == null) ? this.highlightingDefs["Default"] : obj);
        }
        public IHighlightingStrategy FindHighlighterForFile(string fileName)
        {
            string text = (string)this.extensionsToName[Path.GetExtension(fileName).ToUpper()];
            if (text == null)
            {
                return (IHighlightingStrategy)this.highlightingDefs["Default"];
            }
            object obj = this.highlightingDefs[text];
            if (obj is DictionaryEntry)
            {
                return this.LoadDefinition((DictionaryEntry)obj);
            }
            return (IHighlightingStrategy)((obj == null) ? this.highlightingDefs["Default"] : obj);
        }
        protected virtual void OnReloadSyntaxHighlighting(EventArgs e)
        {
            if (this.ReloadSyntaxHighlighting != null)
            {
                this.ReloadSyntaxHighlighting(this, e);
            }
        }
	}
}
