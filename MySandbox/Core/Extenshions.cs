using Microsoft.Xna.Framework;

namespace MySandbox.Core
{
    public class Extenshions
    {

        public static Rectangle[] MyCreatePatches(Rectangle rectangle, int lp=0, int rp=0, int tp=0, int bp=0, int sx=-1, int sy=-1)
        {
            

            var x = rectangle.X;
            var y = rectangle.Y;
            var w = rectangle.Width;
            var h = rectangle.Height;

            if (sx == -1)
                sx = rectangle.Width - lp - rp;

            if (sy == -1)
                sy = rectangle.Height - bp - tp;

            return new Rectangle[] {
                new Rectangle(x,y,lp,tp),
                new Rectangle(x+lp,y,sx, tp),
                new Rectangle(x+lp+sx,y,rp,tp),

                new Rectangle(x,y+tp,lp,sy),
                new Rectangle(x+lp,y+tp,sx,sy),
                new Rectangle(x+lp+sx,y+tp,rp,sy),

                new Rectangle(x,y+tp+sy,lp,bp),
                new Rectangle(x+lp,y+tp+sy,sx,bp),
                new Rectangle(x+lp+sx,y+tp+sy,rp,bp)

            };
        }
    }
}
