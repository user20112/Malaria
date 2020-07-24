using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Malaria2.Interfaces
{
    public interface ITile
    {
        bool Passable { get; set; }
        bool Liquid { get; set; }
        bool ActOnUpdate { get; set; }
        bool NeedsToDraw { get; set; }

        void Update(GameTime gametime, IEntity[] OnBlock);

        void Draw(int x, int y, int height, int width, SpriteBatch g, SpriteSheet sheet, float SpriteDepth);
    }
}