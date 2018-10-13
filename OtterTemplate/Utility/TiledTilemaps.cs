using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using TiledSharp;

namespace GGJ17
{
    // Object Layer in Tiled Maps
    public class TiledObject
    {
        public string Name;
        public Dictionary<string, string> Properties;
        public float X, Y;
    }



    public class TiledTilemap
    {
        public TmxMap tmxMapData;
        public Dictionary<string, Entity> TiledTileLayers;
        public Dictionary<string, TiledObject> TiledMapObjects;

        public void LoadFromFile(string filePath)
        {
            tmxMapData = new TmxMap(filePath);


            // Organize Layers
            TiledTileLayers = new Dictionary<string, Entity>();
            TiledMapObjects = new Dictionary<string, TiledObject>();

            // Load Tile Layers
            foreach (TmxLayer layer in tmxMapData.Layers)
            {

                string imageFilePath = "";

                // Check layer properties for which tileset to use.
                int desiredTileset = 0;

                if (layer.Properties["TilesetID"] != null)
                {
                    desiredTileset = int.Parse(layer.Properties["TilesetID"]);
                }

                imageFilePath = Assets.ASSET_BASE_PATH + "Graphics/" + tmxMapData.Tilesets[desiredTileset].Name;

                Tilemap newLayer = new Tilemap(imageFilePath, tmxMapData.Width * tmxMapData.TileWidth, tmxMapData.Height * tmxMapData.TileHeight, tmxMapData.TileWidth, tmxMapData.TileHeight);

                newLayer.X = (float)layer.OffsetX;
                newLayer.Y = (float)layer.OffsetY;

                // Set tiles from Layer, making sure to flip where appropriate
                foreach (TmxLayerTile tmxTile in layer.Tiles)
                {
                    newLayer.SetTile(tmxTile.X, tmxTile.Y, tmxTile.Gid - 1, "base");
                    newLayer.GetTile(tmxTile.X, tmxTile.Y, "base").FlipX = tmxTile.HorizontalFlip;
                    newLayer.GetTile(tmxTile.X, tmxTile.Y, "base").FlipY = tmxTile.VerticalFlip;
                    newLayer.GetTile(tmxTile.X, tmxTile.Y, "base").FlipD = tmxTile.DiagonalFlip;
                }

                Entity layerEnt = new Entity();
                layerEnt.AddGraphic(newLayer);

                if (layer.Properties["VisualLayer"] != null)
                {
                    layerEnt.Layer = int.Parse(layer.Properties["VisualLayer"]);
                }

                // TEMP!! Adding collider
                if (layer.Name.ToLower().Contains("solid"))
                {
                    GridCollider gridCol = new GridCollider(newLayer.Width, newLayer.Height, newLayer.TileWidth, newLayer.TileHeight, Assets.CollisionTags.SOLID_TILE);
                    layerEnt.AddCollider(gridCol);
                    foreach (TmxLayerTile tmxTile in layer.Tiles)
                    {
                        if (tmxTile.Gid > 0)
                        {
                            gridCol.SetTile(tmxTile.X, tmxTile.Y, true);
                        }
                    }


                }

                ///// ADD TILEMAP TO LIST
                TiledTileLayers.Add(layer.Name, layerEnt);
            }

            // Load Objects from all Object Layers
            foreach (TmxObjectGroup tmxObjectLayer in tmxMapData.ObjectGroups)
            {
                foreach (TmxObject tmxObject in tmxObjectLayer.Objects)
                {
                    TiledObject newObj = new TiledObject();
                    newObj.Name = tmxObject.Name;
                    newObj.X = (float)(tmxObject.X + tmxObjectLayer.OffsetX);
                    newObj.Y = (float)(tmxObject.Y + tmxObjectLayer.OffsetY);

                    foreach (KeyValuePair<string, string> prop in tmxObject.Properties)
                    {
                        newObj.Properties.Add(prop.Key, prop.Value);
                    }

                    ///// ADD MAP OBJECT TO LIST
                    TiledMapObjects.Add(newObj.Name + TiledMapObjects.Count.ToString(), newObj);
                }
            }
        }
    }
}