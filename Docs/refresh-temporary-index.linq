<Query Kind="Program" />

void Main()
{
	CreateDocs("readme.md");
}


void CreateDocs(string filename)
{
	var root = Path.Combine(new FileInfo(Util.CurrentQuery.FilePath).Directory.FullName, "v3");
	var dir = new DirectoryInfo(root);
	var sb = WriteHeading();
	
	foreach(var md in dir.GetFiles("*.md").Where(f => !f.Name.StartsWith("temporary")))
	{
		var description = GetTitle(md);
		var name = md.Name.Substring(0,md.Name.Length-3);
		var file = md.Name;
		RenderLink(sb, name, file, description);
	}
	WriteFooter(sb);
	var dest = Path.Combine(root, filename);
	File.WriteAllText(dest, sb.ToString());
	Console.WriteLine(sb.ToString());
}

string GetTitle(FileInfo file)
{
	return File.ReadAllLines(file.FullName)[4];
}


StringBuilder WriteHeading()
{
	var sb = new StringBuilder();
	sb.AppendLine($"## Temporary FluentAutomation documentation");
	sb.AppendLine();
	sb.AppendLine("method | summary");
	sb.AppendLine("--- | ---");
	return sb;
}

void WriteFooter(StringBuilder sb) {
	sb.AppendLine();
	sb.AppendLine($"Created : {DateTime.UtcNow.ToShortDateString()}");
}

void RenderLink(StringBuilder sb, string name, string markdownfile, string description)
{
	sb.AppendLine($"[{name}]({markdownfile})|{description}");
}