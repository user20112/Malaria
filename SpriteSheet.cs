using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Malaria2
{
    public class SpriteSheet
    {
        public Texture2D Sheet;
        private int NumberOfRows;
        private int PerRow;
        public static int SpriteSize = 16;

        public SpriteSheet(Texture2D sheet, int numberOfRows, int perRow)
        {
            Sheet = sheet;
            NumberOfRows = numberOfRows;
            PerRow = perRow;
        }

        public Rectangle GetSprite(int ID)
        {
            return new Rectangle((ID % PerRow) * SpriteSize, (ID / PerRow) * SpriteSize, SpriteSize, SpriteSize);
        }
    }
}