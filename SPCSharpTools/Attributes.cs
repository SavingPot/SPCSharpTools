using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    [AttributeUsage(AttributeTargets.All)]
    public class ChineseNameAttribute : Attribute
	{
		public readonly string chineseName;

		public ChineseNameAttribute(string chineseName)
		{
			this.chineseName = chineseName;
		}
	}

}
