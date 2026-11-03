using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace JingFamily {
 public static class Tests {
  static int count;static void Check(bool ok,string message){if(!ok)throw new Exception(message);count++;}
  static void Reject(Action a,string message){bool thrown=false;try{a();}catch{thrown=true;}Check(thrown,message);}
  public static int Run(string[] args){System.Windows.Forms.Application.EnableVisualStyles();System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);string dir=args.Length>1?Path.GetFullPath(args[1]):Path.Combine(Path.GetTempPath(),"JingFamilyTest-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(dir);Store.Root=Path.Combine(dir,"data");try{
   Store.Load();Check(Store.Data.People.Count==0,"new store");Store.Data=Store.Sample();Store.Save();Store.Load();Check(Store.Data.People.Count==7,"XML round trip");Check(Store.IsAncestor(Store.Data,Store.Data.People[0].Id,Store.Data.People[6].Id),"ancestor traversal");
   var broken=Store.Deserialize(Store.Serialize(Store.Data));broken.People[0].FatherId=broken.People[6].Id;Reject(()=>Store.Validate(broken),"cycle detection");broken=Store.Deserialize(Store.Serialize(Store.Data));broken.People[0].Spouses.Clear();Reject(()=>Store.Validate(broken),"reciprocal spouse");Reject(()=>Store.CheckDate("2023-02-29"),"invalid date");Store.CheckDate("2024-02-29");count++;Reject(()=>Store.CheckMedia("media/../../secret"),"traversal");Reject(()=>Store.CheckMedia("C:/secret"),"absolute path");
   string attachment=Path.Combine(dir,"source.txt");File.WriteAllText(attachment,"test attachment 中文");string media=Store.AddMedia(attachment);Store.Data.Documents.Add(new Document{Title="测试附件",File=media,PersonId=Store.Data.People[0].Id});Store.Save();
   string zip=Path.Combine(dir,"backup.zip");Store.Backup(zip);Dictionary<string,byte[]> files;var restored=Store.ReadBackup(zip,out files);Check(restored.People.Count==7&&files.Count==1,"ZIP backup round trip");Check(System.Text.Encoding.UTF8.GetString(files[media]).Contains("中文"),"attachment bytes");
   string ged=Path.Combine(dir,"family.ged");Store.ExportGed(ged);var imported=Store.ImportGed(ged);Check(imported.People.Count==7,"GEDCOM people");Check(imported.People.Count(p=>p.FatherId!="")==4,"GEDCOM parents");Check(imported.People.Sum(p=>p.Spouses.Count)==4,"GEDCOM spouses");Check(imported.People.First(p=>p.Name=="景明远").Birth=="1938-03-12","GEDCOM date");
   Store.Data.People[0].Biography="<script>alert(1)</script>";string html=Path.Combine(dir,"family.html");Reports.ExportHtml(html);Check(!File.ReadAllText(html).Contains("<script>alert(1)</script>"),"HTML escaping");Store.ExportJson(Path.Combine(dir,"family.json"));var json=new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<FamilyData>(File.ReadAllText(Path.Combine(dir,"family.json")));Store.Validate(json);Check(json.Documents.Count==1,"JSON data");Store.Data.People[0].Name="=DANGER";Store.ExportCsv(Path.Combine(dir,"people.csv"));Check(File.ReadAllText(Path.Combine(dir,"people.csv")).Contains("'=DANGER"),"CSV formula protection");
   string deleted=Store.Data.People[0].Id;Store.DeletePerson(deleted);Check(Store.Data.People.All(p=>p.FatherId!=deleted&&!p.Spouses.Contains(deleted)),"delete unlinks");Check(Store.Data.Documents[0].PersonId=="","delete preserves attachment");Store.Replace(restored,files);Check(Directory.GetFiles(Path.Combine(Store.Root,"backups"),"*.zip").Length==1,"automatic pre-restore backup");Check(Store.Data.People.Count==7,"restore replaces");
   Check(RelationWindow.FindPath(Store.Data.People[0].Id,Store.Data.People[6].Id).Contains("女儿"),"relationship path");
   Check(Store.DateStart("1980-12")<=Store.DateEnd("1980"),"partial date overlap");
   string atom=Path.Combine(dir,"atomic.zip");File.WriteAllText(atom,"old backup");Store.Data.Documents.Add(new Document{Title="missing",File="media/missing.txt"});Reject(()=>Store.Backup(atom),"failed backup rejected");Check(File.ReadAllText(atom)=="old backup","failed backup preserves existing file");Store.Data.Documents.RemoveAt(Store.Data.Documents.Count-1);
   using(var tree=new FamilyTree()){tree.Relayout();tree.ExportPng(Path.Combine(dir,"tree.png"));Check(new FileInfo(Path.Combine(dir,"tree.png")).Length>1000,"PNG render");}

   using(var win=new MainWindow()){win.CreateControl();foreach(string page in new[]{"家谱概览","成员档案","世系图谱","家族大事记","资料收藏","数据管理","关于软件"}){typeof(MainWindow).GetMethod("Navigate",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).Invoke(win,new object[]{page});win.PerformLayout();count++;}}
   using(var ed=new PersonEditor(Store.Data.People[0])){ed.CreateControl();count++;}using(var ed=new EventEditor(null)){ed.CreateControl();count++;}using(var ed=new DocumentEditor(null)){ed.CreateControl();count++;}
   count+=TemplateTests.Run(dir);
   count+=WorkshopTests.Run(dir);
   count+=DiagramTests.Run(dir);
   count+=LibraryTests.Run(dir);
   count+=PurgeTests.Run(dir);
   count+=VisualTests.Run(dir);
   File.WriteAllText(Path.Combine(dir,"test-results.txt"),"PASS: "+count+" checks\r\n"+DateTime.Now.ToString("s"));return 0;
  }catch(Exception e){File.WriteAllText(Path.Combine(dir,"test-results.txt"),"FAIL after "+count+": "+e);return 1;}}
 }
}
