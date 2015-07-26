using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ZeldaStyle.Map {
    class MapManager : Component {

        List<Tile> Tiles;
        List<AnimatedTile> AnimatedTiles;

        Dictionary<int, int[]> IDs;

        public MapManager() {
            Tiles = new List<Tile>();
            IDs = new Dictionary<int, int[]>();
        }

        /// <summary>
        /// Loads a new map.
        /// </summary>
        /// <param name="path">Map path WITHOUT .json</param>
        public void LoadMap(string path, ContentManager content) {          
         
            string idsJSON = System.IO.File.ReadAllText(path + "_ids.json");
            List<IDHolder> ids = JsonConvert.DeserializeObject<List<IDHolder>>(idsJSON);


            for (int i = 0; i < ids.Count; i++) {
                IDs.Add(ids[i].ID, new int[] { ids[i].X, ids[i].Y });
            }
            
            //JsonConvert.
            
            

            string json;
            json = System.IO.File.ReadAllText(path + ".json");

            //read JSON data into anonymous type
            dynamic map = JsonConvert.DeserializeObject(json);
            Console.WriteLine("Loading new map. Name: " + map.Name);

            Texture2D texture = content.Load<Texture2D>(map.Tileset);

            for (int y = 0; y < map.Height; y++) {
                for (int x = 0; x < map.Width; x++) {
                    Tile tile = new Tile();
                    tile.XPos = x;
                    tile.YPos = y;
                    int ID = map.Layer1Data[y][x];
                    tile.Frame = new Rectangle(IDs[ID][0], IDs[ID][1], Tile.SIZE, Tile.SIZE);
                    tile.Tileset = texture;
                }
            }
        }

        public override void Update(float deltaTime) {
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }
    }
}
