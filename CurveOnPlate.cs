using System;
using System.IO;
using System.Text;
using System.Collections.Generic;


namespace Tarelka
{

class Base
{
  public static double Radius;
  
  public static void Main()
  {
  List<Vertex> vertexList=new List<Vertex>();
    
  Console.WriteLine("работать с консолью(1) или с файлом(2)?");
  int k=Convert.ToInt32(Console.ReadLine());
  Console.WriteLine("Введите радиус тарелки");
  Radius=Convert.ToDouble(Console.ReadLine());
  switch(k)//заполнение списка вершин координатами
  {
  
  case 1://считанными из консоли
  {
  Console.WriteLine("Введите координаты точек в формате r1:fi1 r2:fi2 ...");
  string s=Console.ReadLine();
  string[] elts=s.Split(' ');
  foreach (string elt in elts)
         vertexList.Add(Vertex.FromString(elt));
  break;
  }
  
  case 2://считанными из файла
  {
  Console.WriteLine("Введите полное имя файла");
  string f=Console.ReadLine();
  string dir = AppDomain.CurrentDomain.BaseDirectory;
  string line;
  StreamReader file=new StreamReader(dir+f);
  while ((line=file.ReadLine())!=null)
   vertexList.Add(Vertex.FromString(line));
  file.Close(); 
  break;
  }
 
  }
  if(vertexList[0].Rad!=Radius || vertexList[c].Rad!=Radius)
  throw new Exception ("Крайние вершины должны лежать на окружности");
    int c=vertexList.Count-1;
  if (Osn(vertexList, c)==true)
  Console.WriteLine("Трещина позволяет раздвинуть части тарелки");
  else
  Console.WriteLine("Трещина не позволяет раздвинуть части тарелки");
  }
 
  static public bool Osn( List<Vertex> xList, int count)
  {
   double levgr=0, pravgr=2*Math.PI,onegr,secondgr;
   bool flag=true;
   for(int i=1; (i<count) && (flag==true);i++)
   {
   onegr=xList[i-1].Perehod(xList[i].Absciss,xList[i].Ordinat);
   secondgr=xList[i+1].Perehod(xList[i].Absciss,xList[i].Ordinat);
   if(secondgr<onegr)
   Swap (ref secondgr, ref onegr);
   if(secondgr<levgr || onegr>pravgr)
   flag=false;
   else 
   {
   if(secondgr<pravgr)
   pravgr=secondgr;
   if(onegr>levgr)
   levgr=onegr;
   }  
   }
   
   return flag;
  }
  
  public static void Swap(ref double a, ref double b)
  {
  double t=a;
  a=b;
  b=t;
  }
  
  }
 
  
  public class Vertex
  {
   private double r;
   private double fi;
   private double x;
   private double y;
  
  public double Rad
  {
  get 
  { 
  return r;
  }
  set 
  { 
        if (value>=0 && value<=Base.Radius) 
         r=value;
        else throw new Exception("Некорректное значение радиуса у одной из вершин"); 
  }
  }
  
  public double Ugol
  {
  get 
  {
  return fi;
  } 
  set 
  { 
  double a=2*Math.PI;
  fi=value;
  while (fi<0)
  {
  fi+=a;
  }
  while (fi>a)
  {
  fi-=a;
  }
  
  }  
  }
  
  public double Absciss
  {
  get
  {
  return x;
  }
  set
  {
    x=value;
  }  
  }
  
  public double Ordinat
  {
  get
  {
  return y;
  }
  set
  {
    y=value;
  } 
  }
  
  public override string ToString()
  {
  return string.Format ("rad {0} ugol {1} absc {2} ordin {3}", r, fi, x, y);
  }
  
  public static Vertex FromString(string s)
  {
  Vertex one=new Vertex();
   string[] coords=s.Split(':');
   one.Rad=Convert.ToDouble(coords[0]);
   one.Ugol=Convert.ToDouble(coords[1]);
   one.Absciss=one.Rad*Math.Cos(one.Ugol);
   one.Ordinat=one.Rad*Math.Sin(one.Ugol);
   return one;
  }
  
    public double Perehod(double xcentr, double ycentr)
  {
   double xnew=this.x-xcentr;
   double ynew=this.y-ycentr;
   return Math.Atan(ynew/xnew);
  }
  
  }
  



}
