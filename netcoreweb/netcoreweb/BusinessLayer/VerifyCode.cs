using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using netcoreweb.Models;
using SkiaSharp;

namespace netcoreweb.BusinessLayer
{
	public static class VerifyCode
	{
		private static Timer _timer;

		private static Dictionary<string, VerifyCodeInfo> _verifyCodeDictionary;

		/// <summary>
		/// 所有验证码列表
		/// </summary>
		public static Dictionary<string, VerifyCodeInfo> VerifyCodeDictionary
		{
			get => _verifyCodeDictionary ?? (_verifyCodeDictionary = new Dictionary<string, VerifyCodeInfo>());
			set => _verifyCodeDictionary = value;
		}

		private static string RndNum(int vcodeNum)
		{
			//验证码可以显示的字符集合  
			var vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
						   ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
						   ",R,S,T,U,V,W,X,Y,Z";
			var vcArray = vchar.Split(new char[] { ',' });//拆分成数组   
			var code = "";//产生的随机数  
			var temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数  

			var rand = new Random();
			//采用一个简单的算法以保证生成随机数的不同  
			for (var i = 1; i < vcodeNum + 1; i++)
			{
				if (temp != -1)
				{
					rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类  
				}
				int t = rand.Next(vcArray.Length - 1);//获取随机数  
				if (temp != -1 && temp == t)
				{
					return RndNum(vcodeNum);//如果获取的随机数重复，则递归调用  
				}
				temp = t;//把本次产生的随机数记录起来  
				code += vcArray[t];//随机数的位数加一  
			}
			return code;
		}

		public static byte[] GetVerifyBytes(string guid)
		{
			byte[] imageBytes = null;

			if (VerifyCodeDictionary.ContainsKey(guid))
			{
				imageBytes = GetImg(VerifyCodeDictionary[guid].Code);
			}
			else
			{
				imageBytes = GetImg("");
			}
			return imageBytes;
		}

		/// <summary>
		/// 生成图像    //todo 创建动态gif图
		/// </summary>
		/// <param name="OldCodeKey"></param>
		/// <returns></returns>
		public static string CreateNewVerifyCode(string OldCodeKey = null)
		{
			if (!string.IsNullOrEmpty(OldCodeKey))
			{
				VerifyCodeDictionary.Remove(OldCodeKey);
			}
			var length = new Random().Next(4, 6);
			var newNum = RndNum(length);
			var newGuid = Guid.NewGuid().ToString();
			VerifyCodeDictionary.Add(newGuid, new VerifyCodeInfo(newGuid, newNum));

			if (_timer == null)
			{
				_timer = new Timer(ClearVerifyDic, null, new TimeSpan(0, 0, 5, 10), new TimeSpan(0, 0, 5, 10));
			}

			return newGuid;
		}

