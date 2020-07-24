using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Malaria2.Interfaces
{
    public interface IEntity
    {
        float Posx { get; set; }
        float Posy { get; set; }

        void Draw(int x, int y, SpriteBatch g, EntitySpriteSheet sheet, float SpriteDepth);

        void Update(GameTime gametime, ITile[] SurroundingTiles);
    }
}