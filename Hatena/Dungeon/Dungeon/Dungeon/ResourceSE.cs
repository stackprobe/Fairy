using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte
{
	public class ResourceSE
	{
		public DDSE PauseEnter = new DDSE(@"SE\coin01.mp3");
		public DDSE PauseLeave = new DDSE(@"SE\coin02.mp3");
		public DDSE Restart = new DDSE(@"SE\coin03.mp3");
		public DDSE ReturnToTitle = new DDSE(@"SE\coin04.mp3");
		public DDSE Poka = new DDSE(@"SE\poka02.mp3");
	}
}