		private static void ClearVerifyDic(object state)
		{
			lock (_verifyCodeDictionary)
			{
				var keylist = new List<string>();
				foreach (var verifyCodeInfo in VerifyCodeDictionary)
				{
					if ((DateTime.Now - verifyCodeInfo.Value.CretetimeDateTime).TotalMinutes >= 5)
					{
						keylist.Add(verifyCodeInfo.Key);
					}
				}
				foreach (var key in keylist)
				{
					try
					{
						VerifyCodeDictionary.Remove(key);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				}

				if (VerifyCodeDictionary.Count <= 0)
				{
					if (_timer != null)
					{
						_timer.Dispose();
						_timer = null;
					}
				}

			}
		}


		public static SKPaint CreatePaint()
		{
			string font = @"";
			font = @"Arial";
			font = @"Liberation Serif";
			font = @"Segoe Script";
			font = @"Consolas";
			font = @"Comic Sans MS";
			font = @"SimSun";
			font = @"Impact";
			return CreatePaint(SKColors.White, font, 40, SKTypefaceStyle.Normal);
		}


		public static SKPaint CreatePaint(SKColor color, string fontName, float fontSize, SKTypefaceStyle fontStyle)
		{
			SkiaSharp.SKTypeface font = SkiaSharp.SKTypeface.FromFamilyName(fontName, fontStyle);
			SKPaint paint = new SKPaint();
			paint.IsAntialias = true;
			paint.Color = color;
			// paint.StrokeCap = SKStrokeCap.Round;
			paint.Typeface = font;
			paint.TextSize = fontSize;

			return paint;
		}

		public static SKPaint CreateLinePaint()
		{
			SKPaint paint = new SKPaint();

			paint.IsAntialias = true;
			paint.Color = SKColors.Blue;
			paint.StrokeCap = SKStrokeCap.Square;
			paint.StrokeWidth = 1;

			return paint;
		}

		private static byte[] GetImg(string showtext)
		{
			byte[] imageBytes = null;
			if (string.IsNullOrEmpty(showtext))
				return imageBytes;

			int image2Dx = 0;
			int image2Dy = 0;

			using (SKPaint drawStyle = CreatePaint())
			{
				var compensateDeepCharacters = (int)drawStyle.TextSize / 5;
				if (System.StringComparer.Ordinal.Equals(showtext, showtext.ToUpperInvariant()))
					compensateDeepCharacters = 0;
				var size = SkiaHelpers.MeasureText(showtext, drawStyle);
				image2Dx = (int)size.Width+10;
				image2Dy = (int)size.Height;
			}
			var zu = showtext.ToList();
			using (var bmp = new SKBitmap(image2Dx, image2Dy))
			{
				using (var canvas = new SKCanvas(bmp))
				{
					canvas.DrawColor(SKColors.White);//背景色
					using (var sKPaint = new SKPaint())
					{
						sKPaint.TextSize = 18;//字体大小
						sKPaint.IsAntialias = true;//开启抗锯齿                   
						sKPaint.Typeface = SKTypeface.FromFamilyName("微软雅黑", SKTypefaceStyle.Bold);//字体
						SKRect size = new SKRect();
						sKPaint.MeasureText(zu[0].ToString(), ref size);//计算文字宽度以及高度
						float temp = ((bmp.Width / 4) - size.Size.Width) / 2;
						float temp1 = bmp.Height - (bmp.Height - size.Size.Height) / 2;
						Random random = new Random();
						for (int i = 0; i < zu.Count; i++)
						{
							sKPaint.Color = new SKColor((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
							canvas.DrawText(zu[i].ToString(), temp + 20 * i, temp1, sKPaint);//画文字
						}
						//干扰线
						for (var i = 0; i < 10; i++)
						{
							sKPaint.Color = new SKColor((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
							canvas.DrawLine(random.Next(bmp.Width), random.Next(bmp.Height), random.Next(bmp.Width), random.Next(bmp.Height), sKPaint);
						}

						//画图片的前景干扰点  
						for (var i = 0; i < 100; i++)
						{
							bmp.SetPixel(random.Next(bmp.Width), random.Next(bmp.Height), new SKColor((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
						}
						sKPaint.Color = new SKColor((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
						//canvas.DrawRoundRect(bmp.rect,sKPaint);
					}

					//页面展示图片
					using (SKImage img = SKImage.FromBitmap(bmp))
					{
						using (SKData p = img.Encode())
						{
							imageBytes = p.ToArray();
						}
					}
					

				}
			}

			return imageBytes;

		}

		private static byte[] GetCaptcha(string captchaText)
		{
			byte[] imageBytes = null;
			int image2d_x = 0;
			int image2d_y = 0;
			SKRect size;
			int compensateDeepCharacters = 0;
			using (SKPaint drawStyle = CreatePaint())
			{
				compensateDeepCharacters = (int)drawStyle.TextSize / 5;
				if (System.StringComparer.Ordinal.Equals(captchaText, captchaText.ToUpperInvariant()))
					compensateDeepCharacters = 0;
				size = SkiaHelpers.MeasureText(captchaText, drawStyle);
				image2d_x = (int)size.Width + 10;
				image2d_y = (int)size.Height + 10 + compensateDeepCharacters;
			}
			using (SKBitmap image2d = new SKBitmap(image2d_x, image2d_y, SKColorType.Bgra8888, SKAlphaType.Premul))
			{
				using (SKCanvas canvas = new SKCanvas(image2d))
				{
					canvas.DrawColor(SKColors.Black); // Clear 
					using (SKPaint drawStyle = CreatePaint())
					{
						canvas.DrawText(captchaText, 0 + 5, image2d_y - 5 - compensateDeepCharacters, drawStyle);
					}
					using (SKImage img = SKImage.FromBitmap(image2d))
					{
						using (SKData p = img.Encode(SKEncodedImageFormat.Png, 100))
						{
							imageBytes = p.ToArray();
						}
					}
				}
			}
			return imageBytes;
		}

	}
}
