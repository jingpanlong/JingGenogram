using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
namespace JingFamily { public static class BrandMark {
 public static Color Green=Color.FromArgb(23,63,53),Gold=Color.FromArgb(221,190,119),Paper=Color.FromArgb(247,235,202);
 static GraphicsPath Round(){var p=new GraphicsPath();p.AddArc(0,0,108,108,180,90);p.AddArc(148,0,108,108,270,90);p.AddArc(148,148,108,108,0,90);p.AddArc(0,148,108,108,90,90);p.CloseFigure();return p;}
 public static void Draw(Graphics g){g.SmoothingMode=SmoothingMode.AntiAlias;using(var bg=new SolidBrush(Green))using(var round=Round())g.FillPath(bg,round);
  using(var tree=new Pen(Gold,8)){tree.StartCap=tree.EndCap=LineCap.Round;g.DrawLine(tree,128,153,128,76);g.DrawBezier(tree,128,132,128,107,90,126,84,103);g.DrawBezier(tree,128,132,128,107,166,126,172,103);}
  using(var gold=new SolidBrush(Gold)){g.FillEllipse(gold,114,44,28,28);g.FillEllipse(gold,72,78,24,24);g.FillEllipse(gold,160,78,24,24);}
  using(var book=new GraphicsPath()){book.AddBezier(48,151,77,137,101,140,128,156);book.AddBezier(128,156,155,140,179,137,208,151);book.AddLine(208,151,208,207);book.AddBezier(208,207,178,193,151,197,128,215);book.AddBezier(128,215,105,197,78,193,48,207);book.CloseFigure();using(var fill=new SolidBrush(Paper))g.FillPath(fill,book);using(var outline=new Pen(Gold,4))g.DrawPath(outline,book);}
  using(var line=new Pen(Green,3.5f)){line.StartCap=line.EndCap=LineCap.Round;g.DrawLine(line,128,159,128,209);g.DrawBezier(line,63,165,81,159,99,163,111,170);g.DrawBezier(line,63,179,81,173,99,177,111,184);g.DrawBezier(line,145,170,157,163,175,159,193,165);g.DrawBezier(line,145,184,157,177,175,173,193,179);}
 }
 public static Bitmap Render(int size){var b=new Bitmap(size,size,PixelFormat.Format32bppArgb);using(var g=Graphics.FromImage(b)){g.Clear(Color.Transparent);g.ScaleTransform(size/256f,size/256f);Draw(g);}return b;}
 public const string Svg="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 256 256'><title>景·谱：家谱与家族树</title><rect width='256' height='256' rx='54' fill='#173f35'/><g stroke='#ddbe77' stroke-width='8' stroke-linecap='round' fill='none'><path d='M128 153V76M128 132C128 107 90 126 84 103M128 132C128 107 166 126 172 103'/></g><g fill='#ddbe77'><circle cx='128' cy='58' r='14'/><circle cx='84' cy='90' r='12'/><circle cx='172' cy='90' r='12'/></g><path d='M48 151C77 137 101 140 128 156C155 140 179 137 208 151V207C178 193 151 197 128 215C105 197 78 193 48 207Z' fill='#f7ebca' stroke='#ddbe77' stroke-width='4'/><g fill='none' stroke='#173f35' stroke-width='3.5' stroke-linecap='round'><path d='M128 159V209M63 165C81 159 99 163 111 170M63 179C81 173 99 177 111 184M145 170C157 163 175 159 193 165M145 184C157 177 175 173 193 179'/></g></svg>";
 public static string HtmlHead(){return "<link rel='icon' type='image/svg+xml' href='data:image/svg+xml;base64,"+Convert.ToBase64String(Encoding.UTF8.GetBytes(Svg))+"'>";}
 public static string HtmlImage(int size){return "<img alt='景·谱 Logo' width='"+size+"' height='"+size+"' src='data:image/svg+xml;base64,"+Convert.ToBase64String(Encoding.UTF8.GetBytes(Svg))+"'>";}
} }