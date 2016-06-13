using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Windows.UI;
using System.ComponentModel.DataAnnotations;

namespace demo3th
{
    class ColorContext:DbContext
    {
        
        public DbSet<Colorinfo> colors { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=color.db");
        }
    }
    class Colorinfo
    {
        [Key]public string name { get; set; }
        public string value { get; set; }
        public Colorinfo(string n ,string v)
        {
            name = n;
            value = v;
        }
        
    }
    class MyClass
    {
        public void init()
        {
            using (var db=new ColorContext())
            {
                var ColorList = new List<Windows.UI.Color> {Colors.Black,
                Colors.Blue,Colors.Brown,Colors.Gold,Colors.Gray,
                Colors.Green,Colors.Orange,Colors.Pink,Colors.Purple,
                Colors.Red,Colors.White,Colors.Yellow };
                var sl = new List<string> { "Black", "Blue", "Brown", "Gold", "Gray", "Green", "Orange", "Pink", "Purple", "Red", "White", "Yellow" };
                var t= ColorList.GetEnumerator();
                foreach (var c in sl)
                {
                    db.colors.Add(new Colorinfo(c,t.Current.ToString()));
                }
                db.Database.Migrate();
            }
        } 
        public void read()
        {
            using (var db = new ColorContext())
            {

                var l = db.colors;
               
            }
        }
    }
}
