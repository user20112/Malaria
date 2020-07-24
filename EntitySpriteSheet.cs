using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Malaria2
{
    public class EntitySpriteSheet
    {
        public Texture2D Sheet;
        private int NumberOfRows;
        private int PerRow;
        public static int EntityHeight = 64;
        public static int EntityWidth = 32;

        public EntitySpriteSheet(Texture2D sheet, int numberOfRows, int perRow)
        {
            Sheet = sheet;
            NumberOfRows = numberOfRows;
            PerRow = perRow;
        }

        public Rectangle GetSprite(int ID)
        {
            return new Rectangle((ID % PerRow) * EntityWidth, (ID / PerRow) * EntityHeight, EntityWidth, EntityHeight);
        }
    }
}