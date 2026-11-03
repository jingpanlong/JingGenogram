using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
namespace JingFamily {
 public static class VisualTests {
  public static void Render(Form form,string file){StudioStyle.Polish(form);var controls=form.Controls.Cast<Control>().ToArray();using(var root=new Panel{Size=form.ClientSize,Font=form.Font,BackColor=form.BackColor}){foreach(var c in controls)c.Parent=null;root.Controls.AddRange(controls);root.CreateControl();root.PerformLayout();using(var image=new Bitmap(root.Width,root.Height)){root.DrawToBitmap(image,new Rectangle(0,0,image.Width,image.Height));image.Save(file);}}}
  public static int Run(string dir){int count=0;string folder=Path.Combine(dir,"ui-preview");Directory.CreateDirectory(folder);foreach(string page in new[]{"家谱概览","家谱管理","成员档案","编修工作台","世系图谱","家族大事记","资料收藏","数据管理","关于软件"}){using(var win=new MainWindow()){typeof(MainWindow).GetMethod("Navigate",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).Invoke(win,new object[]{page});StudioStyle.Polish(win);if(page=="家谱概览"){var picker=(ComboBox)typeof(MainWindow).GetField("familyPicker",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(win);if(!picker.Text.Contains(Store.Data.Title))throw new Exception("顶部家谱选择器丢失选中值。");count++;}if(page=="成员档案"){var detail=(RichTextBox)typeof(MainWindow).GetField("detail",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(win);if(detail.TextLength==0)throw new Exception("成员详情未绑定选中档案。");count++;}Render(win,Path.Combine(folder,page+".png"));count++;}}
   using(var editor=new PersonEditor(Store.Data.People.First())){Render(editor,Path.Combine(folder,"成员编辑.png"));count++;}using(var editor=new FamilyEditor()){Render(editor,Path.Combine(folder,"家谱设置.png"));count++;}using(var dialog=new BrandMessage("操作提示","界面采用统一绿金配色。\r\n所有家谱仍分别保存。",true)){Render(dialog,Path.Combine(folder,"操作提示.png"));count++;}using(var dialog=new PurgeConfirmation("景氏家谱 · 分支卷",false,1)){Render(dialog,Path.Combine(folder,"永久删除确认.png"));count++;}
   var dummy=Library.Create("回收区界面示例");Library.Remove(dummy.Id);using(var panel=new LibraryPanel(()=>{})){panel.Size=new Size(1140,690);panel.CreateControl();var tabs=panel.Controls.OfType<TabControl>().Single();tabs.SelectedIndex=1;panel.PerformLayout();using(var image=new Bitmap(panel.Width,panel.Height)){panel.DrawToBitmap(image,new Rectangle(0,0,image.Width,image.Height));image.Save(Path.Combine(folder,"回收区.png"));}count++;}
   using(var combo=new ComboBox{DropDownStyle=ComboBoxStyle.DropDownList}){combo.Items.Add("当前家谱 · 示例");combo.SelectedIndex=0;StudioStyle.StyleCombo(combo);if(combo.SelectedItem.ToString()!="当前家谱 · 示例"||combo.Text!="当前家谱 · 示例")throw new Exception("组合框主题更改后丢失选中值。");count++;}
   using(var win=new RelationWindow()){Render(win,Path.Combine(folder,"亲缘查询.png"));count++;}using(var win=new DiagramSettings(new DiagramOptions())){Render(win,Path.Combine(folder,"图谱样式设置.png"));count++;}using(var win=new TaskEditor(null)){Render(win,Path.Combine(folder,"编修待办编辑.png"));count++;}return count;
  }
 }
}
