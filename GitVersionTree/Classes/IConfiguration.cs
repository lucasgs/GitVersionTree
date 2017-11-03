using System;

namespace GitVersionTree
{
	public interface IConfiguration
	{
		string Read(string Name, string Section = "");
		void Write(string Name, string Value, string Section = "");

	}
}

