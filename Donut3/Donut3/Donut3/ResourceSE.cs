using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte
{
	public class ResourceSE
	{
		public DDSE PauseIn = new DDSE(@"Fairy\Donut3\Taira\coin05.mp3");
		public DDSE PauseOut = new DDSE(@"Fairy\Donut3\Taira\coin07.mp3");

		// 以下不使用

		public DDSE Bomb = new DDSE(@"Taira\bomb.mp3");
		public DDSE Coin04 = new DDSE(@"Taira\coin04.mp3");
		public DDSE CrrectAnswer2 = new DDSE(@"Taira\Crrect_answer2.mp3");
		public DDSE Explosion05 = new DDSE(@"Taira\explosion05.mp3");
		public DDSE Jump12 = new DDSE(@"Taira\jump12.mp3");
		public DDSE Powerup04 = new DDSE(@"Taira\powerup04.mp3");
		public DDSE Poyo = new DDSE(@"Taira\poyo.mp3");
		public DDSE Question = new DDSE(@"Taira\question.mp3");
		public DDSE Surprise = new DDSE(@"Taira\Surprise.mp3");
	}
}
