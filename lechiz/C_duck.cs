using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lechiz
{
    class C_duck : IDisposable
    {
        public Image duck_img { get; private set; }
        protected int id;
        public Rectangle cords { get; set; }
        public C_duck(Image duck_image,int id,int cord_duck_x,int cord_duck_y)
        {
            this.duck_img = duck_image;
            this.id = id;
            this.cords = new Rectangle(new Point(cord_duck_x, cord_duck_y), new Size(50, 50));
            
        }
        public void Dispose()
        {
            GC.Collect(GC.GetGeneration(this.duck_img));
            GC.Collect(GC.GetGeneration(this.id));
            GC.Collect(GC.GetGeneration(this.cords));
        }
    }
    
}
