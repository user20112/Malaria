using Malaria2.Entities;
using Malaria2.Interfaces;
using Malaria2.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malaria2
{
    public class World
    {
        public List<IEntity> Entities;
        public List<IProjectile> Projectiles;
        public ITile[,] Tiles;
        public ITile[,] BackTiles;
        public static int Height;
        public static int Width;
        private Random Rand = new Random(DateTime.Now.Millisecond);
        private float[] data;

        public int GetGrassLevel(int x)
        {
            return (int)data[x];
        }

        public World(int height, int width)
        {
            Height = height;
            Width = width;
            Tiles = new ITile[width, height];
            BackTiles = new ITile[width, height];
            Entities = new List<IEntity>();
            Projectiles = new List<IProjectile>();
            Generate();
        }

        private void Generate()
        {
            PerlinNoiseGenerator NoiseGenerator = new PerlinNoiseGenerator();
            float min = float.MaxValue;
            float max = float.MinValue;
            float frequency = 0.5f;
            float amplitude = 1f;
            float MaxHeight = (int)(Height * .05);
            int MinHeight = (int)(Height * .80);
            data = new float[Width * Height];
            int octave = 5;
            for (int x = 0; x < octave; x++)
            {
                Parallel.For(0
                    , Width * Height
                    , (Current) =>
                    {
                        int CurX = Current % Width;
                        int CurY = Current / Width;
                        float noise = NoiseGenerator.Noise(CurX * frequency * 1f / Width, CurY * frequency * 1f / Height);
                        noise = data[CurY * Width + CurX] += noise * amplitude;
                        min = Math.Min(min, noise);
                        max = Math.Max(max, noise);
                    }
                );
                frequency *= 2;
                amplitude /= 2;
            }
            for (int x = 0; x < data.Length; x++)
            {
                data[x] = ((data[x] - min) / (max - min)) * (MaxHeight - MinHeight) + MinHeight;
            }
            for (int x = 0; x < Width; x++)
            {
                bool AddedGrass = false;
                for (int y = Height - 1; y >= 0; y--)
                {
                    if (y >= Height * .95)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky4);
                    }
                    if (y < Height * .95 && y >= Height * .90)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky3);
                    }
                    if (y < Height * .90 && y >= Height * .85)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky2);
                    }
                    int GrassHeight = (int)data[y * Width + x];
                    if (y < Height * .85 && y > GrassHeight)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky1);
                    }
                    if (y <= GrassHeight && y >= GrassHeight * .80 && AddedGrass)
                    {
                        Tiles[x, y] = new BasicTile(false, false, true, TileIDs.Dirt);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Dirt);
                    }
                    if (y <= GrassHeight && y >= GrassHeight * .80 && !AddedGrass)
                    {
                        AddedGrass = true;
                        Tiles[x, y] = new BasicTile(false, false, true, TileIDs.Grass);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Air);
                    }
                    if (y < GrassHeight * .80)
                    {
                        Tiles[x, y] = new BasicTile(false, false, true, TileIDs.Cobblestone);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Cobblestone);
                    }
                }
            }
        }

        private void generate()
        {
            for (int x = 0; x < Width; x++)
            {
                bool AddedGrass = false;
                for (int y = 0; y < Height; y++)
                {
                    if (y >= Height * .84)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky4);
                    }
                    if (y < Height * .84 && y >= Height * .68)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky3);
                    }
                    if (y < Height * .68 && y >= Height * .52)
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky2);
                    }
                    if (y < Height * .52 && y > Math.Round(Height * .36))
                    {
                        Tiles[x, y] = new BasicTile(true, false, false, TileIDs.Air);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Sky1);
                    }
                    if (y < Height * .36 && y >= Height * .20 && !AddedGrass)
                    {
                        AddedGrass = true;
                        Tiles[x, y] = new BasicTile(false, false, true, TileIDs.Sky1);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Grass);
                    }
                    if (y < Height * .36 && y >= Height * .20 && AddedGrass)
                    {
                        Tiles[x, y] = new BasicTile(false, false, true, TileIDs.Dirt);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Dirt);
                    }
                    if (y < Height * .20)
                    {
                        Tiles[x, y] = new BasicTile(false, false, true, TileIDs.Cobblestone);
                        BackTiles[x, y] = new BasicTile(true, false, true, TileIDs.Cobblestone);
                    }
                }
            }
        }

        public void Update(float x, float y, GameTime time, float ScreenHeight, float ScreenWidth)
        {
            int Screenbuffer = (int)(ScreenHeight * .2);
            int BlockWidth = (int)(ScreenHeight + Screenbuffer * 2) * 2 / SpriteSheet.SpriteSize;
            int BlockHeight = (int)(ScreenWidth + Screenbuffer * 2) * 2 / SpriteSheet.SpriteSize;
            foreach (IEntity Entity in Entities)
            {
                /*
                 1 2 3
                 4 5 6
                 7 8 9
                 10 11 12
                 */
                ITile[] nearby = new ITile[12];
                nearby[0] = Tiles[(int)Entity.Posx - 1, (int)Entity.Posy + 1];
                nearby[1] = Tiles[(int)Entity.Posx, (int)Entity.Posy + 1];
                nearby[2] = Tiles[(int)Entity.Posx + 1, (int)Entity.Posy + 1];
                nearby[3] = Tiles[(int)Entity.Posx - 1, (int)Entity.Posy];
                nearby[4] = Tiles[(int)Entity.Posx, (int)Entity.Posy];
                nearby[5] = Tiles[(int)Entity.Posx + 1, (int)Entity.Posy];
                nearby[6] = Tiles[(int)Entity.Posx - 1, (int)Entity.Posy - 1];
                nearby[7] = Tiles[(int)Entity.Posx, (int)Entity.Posy - 1];
                nearby[8] = Tiles[(int)Entity.Posx + 1, (int)Entity.Posy - 1];
                nearby[9] = Tiles[(int)Entity.Posx - 1, (int)Entity.Posy - 2];
                nearby[10] = Tiles[(int)Entity.Posx, (int)Entity.Posy - 2];
                nearby[11] = Tiles[(int)Entity.Posx + 1, (int)Entity.Posy - 2];
                Entity.Update(time, nearby);
            }
            List<IEntity> EntitiesOnTile = new List<IEntity>();
            for (int CurX = 0; CurX < BlockWidth; CurX++)
                for (int CurY = 0; CurY < BlockHeight; CurY++)
                {
                    bool UpdateBackTile = BackTiles[CurX + (int)x, CurY + (int)y].ActOnUpdate;
                    bool UpdateForeTile = Tiles[CurX + (int)x, CurY + (int)y].ActOnUpdate;
                    if (UpdateBackTile || UpdateForeTile)
                        foreach (IEntity entity in Entities)
                            if (entity.Posx + 1 == CurX && entity.Posy - 2 == CurY)
                                EntitiesOnTile.Add(entity);
                    if (UpdateBackTile)
                        BackTiles[CurX + (int)x, CurY + (int)y].Update(time, EntitiesOnTile.ToArray());
                    if (UpdateForeTile)
                        Tiles[CurX + (int)x, CurY + (int)y].Update(time, EntitiesOnTile.ToArray());
                    if (UpdateBackTile || UpdateForeTile)
                        EntitiesOnTile.Clear();
                }
        }

        public void Draw(float x, float y, float ScreenHeight, float ScreenWidth, SpriteBatch g, SpriteSheet sheet, EntitySpriteSheet ESheet)
        {
            int xoffset = (int)(x * SpriteSheet.SpriteSize % SpriteSheet.SpriteSize) * -1;
            int yoffset = (int)(y * SpriteSheet.SpriteSize % SpriteSheet.SpriteSize);
            int BlockWidth = (int)(ScreenWidth) / SpriteSheet.SpriteSize;
            int BlockHeight = (int)(ScreenHeight) / SpriteSheet.SpriteSize;
            x -= BlockWidth / 2;
            y -= BlockHeight / 2;
            int WidthBuffer = (int)(ScreenWidth * .1) / SpriteSheet.SpriteSize;
            int HeightBuffer = (int)(ScreenHeight * .1) / SpriteSheet.SpriteSize;
            for (int CurX = -1 * WidthBuffer; CurX < BlockWidth + WidthBuffer; CurX++)
            {
                for (int CurY = -1 * HeightBuffer; CurY < BlockHeight + HeightBuffer; CurY++)
                {
                    try
                    {
                        if (BackTiles[CurX + (int)x, CurY + (int)y].NeedsToDraw)
                            BackTiles[CurX + (int)x, CurY + (int)y].Draw((int)(CurX * SpriteSheet.SpriteSize + xoffset), (int)(ScreenHeight - SpriteSheet.SpriteSize - CurY * SpriteSheet.SpriteSize + yoffset), SpriteSheet.SpriteSize, SpriteSheet.SpriteSize, g, sheet, 1);
                        if (Tiles[CurX + (int)x, CurY + (int)y].NeedsToDraw)
                            Tiles[CurX + (int)x, CurY + (int)y].Draw((int)(CurX * SpriteSheet.SpriteSize + xoffset), (int)(ScreenHeight - SpriteSheet.SpriteSize - CurY * SpriteSheet.SpriteSize + yoffset), SpriteSheet.SpriteSize, SpriteSheet.SpriteSize, g, sheet, .5f);
                    }
                    catch
                    {
                    }
                }
            }
            foreach (IEntity Entity in Entities)
            {
                if (Entity is Player)
                {
                    Entity.Draw((int)(ScreenWidth / 2), (int)(ScreenHeight / 2), g, ESheet, 0);
                }
                else
                {
                }
            }
        }
    }
}