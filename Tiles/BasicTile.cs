using Malaria2.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Malaria2.Tiles
{
    public class BasicTile : ITile
    {
        public bool Passable { get; set; }
        public bool Liquid { get; set; }
        public bool ActOnUpdate { get; set; }
        public bool NeedsToDraw { get; set; }
        public int ID;

        public BasicTile(bool passable, bool liquid, bool needsToDraw, int ID)
        {
            this.ID = ID;
            Passable = passable;
            Liquid = liquid;
            NeedsToDraw = needsToDraw;
        }

        public void Draw(int x, int y, int height, int width, SpriteBatch g, SpriteSheet sheet, float SpriteDepth)
        {
            g.Draw(sheet.Sheet, new Rectangle(x, y, width, height), sheet.GetSprite(ID), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, SpriteDepth);
        }

        public void Update(GameTime gametime, IEntity[] OnBlock)
        {
        }
    }
}