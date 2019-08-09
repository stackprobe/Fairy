using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	public class GameCommonEffect
	{
		public GameTaskList TL = GameGround.EL;
		public List<GamePicture> Pictures = new List<GamePicture>();
		public int FramePerPicture = 1;
		public double X = GameConsts.Screen_W / 2.0;
		public double Y = GameConsts.Screen_H / 2.0;
		public double R = 0.0;
		public double Z = 1.0;
		public double A = 1.0;
		public double XAdd = 0.0;
		public double YAdd = 0.0;
		public double RAdd = 0.0;
		public double ZAdd = 0.0;
		public double AAdd = 0.0;
		public double XAdd2 = 0.0;
		public double YAdd2 = 0.0;
		public double RAdd2 = 0.0;
		public double ZAdd2 = 0.0;
		public double AAdd2 = 0.0;

		public GameCommonEffect()
		{ }

		public GameCommonEffect(GamePicture picture)
		{
			this.Pictures.Add(picture);
		}

		private IEnumerable<bool> GetRoutine()
		{
			if (this.Pictures.Count == 0) // ? 画像が追加されていない。
				throw new GameError();

			int outOfCameraFrame = 0;

			for (int frame = 0; ; frame++)
			{
				double drawX = this.X - GameGround.ICamera.X;
				double drawY = this.Y - GameGround.ICamera.Y;

				GameDraw.SetTaskList(this.TL);
				GameDraw.SetAlpha(this.A);
				GameDraw.DrawBegin(this.Pictures[(frame / this.FramePerPicture) % this.Pictures.Count], drawX, drawY);
				GameDraw.DrawRotate(this.R);
				GameDraw.DrawZoom(this.Z);
				GameDraw.DrawEnd();

				this.X += this.XAdd;
				this.Y += this.YAdd;
				this.R += this.RAdd;
				this.Z += this.ZAdd;
				this.A += this.AAdd;

				this.XAdd += this.XAdd2;
				this.YAdd += this.YAdd2;
				this.RAdd += this.RAdd2;
				this.ZAdd += this.ZAdd2;
				this.AAdd += this.AAdd2;

				if (GameUtils.IsOutOfScreen(new D2Point(drawX, drawY)))
				//if (GameUtils.IsOutOfCamera(new D2Point(this.X, this.Y)))
				{
					outOfCameraFrame++;

					if (20 < outOfCameraFrame)
						break;
				}
				else
					outOfCameraFrame = 0;

				if (this.A < 0.0)
					break;

				yield return true;
			}
		}

		public IGameTask GetTask()
		{
			return new GameIEnumerableTask(this.GetRoutine(), () => { });
		}
	}
}
