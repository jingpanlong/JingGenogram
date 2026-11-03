using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing.Imaging;
using JingFamily;
class MakeIcon {
 static Bitmap Render(int size){return BrandMark.Render(size);}
 static void Main(string[] args){string dir=Path.GetDirectoryName(Path.GetFullPath(args[0]));int[] sizes={16,24,32,48,64,128,256};var bytes=sizes.Select(size=>{using(var b=Render(size))using(var m=new MemoryStream()){b.Save(m,ImageFormat.Png);return m.ToArray();}}).ToArray();using(var w=new BinaryWriter(File.Create(args[0]))){w.Write((ushort)0);w.Write((ushort)1);w.Write((ushort)sizes.Length);int offset=6+16*sizes.Length;for(int i=0;i<sizes.Length;i++){w.Write((byte)(sizes[i]==256?0:sizes[i]));w.Write((byte)(sizes[i]==256?0:sizes[i]));w.Write((byte)0);w.Write((byte)0);w.Write((ushort)1);w.Write((ushort)32);w.Write(bytes[i].Length);w.Write(offset);offset+=bytes[i].Length;}foreach(var b in bytes)w.Write(b);}
  using(var b=Render(1024))b.Save(Path.Combine(dir,"JingPu-logo.png"),ImageFormat.Png);using(var b=Render(256))b.Save(Path.Combine(dir,"JingPu-icon-256.png"),ImageFormat.Png);
  using(var b=new Bitmap(1200,400)){using(var g=Graphics.FromImage(b)){g.Clear(Color.FromArgb(247,246,241));using(var mark=Render(256))g.DrawImage(mark,60,72,256,256);g.TextRenderingHint=System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;using(var f=new Font("Microsoft YaHei UI",74,FontStyle.Bold))using(var brush=new SolidBrush(BrandMark.Green))g.DrawString("景·谱",f,brush,365,75);using(var f=new Font("Microsoft YaHei UI",21))using(var brush=new SolidBrush(BrandMark.Green))g.DrawString("记录亲缘  ·  传承家风",f,brush,379,229);}b.Save(Path.Combine(dir,"JingPu-wordmark.png"),ImageFormat.Png);}
  File.WriteAllText(Path.Combine(dir,"JingPu-logo.svg"),BrandMark.Svg,new UTF8Encoding(false));
 }
}
